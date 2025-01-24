using Orders.Core.Entities;

namespace Orders.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> CreateAsync(Order order);
        Task<Order?> GetByCodeAsync(string code);
        Task<List<Order>?> GetAllAsync(int pageNumber, int pageSize, Guid customerId);
        Task UpdateAsync(Order order);
        Task UpdateToProjectionAsync(Order order);
        Task CreateToProjectionAsync(Order order);
    }
}
