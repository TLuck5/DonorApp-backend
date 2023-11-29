using Microsoft.AspNetCore.Mvc;
using Service_logic_layer.Models.requestmodel;
using Service_logic_layer.Services.userServices;
using System.Text.Json;

namespace VerifyEmailPass.Controllers
{
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult>Register([FromBody]UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
            {              
                return BadRequest(ModelState);
            }

            var value =await _userServices.Register(request);
            string jsonString = JsonSerializer.Serialize(value);
            return Ok(jsonString);
        }
        
        [HttpPost]
        [Route("login")]

        public async Task<IActionResult>login([FromBody]UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var value = await _userServices.Login(request);
            string jsonString = JsonSerializer.Serialize(value);
            return Ok(jsonString);
        }

      

        [HttpPost]
        [Route("verify")]

        public async Task<IActionResult>Verify([FromBody]string token)
        {
            var val = await _userServices.Verify(token);
            string jsonString = JsonSerializer.Serialize(val);
            return Ok(jsonString);
        }

        [HttpPost]
        [Route("forgot-password")]

        public async Task<IActionResult>ForgotPassword([FromBody]string email)
        {
           var value = await _userServices.Forgotpassword(email);
            return Ok(value);
        }

        [HttpPost]
        [Route("reset-password")]

        public async Task<IActionResult>ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var val =await _userServices.ResetPassword(request);
            string jsonString = JsonSerializer.Serialize(val);
            return Ok(jsonString);
        }
    }
}
