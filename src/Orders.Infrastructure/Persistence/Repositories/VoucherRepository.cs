using Dapper;
using Microsoft.EntityFrameworkCore;
using Orders.Core.Entities;
using Orders.Core.Repositories;
using Orders.Infrastructure.Persistence.Contexts;
using Orders.Infrastructure.Persistence.Factories;

namespace Orders.Infrastructure.Persistence.Repositories
{
    public class VoucherRepository(SqlConnectionFactory connectionFactory, ReadDbContext context) : IVoucherRepository
    {
        private readonly SqlConnectionFactory _connectionFactory = connectionFactory;
        private readonly ReadDbContext _context = context;
        public async Task CreateAsync(Voucher voucher)
        {
            using var connection = _connectionFactory.Create();

            const string sql = @"
                INSERT INTO Vouchers 
                    (Id, Code, Percentual,
                    DiscountValue, Quantity, DiscountType,
                    CreatedAt, ExpiresAt, IsActive)
                VALUES 
                    (@Id, @Code, @Percentual,
                     @DiscountValue, @Quantity, @DiscountType,
                     @CreatedAt, @ExpiresAt, @IsActive)";

            await connection.ExecuteAsync(sql, new
            {
                voucher.Id,
                voucher.Code,
                voucher.Percentual,
                voucher.DiscountValue,
                voucher.Quantity,
                voucher.DiscountType,
                voucher.CreatedAt,
                voucher.ExpiresAt,
                voucher.IsActive
            });
        }

        public async Task CreateToProjectionAsync(Voucher voucher) =>
            await _context.Vouchers.AddAsync(voucher);

        public async Task<Voucher?> GetByCodeAsync(string code) =>
            await _context.Vouchers.FirstOrDefaultAsync(x => x.Code == code);

        public async Task UpdateAsync(Voucher voucher)
        {
            using var connection = _connectionFactory.Create();

            const string sql = @"UPDATE Vouchers
                                    SET 
                                        Percentual = @Percentual,
                                        DiscountValue = @DiscountValue,
                                        Quantity = @Quantity,
                                        DiscountType = @DiscountType,
                                        ExpiresAt = @ExpiresAt,
                                        IsActive = @IsActive
                                    WHERE Id = @Id";

            await connection.ExecuteAsync(sql, new
            {
                voucher.Percentual,
                voucher.DiscountValue,
                voucher.Quantity,
                voucher.DiscountType,
                voucher.ExpiresAt,
                voucher.IsActive,
                voucher.Id
            });
        }
    }
}
