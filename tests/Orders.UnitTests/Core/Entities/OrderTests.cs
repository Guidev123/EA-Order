using Orders.Core.Entities;
using Orders.Core.Enums;

namespace Orders.UnitTests.Core.Entities
{
    public class OrderTests
    {
        [Fact]
        public void Should_Calculate_Order_Price()
        {
            var order = new Order(Guid.NewGuid(), 0);
            var items = new List<OrderItem>
            {
                new(Guid.NewGuid(), Guid.NewGuid(), "Item 1", 2, 10, "image1.png"),
                new(Guid.NewGuid(), Guid.NewGuid(), "Item 2", 1, 20, "image2.png")
            };

            order.AddItems(items);
            order.CalculateOrderPrice();

            Assert.Equal(40, order.TotalPrice);
        }

        [Fact]
        public void Should_Apply_Voucher_Discount_Percentual()
        {
            var order = new Order(Guid.NewGuid(), 0);
            var items = new List<OrderItem>
            {
                new(Guid.NewGuid(), Guid.NewGuid(), "Item 1", 1, 100, "image1.png")
            };

            var voucher = new Voucher("PERCENT10", 10, null, 100, EDiscountType.Percentual, DateTime.Now.AddDays(10));

            order.AddItems(items);
            order.ApplyVoucher(voucher);
            order.CalculateOrderPrice();

            Assert.Equal(90, order.TotalPrice);
            Assert.Equal(10, order.Discount);
        }

        [Fact]
        public void Should_Apply_Voucher_Discount_Value()
        {
            // Arrange
            var order = new Order(Guid.NewGuid(), 0);
            var items = new List<OrderItem>
            {
                new(Guid.NewGuid(), Guid.NewGuid(), "Item 1", 1, 100, "image1.png")
            };

            var voucher = new Voucher("FIXED50", 0, 50, 100, EDiscountType.Value, DateTime.Now.AddDays(10));

            order.AddItems(items);
            order.ApplyVoucher(voucher);
            order.CalculateOrderPrice();

            Assert.Equal(50, order.TotalPrice);
            Assert.Equal(50, order.Discount);
        }

        [Fact]
        public void Should_Not_Apply_Voucher_When_Expired()
        {
            // Arrange
            var order = new Order(Guid.NewGuid(), 0);
            var items = new List<OrderItem>
            {
                new(Guid.NewGuid(), Guid.NewGuid(), "Item 1", 1, 100, "image1.png")
            };

            var voucher = new Voucher("EXPIRED", 0, 50, 100, EDiscountType.Value, DateTime.Now.AddDays(-1));

            order.AddItems(items);
            order.ApplyVoucher(voucher);
            order.CalculateOrderPrice();

            Assert.False(order.VoucherIsUsed);
            Assert.Equal(100, order.TotalPrice);
        }

        [Fact]
        public void Should_Update_Order_Status_To_Authorized()
        {
            var order = new Order(Guid.NewGuid(), 0);

            order.AuthorizeOrder();

            Assert.Equal(EOrderStatus.Authorized, order.OrderStatus);
        }

        [Fact]
        public void Should_Update_Order_Status_To_Canceled()
        {
            var order = new Order(Guid.NewGuid(), 0);

            order.CancelOrder();

            Assert.Equal(EOrderStatus.Canceled, order.OrderStatus);
        }

        [Fact]
        public void Should_Update_Order_Status_To_Paid()
        {
            var order = new Order(Guid.NewGuid(), 0);

            order.PayOrder();

            Assert.Equal(EOrderStatus.Paid, order.OrderStatus);
        }

        [Fact]
        public void Should_Update_Order_Status_To_Delivered()
        {
            var order = new Order(Guid.NewGuid(), 0);

            order.DeliveryOrder();

            Assert.Equal(EOrderStatus.Delivered, order.OrderStatus);
        }
    }
}
