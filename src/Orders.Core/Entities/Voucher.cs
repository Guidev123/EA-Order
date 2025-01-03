﻿using Orders.Core.DomainObjects;
using Orders.Core.Enums;

namespace Orders.Core.Entities
{
    public class Voucher : Entity, IAggregateRoot
    {
        public Voucher() { }
        public Voucher(string code, decimal? percentual,
               decimal? discountValue, int? quantity,
               EDiscountType? discountType, DateTime expiresAt)
        {
            Code = code;
            Percentual = percentual;
            DiscountValue = discountValue;
            Quantity = quantity;
            ExpiresAt = expiresAt;
            DiscountType = discountType ?? EDiscountType.Value;
            IsActive = true;
            CreatedAt = DateTime.Now;
        }
        public string Code { get; private set; } = string.Empty;
        public decimal? Percentual { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public int? Quantity { get; private set; }
        public EDiscountType DiscountType { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }

        public void DebitQuantity()
        {
            Quantity -= 1;
            if (Quantity >= 1) return;
        }

        public bool IsValid()
            => IsActive && Quantity > 0 && ExpiresAt >= DateTime.Now;

        public void SetExpirationDate(DateTime expirationDate) => ExpiresAt = expirationDate;

        // CTOR mapping
        public Voucher(Guid id, string code, decimal? percentual,
                       decimal? discountValue, int? quantity,
                       EDiscountType? discountType, DateTime expiresAt, DateTime createdAt)
        {
            Id = id;
            Code = code;
            Percentual = percentual;
            DiscountValue = discountValue;
            Quantity = quantity;
            ExpiresAt = expiresAt;
            DiscountType = discountType ?? EDiscountType.Value;
            IsActive = true;
            CreatedAt = createdAt;
        }
    }
}
