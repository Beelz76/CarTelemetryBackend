using WebApi.Contracts;
using WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;

        public UserController(UserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<JwtToken> Register(UserRegisterCredentials credentials)
        {

            if (_userService.CheckEmail(credentials.Email))
            {
                ModelState.AddModelError("", "Email already exists");

                return BadRequest(ModelState);
            }

            var userUid = _userService.Register(credentials);

            return new JwtToken
            {
                Token = _jwtService.GenerateToken(userUid, credentials.Email, false)
            };
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<JwtToken> Login(UserLoginCredentials credentials)
        {
            var userUid = _userService.Login(credentials);

            if (userUid == null)
            {
                ModelState.AddModelError("user", "Invalid email or password");

                return BadRequest(ModelState);
            }

            return new JwtToken
            {
                Token = _jwtService.GenerateToken(userUid.Value, credentials.Email, _userService.IsAdmin(userUid.Value))
            };
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public ActionResult<UserInfo> GetUserInfo(Guid userUid)
        {
            var user = _userService.GetUserInfo(userUid);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        [HttpPut]
        [Authorize(Roles = "Admin, User")]
        public ActionResult UpdateUser(Guid userUid, UserUpdate userUpdate)
        {
            if (userUpdate.Email == null || userUpdate.Password == null)
            {
                return BadRequest();
            }

            if (_userService.GetEmail(userUid) != userUpdate.Email)
            {
                if (!_userService.CheckEmailRegex(userUpdate.Email))
                {
                    ModelState.AddModelError("", "Invalid email format");

                    return BadRequest(ModelState);
                }

                if (_userService.CheckEmail(userUpdate.Email))
                {
                    ModelState.AddModelError("", "Email already exists");

                    return BadRequest(ModelState);
                }
            }

            if (!_userService.UpdateUser(userUid, userUpdate))
            {
                ModelState.AddModelError("", "Failed to update user");

                return BadRequest(ModelState);
            }

            return Ok("User updated");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateUserAdminStatus(Guid userUid)
        {
            if (!_userService.UpdateUserAdminStatus(userUid))
            {
                ModelState.AddModelError("", "Failed to update user status");

                return BadRequest(ModelState);
            }

            return Ok("User status updated");
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser(Guid userUid)
        {
            if (!_userService.DeleteUser(userUid))
            {
                ModelState.AddModelError("", "Failed to delete user");

                return BadRequest(ModelState);
            }

            return Ok("User deleted");
        }
    }
}
