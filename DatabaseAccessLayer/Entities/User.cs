namespace DatabaseAccessLayer.Entities
{
    public class User
    {
        public int UserId { get; init; }

        public required Guid UserUid { get; init; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public required bool IsAdmin { get; set; } = false;

        public List<Car> Cars { get; set; }
    }
}
