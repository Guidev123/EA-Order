namespace Orders.Application.DTOs
{
    public record OrderItemDTO(Guid ProductId, string Name, decimal Price,
                               string Image, int Quantity, Guid OrderId);
}
