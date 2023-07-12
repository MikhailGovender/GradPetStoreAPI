namespace WebAPIv1.Domain.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CreatedAt { get; set; }

        public User(Guid userId, string userName, string password, string createdAt)
        {
            UserId = userId;
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            CreatedAt = createdAt;
        }
    }
}
