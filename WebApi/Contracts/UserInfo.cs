namespace WebApi.Contracts
{
    public class UserInfo
    {
        public required string Password { get; init; }

        public required string Email { get; init; }
    }
}
