namespace Orders.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransaction();
        Task<bool> Commit();
        Task<bool> Rollback();
    }
}

