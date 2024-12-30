using Orders.Core.Entities;

namespace Orders.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> CreateAsync(Order order);
    }
}
