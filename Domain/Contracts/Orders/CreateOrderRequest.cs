namespace WebAPIv1.Domain.Contracts.Orders
{
    public record CreateOrderRequest
    (
        Guid PetId,
        string OrderStatus,
        double Price,
        bool Complete
    );
}
