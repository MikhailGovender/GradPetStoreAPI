using WebAPIv1.Domain.Contracts.Users;
using WebAPIv1.Domain.Models;

namespace WebAPIv1.Services.Users
{
    public interface IUserService
    {
        void CreateUser(User newUser);
        void DeleteUserAsync(Guid userId);
        Task<User> GetUser(string username);
        void UpdateUserAsync(UpdateUserRequest request);
    }
}

