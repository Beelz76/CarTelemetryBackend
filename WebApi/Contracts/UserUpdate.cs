namespace WebApi.Contracts
{
    public class UserUpdate
    {
        public required string Password { get; init; }

        public required string Email { get; init; } 
    }
}
