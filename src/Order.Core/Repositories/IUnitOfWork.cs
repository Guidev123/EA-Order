namespace Order.Core.Repositories
{
    using Microsoft.Data.SqlClient;
    using System.Data.Common;

    namespace Order.Core.Repositories
    {
        public interface IUnitOfWork : IDisposable
        {
            SqlConnection Connection { get; }
            DbTransaction? Transaction { get; }
            Task BeginTransaction();
            Task<bool> Commit();
            Task<bool> Rollback();
        }
    }

}
