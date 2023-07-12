namespace WebAPIv1.Domain.Contracts.Users
{
    public record UpdateUserRequest(
        string UserName,
        string Password);
}
