namespace WebApi.Contracts
{
    public class UserRegisterCredentials
    {
        public required string Email { get; init; }

        public required string Password { get; init; }
    }
}
