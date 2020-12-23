using Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Identity.Models;

namespace Identity.Controllers
{

    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
         private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Identity.Models.Login loginParam)
        {
            var token = _userService.Authenticate(loginParam.Username, loginParam.Password);

            if (token == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult RegisterUser([FromBody]User login)
        {
            
            _userService.RegisterUser(login);
            return Ok("registered");
        }
    }
}