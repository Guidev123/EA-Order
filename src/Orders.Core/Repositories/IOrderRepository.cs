using Orders.Core.Entities;

namespace Orders.Core.Repositories
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order order);
        Task CreateOrderItensAsync(IEnumerable<OrderItem> itens);
    }
}
