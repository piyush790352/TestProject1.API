using DemoProject1.API.Model.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProject1.API.Model.DTO;
using TestProject1.API.Service;

namespace DemoProject1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        [HttpGet("GetUserDetails")]
        public async Task<IActionResult> GetUserDetails()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await UserDetailService.GetUserDetails();
            return Ok(response);
        }

        [HttpGet("GetUserDetailById/{Id}")]
        public async Task<IActionResult> GetUserDetailById(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await UserDetailService.GetUserDetailById(Id);
            return Ok(response);
        }

        [HttpPost("AddUserDetail")]
        public async Task<IActionResult> AddUserDetail([FromBody] AddUserDetailDTO addUserDetailRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data. Please recheck!");
            }
            var response = await UserDetailService.AddUserDetail(addUserDetailRequestDTO);
            return Ok(response);
        }
    }
}
