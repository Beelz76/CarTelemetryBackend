namespace WebApi.Contracts
{
    public class User
    {
        public required Guid UserUid { get; init; }  

        public required string Password { get; init; }

        public required string Email { get; init; }
    }
}
