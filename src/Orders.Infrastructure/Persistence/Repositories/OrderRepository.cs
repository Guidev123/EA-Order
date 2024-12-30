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

            const string sql = @"INSERT INTO Orders
                                     (Id, Code, CustomerId, VoucherId, VouchersUsed, Discount, TotalPrice, 
                                     CreatedAt, OrderStatus, Street, Number, AdditionalInfo, Neighborhood, ZipCode, City, State)
                                 VALUES
                                    (@Id, @Code, @CustomerId, @VoucherId, @VoucherIsUsed, @Discount, @TotalPrice, 
                                    @CreatedAt, @OrderStatus, @Street, @Number, @AdditionalInfo, @Neighborhood, 
                                    @ZipCode, @City, @State);";

            await connection.ExecuteAsync(sql, order);
        }

        public async Task CreateOrderItensAsync(IEnumerable<OrderItem> itens)
        {
            using var connection = _connectionFactory.Create();

            const string sql = @"INSERT INTO OrderItems
                                    (Id, OrderId, ProductId, ProductName, Quantity, UnitValue, ProductImage)
                               VALUES
                                    (@Id, @OrderId, @ProductId, @ProductName, @Quantity, @Price, @ProductImage);";

            var orderItens = itens.Select(item => new
            {
                item.Id,
                item.OrderId,
                item.ProductId,
                item.ProductName,
                item.Quantity,
                item.Price,
                item.ProductImage
            });

            await connection.ExecuteAsync(sql, orderItens);
        }

    }
}
