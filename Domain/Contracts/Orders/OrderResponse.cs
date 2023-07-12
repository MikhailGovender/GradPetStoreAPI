namespace WebAPIv1.Domain.Contracts.Orders
{
    public record OrderResponse
    (
        Guid OrderId,
        Guid PetId,
        string OrderStatus,
        double Price,
        bool Complete
    );
}
