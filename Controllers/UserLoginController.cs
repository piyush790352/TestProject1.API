using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProject1.API.Model.DTO;
using TestProject1.API.Repository;
using TestProject1.API.Service;

namespace TestProject1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        UserLoginService userLoginService = new UserLoginService();


        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody] LoginRequestDTO loginRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await userLoginService.UserLogin(loginRequestDTO);
            return Ok(response);
        }
    }
}
