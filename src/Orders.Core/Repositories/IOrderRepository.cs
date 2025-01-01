using Orders.Core.Entities;

namespace Orders.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> CreateAsync(Order order);
        Task<Order?> GetByCodeAsync(string code);
        Task<List<Order>?> GetAllAsync(int pageNumber, int pageSize, Guid customerId);
        Task CreateToProjectionAsync(Order order);
    }
}
