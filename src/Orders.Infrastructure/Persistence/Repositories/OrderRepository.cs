using Dapper;
using Orders.Core.Entities;
using Orders.Core.Repositories;
using Orders.Infrastructure.Persistence.Factories;

namespace Orders.Infrastructure.Persistence.Repositories
{
    public class OrderRepository(SqlConnectionFactory connectionFactory) : IOrderRepository
    {
        private readonly SqlConnectionFactory _connectionFactory = connectionFactory;

        public async Task<bool> CreateAsync(Order order)
        {
            using var connection = _connectionFactory.Create();

            using var transaction = await connection.BeginTransactionAsync();

            try
            {
                const string sqlOrder = @"INSERT INTO Orders
                                    (Id, Code, CustomerId, VoucherId, VouchersUsed, Discount, TotalPrice, 
                                    CreatedAt, OrderStatus, Street, Number, AdditionalInfo, Neighborhood, ZipCode, City, State)
                                VALUES
                                    (@Id, @Code, @CustomerId, @VoucherId, @VoucherIsUsed, @Discount, @TotalPrice, 
                                    @CreatedAt, @OrderStatus, @Street, @Number, @AdditionalInfo, @Neighborhood, 
                                    @ZipCode, @City, @State);";

                await connection.ExecuteAsync(sqlOrder, order, transaction);

                const string sqlItems = @"INSERT INTO OrderItems
                                    (Id, OrderId, ProductId, ProductName, Quantity, UnitValue, ProductImage)
                                VALUES
                                    (@Id, @OrderId, @ProductId, @ProductName, @Quantity, @UnitValue, @ProductImage);";

                var orderItens = order.OrderItems.Select(item => new
                {
                    item.Id,
                    item.OrderId,
                    item.ProductId,
                    item.ProductName,
                    item.Quantity,
                    item.UnitValue,
                    item.ProductImage
                });

                await connection.ExecuteAsync(sqlItems, orderItens, transaction);

                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
