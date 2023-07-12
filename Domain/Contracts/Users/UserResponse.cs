namespace WebAPIv1.Domain.Contracts.Users
{
    public record UserResponse(
        Guid UserId,
        string UserName,
        string Password,
        string CreatedAt);
}
