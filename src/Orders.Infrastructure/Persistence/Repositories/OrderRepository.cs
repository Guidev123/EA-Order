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

            const string sql = @"INSERT INTO Orders (Id, Code, CustomerId, VoucherId, VoucherIsUsed, Discount, TotalPrice, CreatedAt, OrderStatus) 
            VALUES (@Id, @Code, @CustomerId, @VoucherId, @VoucherIsUsed, @Discount, @TotalPrice, @CreatedAt, @OrderStatus)";

            await connection.ExecuteAsync(sql, order);
        }

        public async Task CreateItensAsync(List<OrderItem> items)
        {
            using var connection = _connectionFactory.Create();

            const string sql = @"INSERT INTO OrderItems (Id, OrderId, ProductId, ProductName, Quantity, Price, ProductImage) 
                     VALUES (@Id, @OrderId, @ProductId, @ProductName, @Quantity, @Price, @ProductImage)";

            await connection.ExecuteAsync(sql, items);
        }

        public async Task UpdateOrderStatus(int status)
        {
            using var connection = _connectionFactory.Create();

            const string sql = @"SET OrderStatus = @status ";

            await connection.ExecuteAsync(sql, status);
        }
    }
}
