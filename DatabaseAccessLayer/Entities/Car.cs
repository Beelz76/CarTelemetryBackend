namespace DatabaseAccessLayer.Entities
{
    public class Car
    {
        public int CarId { get; init; }

        public required Guid CarUid { get; init; }

        public required User User { get; init; }

        public required string Name { get; init; }

        public required string Image { get; init; }

        public required string Description { get; init; }

        public required double CurrentSpeed { get; init; }

        public required double MaxSpeed { get; init; }

        public required double AverageSpeed { get; init; }

        public required double CorneringSpeed { get; init; }

        public required double MaxAcceleration { get; init; }

        public required double AverageAcceleration { get; init; }

        public required double MaxBraking { get; init; }

        public required double AverageBraking { get; init; }

        public required double EngineSpeed { get; init; }

        public required double EnginePower { get; init; }

        public required double SuspensionVibrationAmplitude { get; init; }

        public required double SuspensionVibrationSpeed { get; init; }
    }
}
