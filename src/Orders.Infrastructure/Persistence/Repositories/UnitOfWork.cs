﻿using Microsoft.Data.SqlClient;
using Orders.Core.Repositories;
using Orders.Infrastructure.Persistence.Factories;
using System.Data;
using System.Data.Common;

namespace Orders.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork(SqlConnectionFactory connectionFactory,
                            IOrderRepository orderRepository) : IUnitOfWork, IDisposable
    {
        private readonly SqlConnection _connection = connectionFactory.Create();
        private DbTransaction? _transaction;

        IOrderRepository IUnitOfWork.OrderRepository => orderRepository;

        public async Task BeginTransaction()
        {
            if (_connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();

            _transaction = await _connection.BeginTransactionAsync();
        }

        public async Task<bool> Commit()
        {
            if (_transaction is null) return false;

            await _transaction.CommitAsync();
            await DisposeTransaction();
            return true;
        }

        public async Task<bool> Rollback()
        {
            if (_transaction is null) return false;

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
        }

        public void Dispose()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();

            _connection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
