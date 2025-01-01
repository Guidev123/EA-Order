using Dapper;
using MongoDB.Driver;
using Orders.Core.Entities;
using Orders.Core.Repositories;
using Orders.Infrastructure.Persistence.Factories;

namespace Orders.Infrastructure.Persistence.Repositories
{
    public class OrderRepository(SqlConnectionFactory connectionFactory, IMongoDatabase mongoDatabase)
               : IOrderRepository
    {
        private readonly SqlConnectionFactory _connectionFactory = connectionFactory;
        private readonly IMongoCollection<Order> _orderCollection = mongoDatabase.GetCollection<Order>("orders");
        public async Task<bool> CreateAsync(Order order)
        {
            using var connection = _connectionFactory.Create();
            await connection.OpenAsync();

            using var transaction = await connection.BeginTransactionAsync();

            try
            {
                const string sqlOrder = @"INSERT INTO Orders
                            (Id, Code, CustomerId, VoucherId, VoucherIsUsed, Discount, TotalPrice, 
                            CreatedAt, OrderStatus, Street, Number, AdditionalInfo, Neighborhood, ZipCode, City, State)
                        VALUES
                            (@Id, @Code, @CustomerId, @VoucherId, @VoucherIsUsed, @Discount, @TotalPrice, 
                            @CreatedAt, @OrderStatus, @Street, @Number, @AdditionalInfo, @Neighborhood, 
                            @ZipCode, @City, @State);";

                var orderParams = new
                {
                    order.Id,
                    order.Code,
                    order.CustomerId,
                    order.VoucherId,
                    order.VoucherIsUsed,
                    order.Discount,
                    order.TotalPrice,
                    order.CreatedAt,
                    order.OrderStatus,
                    order.Address!.Street,
                    order.Address.Number,
                    order.Address.AdditionalInfo,
                    order.Address.Neighborhood,
                    order.Address.ZipCode,
                    order.Address.City,
                    order.Address.State
                };

                await connection.ExecuteAsync(sqlOrder, orderParams, transaction);

                const string sqlItems = @"INSERT INTO OrderItems
                                    (Id, OrderId, ProductId, ProductName, Quantity, UnitValue, ProductImage)
                                VALUES
                                    (@Id, @OrderId, @ProductId, @ProductName, @Quantity, @UnitValue, @ProductImage);";


                await connection.ExecuteAsync(sqlItems, order.OrderItems, transaction);

                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task CreateToProjectionAsync(Order order)
            => await _orderCollection.InsertOneAsync(order);

        public async Task<List<Order>?> GetAllAsync(int pageNumber, int pageSize, Guid customerId)
            => await _orderCollection.Find(x => x.CustomerId == customerId).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();

        public async Task<Order?> GetByCodeAsync(string code)
            => await _orderCollection.Find(c => c.Code == code).SingleOrDefaultAsync();
    }
}
