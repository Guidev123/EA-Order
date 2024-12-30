using Orders.Core.DomainObjects;

namespace Orders.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }
        IVoucherRepository Vouchers { get; }
        Task PublishDomainEventsAsync<TEntity>(TEntity entity) where TEntity : Entity;
        Task BeginTransactionAsync();
        Task<bool> Commit();
        Task<bool> Rollback();
    }
}

