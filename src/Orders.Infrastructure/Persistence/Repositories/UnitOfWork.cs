using MediatR;
using Microsoft.Data.SqlClient;
using Orders.Core.DomainObjects;
using Orders.Core.Repositories;
using Orders.Infrastructure.Persistence.Factories;
using System.Data;
using System.Data.Common;

namespace Orders.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork(SqlConnectionFactory connectionFactory,
                            IOrderRepository orderRepository,
                            IVoucherRepository voucherRepository,
                            IPublisher publisher)
                          : IUnitOfWork, IDisposable
    {
        private readonly SqlConnection _connection = connectionFactory.Create();
        private DbTransaction? _transaction;
        private readonly IPublisher _publisher = publisher; 

        public IOrderRepository Orders => orderRepository;
        public IVoucherRepository Vouchers => voucherRepository;

        public async Task BeginTransactionAsync()
        {
            if (_connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();

            _transaction = await _connection.BeginTransactionAsync();
        }

        public async Task<bool> Commit()
        {
            if (_transaction is null) return false;

            await _transaction.CommitAsync();
            return true;
        }

        public async Task<bool> Rollback()
        {
            if (_transaction is null) return false;

            await _transaction.RollbackAsync();
            return true;
        }

        public void Dispose()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();

            _connection.Dispose();

            if (_transaction is not null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            GC.SuppressFinalize(this);
        }

        public async Task PublishDomainEventsAsync<TEntity>(TEntity entity)
            where TEntity : Entity
        {
            foreach (var domainEvent in entity.Events)
                await _publisher.Publish(domainEvent);

            entity.ClearAllEvents();
        }
    }
}
