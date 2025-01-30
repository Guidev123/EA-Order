using EA.IntegrationEvents.Integration.ReceivedAddress;
using FluentValidation.Results;
using MediatR;
using Orders.Application.Events.Factories;
using Orders.Application.Events.Orders;
using Orders.Application.Events.Vouchers;
using Orders.Application.Mappers;
using Orders.Application.Responses;
using Orders.Application.Responses.Messages;
using Orders.Application.Services;
using Orders.Core.Entities;
using Orders.Core.Repositories;
using Orders.Core.Validators;
using SharedLib.MessageBus;

namespace Orders.Application.Commands.Orders.Create
{
    public sealed class CreateOrderHandler(IUnitOfWork unitOfWork,
                        IUserService userService,
                        IMessageBus bus)
                      : CommandHandler, IRequestHandler<CreateOrderCommand, Response<CreateOrderResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUserService _userService = userService;
        private readonly IMessageBus _bus = bus;
        public async Task<Response<CreateOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var customerId = await _userService.GetUserIdAsync();
            if (customerId is null || !customerId.HasValue)
                return new(null, 404, ResponseMessages.INVALID_OPERATION.GetDescription());

            var order = request.MapToEntity(customerId.Value);

            var address = request.Address.MapToAddress();
            order.ApplyAddress(address);
            await _bus.PublishAsync(new ReceivedAddressIntegrationEvent(address.Street, address.Number, address.AdditionalInfo,
                                    address.Neighborhood, address.ZipCode,
                                    address.City, address.State));

            order.AddItems(request.OrderItems.MapOrderItemToEntity(order.Id));

            var validation = ValidateEntity(new OrderValidator(), order);

            if (!validation.IsValid)
                return new(null, 400, ResponseMessages.INVALID_OPERATION.GetDescription(), GetAllErrors(validation));

            var voucher = await ApplyVoucherAsync(request, order, validation);
            if (!voucher.IsSuccess)
                return new(null, 400, ResponseMessages.INVALID_OPERATION.GetDescription(), GetAllErrors(validation));

            if (!ValidateOrder(order))
            {
                AddError(validation, ResponseMessages.INCORRECT_PRICE.GetDescription());
                return new(null, 400, ResponseMessages.INVALID_OPERATION.GetDescription(), GetAllErrors(validation));
            }

            order.AuthorizeOrder();

            var result = await _unitOfWork.Orders.CreateAsync(order);
            if (!result) return new(null, 400, ResponseMessages.PERSISTENCE_FAILED.GetDescription());

            order.AddEvent(OrderEventFactory.CreateOrderCreatedProjectionEvent(order));

            await _unitOfWork.PublishDomainEventsAsync(order);

            return new(new(order.Code), 201, ResponseMessages.SUCCESS_OPERATION.GetDescription());
        }

        #region Validators Methods
        private async Task<Response<Voucher>> ApplyVoucherAsync(CreateOrderCommand command, Order order, ValidationResult validationResult)
        {
            if (!command.VoucherIsUsed) return new(null, 200);

            var voucher = await _unitOfWork.Vouchers.GetByCodeAsync(command.VoucherCode);
            if (voucher is null)
            {
                AddError(validationResult, ResponseMessages.VOUCHER_NOT_FOUND.GetDescription());
                return new(null, 400);
            }

            if(!voucher.IsValid())
            {
                AddError(validationResult, ResponseMessages.VOUCHER_NOT_VALID.GetDescription());
                return new(null, 400);
            }

            var voucherValidation = new VoucherValidator().Validate(voucher);
            if (!voucherValidation.IsValid)
            {
                voucherValidation.Errors.ToList().ForEach(m => AddError(validationResult, m.ErrorMessage));
                return new(null, 400);
            }

            order.ApplyVoucher(voucher);
            voucher.DebitQuantity();

            voucher.AddEvent(new VoucherUpdatedProjectionEvent(voucher.MapFromEntity()));

            await _unitOfWork.Vouchers.UpdateAsync(voucher);
            await _unitOfWork.PublishDomainEventsAsync(voucher);

            return new(voucher, 200);
        }

        private static bool ValidateOrder(Order order)
        {
            var orderOriginalPrice = order.TotalPrice;
            var orderDiscount = order.Discount;

            order.CalculateOrderPrice();

            if (order.TotalPrice != orderOriginalPrice) return false;
            if (order.Discount != orderDiscount) return false;

            return true;
        }

        #endregion
    }
}
