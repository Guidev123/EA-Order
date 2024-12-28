using Orders.Core.Entities;

namespace Orders.Core.Repositories
{
    public interface IVoucherRepository
    {
        Task<Voucher?> GetVoucherByCodeAsync(string code);
        void Update(Voucher voucher);
        Task CreateAsync(Voucher voucher);
    }
}
