namespace WebAPIv1.Domain.Contracts.Users
{
    public record CreateUserRequest(
        string UserName,
        string Password);
}
