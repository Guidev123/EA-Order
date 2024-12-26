namespace Orders.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository OrderRepository { get; }
        Task BeginTransaction();
        Task<bool> Commit();
        Task<bool> Rollback();
    }
}

