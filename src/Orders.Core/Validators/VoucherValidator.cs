using FluentValidation;
using Orders.Core.Entities;

namespace Orders.Core.Validators
{
    public class VoucherValidator : AbstractValidator<Voucher>
    {
        public VoucherValidator()
        {
            RuleFor(x => x.Code).NotNull().NotEmpty()
                .WithMessage("Voucher code can not be empty")
                .MaximumLength(80).WithMessage("Voucher code length should be less than 80 caracters");
        }
    }
}
