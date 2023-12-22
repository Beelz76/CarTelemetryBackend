using DatabaseAccessLayer;
using DatabaseAccessLayer.Entities;
using System.IO;
using System.Text.RegularExpressions;

namespace WebApi.Services
{
    public class CarService
    {
        private readonly TelemetryDbContext _telemetryDbContext;

        public CarService(TelemetryDbContext telemetryDbContext)
        {
            _telemetryDbContext = telemetryDbContext;
        }

        public bool CreateCar(Guid userUid, Contracts.CarInfo carInfo)
        {
            var user = _telemetryDbContext.Set<User>().SingleOrDefault(x => x.UserUid == userUid);

            if (user == null) { return false; }

            var car = new Car
            {
                CarUid = Guid.NewGuid(),
                User = user,
                Name = carInfo.Name,
                Image = carInfo.Image,
                Description = carInfo.Description,
                CurrentSpeed = carInfo.CurrentSpeed,
                MaxSpeed = carInfo.MaxSpeed,
                AverageSpeed = carInfo.AverageSpeed,
                CorneringSpeed = carInfo.CorneringSpeed,
                MaxAcceleration = carInfo.MaxAcceleration, 
                AverageAcceleration = carInfo.AverageAcceleration, 
                AverageBraking = carInfo.AverageBraking,
                EngineSpeed = carInfo.EngineSpeed, 
                EnginePower = carInfo.EnginePower, 
                MaxBraking = carInfo.MaxBraking,
                SuspensionVibrationAmplitude = carInfo.SuspensionVibrationAmplitude,
                SuspensionVibrationSpeed = carInfo.SuspensionVibrationSpeed
            };

            _telemetryDbContext.Add(car);

            return _telemetryDbContext.SaveChanges() > 0;
        }

        public List<Contracts.CarNewInfo>? GetAllCars()
        {
            var cars = _telemetryDbContext.Set<Car>().ToList();

            if (cars.Count == 0) { return null; }

            return cars.Select(car => new Contracts.CarNewInfo
            {
                CarUid = car.CarUid,
                Name = car.Name,
                Image = car.Image,
            }).ToList();
        }

        public Contracts.CarDescription? GetCarInfo(Guid carUid)
        {
            var car = _telemetryDbContext.Set<Car>().SingleOrDefault(x => x.CarUid == carUid);

            if (car == null) { return null; }

            return new Contracts.CarDescription
            {
                Name = car.Name,
                Image = car.Image,
                Description = car.Description,
            };
        }

        public Contracts.CarTelemetry? GetCarTelemetry(Guid carUid)
        {
            var car = _telemetryDbContext.Set<Car>().SingleOrDefault(x => x.CarUid == carUid);

            if (car == null) { return null; }

            return new Contracts.CarTelemetry
            {
                Name = car.Name,
                CurrentSpeed = car.CurrentSpeed,
                MaxSpeed = car.MaxSpeed,
                AverageSpeed = car.AverageSpeed,
                CorneringSpeed = car.CorneringSpeed,
                MaxAcceleration = car.MaxAcceleration,
                AverageAcceleration = car.AverageAcceleration,
                AverageBraking = car.AverageBraking,
                EngineSpeed = car.EngineSpeed,
                EnginePower = car.EnginePower,
                MaxBraking = car.MaxBraking,
                SuspensionVibrationAmplitude = car.SuspensionVibrationAmplitude,
                SuspensionVibrationSpeed = car.SuspensionVibrationSpeed
            };
        }

        public bool DeleteCar(Guid carUid)
        {
            var car = _telemetryDbContext.Set<Car>().SingleOrDefault(x => x.CarUid == carUid);

            if (car == null) { return false; }

            _telemetryDbContext.Remove(car);

            return _telemetryDbContext.SaveChanges() > 0;
        }

        public bool CheckCarName(string name)
        {
            var car = _telemetryDbContext.Set<Car>().SingleOrDefault(x => x.Name == name);

            if (car == null) { return false; }

            return true;
        }

        public bool IsCarExists(Guid carUid)
        {
            var car = _telemetryDbContext.Set<Car>().SingleOrDefault(x => x.CarUid == carUid);

            if (car == null) { return false; }

            return true;
        }

        public bool CheckRegex(string name)
        {
            var regex = new Regex(@"^[a-zA-Zа-яА-Я][a-zA-Zа-яА-Я -]{1,}$");

            if (!regex.IsMatch(name))
            {
                return false;
            }

            return true;
        }
    }
}
