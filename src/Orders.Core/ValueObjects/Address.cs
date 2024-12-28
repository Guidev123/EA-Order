namespace Orders.Core.ValueObjects
{
    public record Address(string Street, string Number,
                          string AdditionalInfo, string Neighborhood,
                          string ZipCode, string City,
                          string State) : ValueObject;
}
