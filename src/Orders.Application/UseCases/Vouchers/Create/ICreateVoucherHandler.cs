using Orders.Application.Responses;

namespace Orders.Application.UseCases.Vouchers.Create
{
    public interface ICreateVoucherHandler
    {
        Task<Response<CreateVoucherResponse>> HandleAsync(CreateVoucherRequest request);
    }
}
