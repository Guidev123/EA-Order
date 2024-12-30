namespace Orders.Core.Events
{
    public abstract class Event
    {
        public Event()
        {
            OccurredAt = DateTime.Now;
            EventId = Guid.NewGuid();
        }
        public Guid EventId { get; private set; }
        public DateTime OccurredAt { get; }
    }
}
