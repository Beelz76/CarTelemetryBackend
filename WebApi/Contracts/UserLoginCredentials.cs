namespace WebApi.Contracts
{
    public class UserLoginCredentials
    {
        public required string Email { get; init; }

        public required string Password { get; init; }
    }
}
