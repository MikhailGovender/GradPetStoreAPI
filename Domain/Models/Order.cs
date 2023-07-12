namespace WebAPIv1.Domain.Models
{
    public class Order
    {
        public Guid OrderId { get; }
        public Guid PetId { get; set; }
        public string OrderStatus { get; set; }
        public double Price { get; set; }
        public int Complete { get; set; }

        public Order(Guid orderId, Guid petId, string orderStatus, double price, bool complete)
        {
            OrderId = orderId;
            PetId = petId;
            OrderStatus = orderStatus ?? throw new ArgumentNullException(nameof(orderStatus));
            Price = price;
            Complete = complete ? 1 : 0;
        }
    }
}
