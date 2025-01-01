using MediatR;
using Orders.Application.Events.Factories;
using Orders.Application.Mappers;
using Orders.Application.Responses;
using Orders.Application.Responses.Messages;
using Orders.Core.Repositories;
using Orders.Core.Validators;

namespace Orders.Application.Commands.Vouchers.Create
{
    public sealed class CreateVoucherHandler(IUnitOfWork unitOfWork) 
                      : CommandHandler, IRequestHandler<CreateVoucherCommand,
                        Response<CreateVoucherResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Response<CreateVoucherResponse>> Handle(CreateVoucherCommand request, CancellationToken cancellationToken)
        {
            var voucher = request.MapToEntity();
            var validation = ValidateEntity(new VoucherValidator(), voucher);

            if(!validation.IsValid)
                return new(null, 400, ResponseMessages.INVALID_OPERATION.GetDescription(), GetAllErrors(validation));

            voucher.AddEvent(VoucherEventFactory.CreateVoucherCreatedProjectionEvent(voucher));

            await _unitOfWork.Vouchers.CreateAsync(voucher);
            await _unitOfWork.PublishDomainEventsAsync(voucher);

            return new(new(voucher.Code), 201, ResponseMessages.SUCCESS_OPERATION.GetDescription());
        }
    }
}
