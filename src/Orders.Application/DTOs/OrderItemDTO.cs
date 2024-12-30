namespace Orders.Application.DTOs
{
    public record OrderItemDTO(Guid OrderId, Guid ProductId,
                               string Name, decimal Price,
                               string Image, int Quantity);
}
