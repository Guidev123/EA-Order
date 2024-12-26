using Orders.Core.Entities;

namespace Orders.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id);
        Task<List<Order>?> GetAllAsync(int pageNumber, int pageSize, string customerId);
        Task CreateAsync(Order order);
        void UpdateAsync(Order order);
        Task<OrderItem?> GetItemByIdAsync(Guid id);
        Task<OrderItem?> GetItemByOrder(Guid orderId, Guid productId);
    }
}
