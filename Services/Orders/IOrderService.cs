using WebAPIv1.Domain.Contracts.Orders;
using WebAPIv1.Domain.Models;

namespace WebAPIv1.Services.Orders
{
    public interface IOrderService
    {
        void CreateOrder(Order newOrder);
        void DeleteOrder(Guid id);
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(Guid id);
    }
}
