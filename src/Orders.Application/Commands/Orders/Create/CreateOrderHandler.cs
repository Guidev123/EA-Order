using FluentValidation.Results;
using MediatR;
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
            var validation = ValidateEntity(new OrderValidator(), order);

            if (!validation.IsValid)
                return new(null, 400, "Error", GetAllErrors(validation));

            await _unitOfWork.BeginTransactionAsync();
            
            try
            {
                if (!await ApplyVoucherAsync(request, order, validation))
                    return new(null, 400, "Error", GetAllErrors(validation));

                if (!ValidateOrder(order))
                {
                    AddError(validation, "Order price is not correct");
                    return new(null, 400, "Error", GetAllErrors(validation));
                }

                // TODO: Process Payment
                order.AuthorizeOrder();

                await _unitOfWork.Orders.CreateAsync(order);
                await _unitOfWork.Orders.CreateOrderItensAsync(order.OrderItems);

                await _unitOfWork.Commit();

                return new(new(order.Code), 201);
            }
            catch
            {
                await _unitOfWork.Rollback();
                return new(null, 400, "Fail to persist data");
            }
        }

        #region Validators Methods
        private async Task<bool> ApplyVoucherAsync(CreateOrderCommand command, Order order, ValidationResult validationResult)
        {
            if (!command.VoucherIsUsed) return true;

            var voucher = await _unitOfWork.Vouchers.GetByCode(command.VoucherCode);
            if (voucher is null)
            {
                AddError(validationResult, "Voucher not found");
                return false;
            }

            var voucherValidation = new VoucherValidator().Validate(voucher);
            if (!voucherValidation.IsValid)
            {
                voucherValidation.Errors.ToList().ForEach(m => AddError(validationResult, m.ErrorMessage));
                return false;
            }

            order.ApplyVoucher(voucher);
            voucher.DebitQuantity();

            await _unitOfWork.Vouchers.UpdateAsync(voucher);

            return true;
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
