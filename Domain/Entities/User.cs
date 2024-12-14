namespace Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public required string PasswordHash { get; set; }
    }
}
