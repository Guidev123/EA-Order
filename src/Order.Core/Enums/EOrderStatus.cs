namespace Order.Core.Enums
{
    public enum EOrderStatus
    {
        WaitingPyment = 0,
        Authorized = 1,
        Paid = 2,
        Failed = 3,
        Delivered = 4,
        Canceled = 5
    }
}