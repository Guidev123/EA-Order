using MediatR;
using Orders.Application.Commands;
using Orders.Application.Responses;
using Orders.Core.Repositories;

namespace Orders.Application.Queries.Vouchers.GetVoucherByCode
{
    public sealed class GetVoucherByCodeHandler(IUnitOfWork unitOfWork)
                      : CommandHandler, IRequestHandler<GetVoucherByCodeQuery, Response<GetVoucherByCodeResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Response<GetVoucherByCodeResponse>> Handle(GetVoucherByCodeQuery request, CancellationToken cancellationToken)
        {
            var voucher = await _unitOfWork.Vouchers.GetByCodeAsync(request.Code);
            if (voucher is null) return new(null, 404, "Vocher not found");
            
            return new(new(voucher.Percentual, voucher.DiscountValue, voucher.Code, voucher.DiscountType), 200);
        }
    }
}
