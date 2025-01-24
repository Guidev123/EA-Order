namespace Orders.Core.Enums
{
    public enum EOrderStatus
    {
        Delivered = 0,
        Paid = 1,
        Authorized = 2,
        Failed = 3,
        WaitingPyment = 4,
        Canceled = 5
    }
}