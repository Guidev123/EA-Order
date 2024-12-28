namespace Orders.Core.ValueObjects
{
    public abstract record ValueObject
    {
        public override int GetHashCode() => base.GetHashCode();
        public override string? ToString() => base.ToString();
    }
}
