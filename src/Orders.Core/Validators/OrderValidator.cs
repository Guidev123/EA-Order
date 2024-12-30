using FluentValidation;
using Orders.Core.Entities;

namespace Orders.Core.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            
        }
    }
}
