using WebApi.Contracts;
using WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public ActionResult CreateCar(CarInfo carInfo)
        {
            Guid userUid = Guid.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);

            if (!_carService.CreateCar(userUid, carInfo))
            {
                ModelState.AddModelError("", "Failed to create car");

                return BadRequest(ModelState);
            }

            return Ok("Car created");
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public ActionResult<List<CarNewInfo>> GetAllCars()
        {
            var cars = _carService.GetAllCars();

            if (cars == null)
            {
                return NotFound("No cars found");
            }

            return Ok(cars);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public ActionResult<CarDescription> GetCarInfo(Guid carUid)
        {
            var car = _carService.GetCarInfo(carUid);

            if (car == null)
            {
                return NotFound("Car not found");
            }

            return Ok(car);
        }


        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public ActionResult<CarTelemetry> GetCarTelemetry(Guid carUid)
        {
            var car = _carService.GetCarTelemetry(carUid);

            if (car == null)
            {
                return NotFound("Car not found");
            }

            return Ok(car);
        }


        [HttpDelete]
        [Authorize(Roles = "Admin, User")]
        public ActionResult DeleteCar(Guid carUid)
        {
            if (!_carService.DeleteCar(carUid))
            {
                ModelState.AddModelError("", "Failed to delete car");

                return BadRequest(ModelState);
            }
             
            return Ok("Car deleted");
        }
    }
}
