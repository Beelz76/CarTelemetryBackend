namespace WebApi.Contracts
{
    public class CarNewInfo
    {
        public Guid CarUid { get; init; }

        public required string Name { get; init; }

        public required string Image { get; init; }

    }
}
