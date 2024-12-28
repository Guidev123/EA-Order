namespace Orders.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }
        IVoucherRepository Vouchers { get; }
        Task BeginTransaction();
        Task<bool> Commit();
        Task<bool> Rollback();
    }
}

