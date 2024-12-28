using Orders.Core.Entities;
using Orders.Core.Enums;

namespace Orders.Core.Repositories
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order order);
        Task CreateItensAsync(List<OrderItem> items);
        Task UpdateOrderStatus(int status);
    }
}
