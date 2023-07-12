using WebAPIv1.Domain.Models;
using Dapper;
using Microsoft.AspNetCore.JsonPatch;
using System.Data.SqlClient;
using WebAPIv1.Domain.Contracts.Users;

namespace WebAPIv1.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;

        public UserService(IConfiguration config)
        {
            _config = config;
        }

        public async void CreateUser(User newUser)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(
                "INSERT INTO Users(UserId, UserName, Password, CreatedAt)" +
                "VALUES (@UserId, @UserName, @Password, @CreatedAt)", newUser);
        }

        public async void DeleteUserAsync(Guid userId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("DELETE FROM Users WHERE UserId = @userId", new { userId });
        }

        public async Task<User> GetUser(string username)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return await connection.QueryFirstAsync<User>("SELECT * FROM Users WHERE UserName = @username", new { username });
        }

        public async void UpdateUserAsync(UpdateUserRequest request)
        {
            User retrievedUser = await GetUser(request.UserName);
            if (retrievedUser is not null)
            {
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                await connection.ExecuteAsync("UPDATE Users" +
                    " SET UserName = @UserName, Password = @Password  WHERE UserName = @Username", request);
            }
        }
    }
}
