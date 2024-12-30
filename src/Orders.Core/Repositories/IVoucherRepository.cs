using Orders.Core.Entities;

namespace Orders.Core.Repositories
{
    public interface IVoucherRepository
    {
        Task<Voucher?> GetByCodeAsync(string code);
        Task UpdateAsync(Voucher voucher);
        Task CreateAsync(Voucher voucher);
    }
}
