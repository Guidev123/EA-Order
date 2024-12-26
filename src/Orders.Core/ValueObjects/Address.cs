namespace Orders.Core.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string AdditionalInfo { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
    }
}
