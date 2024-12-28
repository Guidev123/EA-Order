using Orders.Core.Entities;

namespace Orders.Core.Repositories
{
    public interface IVoucherRepository
    {
        Task UpdateAsync(Voucher voucher);
        Task CreateAsync(Voucher voucher);
    }
}
