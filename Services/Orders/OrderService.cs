using WebAPIv1.Domain.Models;
using Dapper;
using System.Data.SqlClient;

namespace WebAPIv1.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IConfiguration _config;
        public OrderService(IConfiguration config)
        {
            _config = config;
        }

        public async void CreateOrder(Order newOrder)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("INSERT INTO Orders (OrderId, PetId, OrderStatus, Price, Complete) VALUES (@OrderId, @PetId, @OrderStatus, @Price, @Complete)", newOrder);
        }

        public async void DeleteOrder(Guid id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("DELETE FROM Orders WHERE OrderId = @Id", new { Id = id });
        }

        public async Task<List<Order>> GetAllOrders()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return (await connection.QueryAsync<Order>("SELECT * FROM Orders")).ToList();
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return await connection.QueryFirstAsync<Order>("SELECT * FROM Orders WHERE OrderId = @Id", new { Id = id });
        }
    }
}
