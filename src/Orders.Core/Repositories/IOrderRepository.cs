using Orders.Core.Entities;
using Orders.Core.Enums;

namespace Orders.Core.Repositories
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order order);
        Task CreateItensAsync(OrderItem item);
        Task UpdateOrderStatus(int status);
    }
}
