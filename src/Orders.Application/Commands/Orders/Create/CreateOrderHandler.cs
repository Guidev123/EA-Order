using FluentValidation.Results;
using MediatR;
using Orders.Application.Events.Factories;
using Orders.Application.Events.Vouchers;
using Orders.Application.Mappers;
using Orders.Application.Responses;
using Orders.Core.Entities;
using Orders.Core.Repositories;
using Orders.Core.Validators;

namespace Orders.Application.Commands.Orders.Create
{
    public sealed class CreateOrderHandler(IUnitOfWork unitOfWork)
                      : CommandHandler, IRequestHandler<CreateOrderCommand, Response<CreateOrderResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Response<CreateOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = request.MapToEntity();

            order.ApplyAddress(request.Address.MapToAddress());
            order.AddItems(request.OrderItems.MapOrderItemToEntity(order.Id));

            var validation = ValidateEntity(new OrderValidator(), order);

            if (!validation.IsValid)
                return new(null, 400, "Error", GetAllErrors(validation));

            var voucher = await ApplyVoucherAsync(request, order, validation);
            if (!voucher.IsSuccess)
                return new(null, 400, "Error", GetAllErrors(validation));

            if (!ValidateOrder(order))
            {
                AddError(validation, "Order price is not correct");
                return new(null, 400, "Error", GetAllErrors(validation));
            }

            order.AuthorizeOrder();

            var result = await _unitOfWork.Orders.CreateAsync(order);
            if (!result) return new(null, 400, "Something has failed to persist data");

            order.AddEvent(OrderEventFactory.CreateOrderCreatedProjectionEvent(order));

            await _unitOfWork.Vouchers.UpdateAsync(voucher.Data!);
            await _unitOfWork.PublishDomainEventsAsync(order);
            await _unitOfWork.PublishDomainEventsAsync(voucher.Data!);

            return new(new(order.Code), 201);
        }

        #region Validators Methods
        private async Task<Response<Voucher>> ApplyVoucherAsync(CreateOrderCommand command, Order order, ValidationResult validationResult)
        {
            if (!command.VoucherIsUsed) return new(null, 200);

            var voucher = await _unitOfWork.Vouchers.GetByCodeAsync(command.VoucherCode);
            if (voucher is null)
            {
                AddError(validationResult, "Voucher not found");
                return new(null, 400);
            }

            if(!voucher.IsValid())
            {
                AddError(validationResult, "Voucher is not valid to use");
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
