namespace Orders.Application.DTOs
{
    public record OrderItemDTO(Guid Id, Guid ProductId, string Name, decimal Price, string Image, int Quantity);
}
