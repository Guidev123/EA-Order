using Dapper;
using Orders.Core.Entities;
using Orders.Core.Repositories;
using Orders.Infrastructure.Persistence.Factories;

namespace Orders.Infrastructure.Persistence.Repositories
{
    public class VoucherRepository(SqlConnectionFactory connectionFactory) : IVoucherRepository
    {
        private readonly SqlConnectionFactory _connectionFactory = connectionFactory;

        public Task CreateAsync(Voucher voucher)
        {
            throw new NotImplementedException();
        }

        public async Task<Voucher?> GetVoucherByCodeAsync(string code)
        {
            using var connection = _connectionFactory.Create();

            const string sql = @"SELECT * FROM Vouchers WHERE Code = @Code";

            return await connection.QueryFirstOrDefaultAsync<Voucher>(sql, code);
        }

        public void Update(Voucher voucher)
        {
            throw new NotImplementedException();
        }
    }
}
