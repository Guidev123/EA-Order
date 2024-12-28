using Dapper;
using Orders.Core.Entities;
using Orders.Core.Repositories;
using Orders.Infrastructure.Persistence.Factories;

namespace Orders.Infrastructure.Persistence.Repositories
{
    public class OrderRepository(SqlConnectionFactory connectionFactory) : IOrderRepository
    {
        private readonly SqlConnectionFactory _connectionFactory = connectionFactory;
        public async Task CreateAsync(Order order)
        {
            using var connection = _connectionFactory.Create();

            const string sql = @"INSERT INTO Orders (Code, CustomerId, VoucherId,
                                 VoucherIsUsed, Discount, TotalPrice, CreatedAt,
                                 OrderStatus, Address, Voucher)
                                 VALUES (@Code, @CustomerId, @VoucherId,
                                 @VoucherIsUsed, @Discount, @TotalPrice, @CreatedAt,
                                 @OrderStatus, @Address, @Voucher)";

            await connection.ExecuteAsync(sql, order);
        }

        public async Task<List<Order>?> GetAllAsync(int pageNumber, int pageSize, string customerId)
        {
            using var connection = _connectionFactory.Create();

            const string sql = @"SELECT * FROM Orders";

            var orders = await connection.QueryAsync<Order>(sql);

            return orders.ToList();
        }

        public Task<Order?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem?> GetItemByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem?> GetItemByOrder(Guid orderId, Guid productId)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
