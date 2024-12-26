using Microsoft.Data.SqlClient;
using Order.Core.Repositories;
using Order.Core.Repositories.Order.Core.Repositories;
using System.Data.Common;

namespace Order.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork(string connectionString) : IUnitOfWork
    {
        private readonly string _connectionString = connectionString;
        private SqlConnection? _connection;
        private DbTransaction? _transaction;
        public SqlConnection Connection => _connection ??= new SqlConnection(_connectionString);
        public DbTransaction? Transaction => _transaction;
        public async Task BeginTransaction()
        {
            if (_connection is null)
            {
                _connection = new SqlConnection(_connectionString);
                await _connection.OpenAsync();
            }

            _transaction = await _connection.BeginTransactionAsync();
        }

        public async Task<bool> Commit()
        {
            if (_transaction is null)
                return false;

            await _transaction.CommitAsync();
            await DisposeTransaction();
            return true;
        }

        public async Task<bool> Rollback()
        {
            if (_transaction is null)
                return false;

            await _transaction.RollbackAsync();
            await DisposeTransaction();
            return true;
        }

        private async Task DisposeTransaction()
        {
            if (_transaction is not null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }

            if (_connection is not null)
            {
                await _connection.CloseAsync();
                await _connection.DisposeAsync();
                _connection = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
