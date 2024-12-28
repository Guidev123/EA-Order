using Orders.Application.Responses;

namespace Orders.Application.UseCases.Orders.Create
{
    public interface ICreateOrderHandler
    {
        Task<Response<CreateOrderResponse>> HandleAsync(CreateOrderRequest request);
    }
}
