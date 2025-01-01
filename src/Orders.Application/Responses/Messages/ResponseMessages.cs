using System.ComponentModel;
using System.Reflection;

namespace Orders.Application.Responses.Messages
{
    public enum ResponseMessages
    {
        [Description("Order not found")]
        ORDER_NOT_FOUND,
        [Description("Order created")]
        ORDER_CREATED,
        [Description("The currrent operation failed")]
        INVALID_OPERATION,
        [Description("Order price is not correct")]
        INCORRECT_PRICE,
        [Description("Voucher not found")]
        VOUCHER_NOT_FOUND,
        [Description("Voucher is not valid to use")]
        VOUCHER_NOT_VALID,
        [Description("Something failed to persist data")]
        PERSISTENCE_FAILED,
        [Description("The current operation was successful")]
        SUCCESS_OPERATION,
        [Description("No orders found")]
        NO_ORDERS_FOUND
    }

    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString())!;

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
                return attributes.First().Description;

            return value.ToString();
        }
    }
}
