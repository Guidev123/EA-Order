using Orders.Application.Responses;
using Orders.Core.Repositories;

namespace Orders.Application.UseCases.Orders.Create
{
    public class CreateOrderHandler(IUnitOfWork unitOfWork) : ICreateOrderHandler
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Response<CreateOrderResponse>> HandleAsync(CreateOrderRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
