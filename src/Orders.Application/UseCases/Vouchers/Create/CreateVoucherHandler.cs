using Orders.Application.Responses;
using Orders.Core.Repositories;

namespace Orders.Application.UseCases.Vouchers.Create
{
    public class CreateVoucherHandler(IUnitOfWork unitOfWork) : ICreateVoucherHandler
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Response<CreateVoucherResponse>> HandleAsync(CreateVoucherRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
