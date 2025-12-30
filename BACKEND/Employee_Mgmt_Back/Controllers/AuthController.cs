using Employee_Mgmt_Back.DTOs;
using Employee_Mgmt_Back.Services;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Mgmt_Back.Controllers
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("signup")]
        public IActionResult Signup(SignupRequest request)
        {
            var success = _auth.Signup(request);
            if (!success)  return BadRequest(new {message = "USer Already Exists!"});

                return Ok(new { message = "User Registered Successfully" });
        }


        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var result = _auth.Login(request);
            if (result == null) return Unauthorized(new { message = "Invalid Credentials" });
            return Ok(result);
        }

    }
}
