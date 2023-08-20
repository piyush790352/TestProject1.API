using DemoProject1.API.Model.Domain;
using DemoProject1.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProject1.API.Model.DTO;

namespace DemoProject1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("GetUserDetails")]
        public async Task<IActionResult> GetUserDetails()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await UserRepository.GetUserDetails();
            return Ok(response);
        }

        [HttpGet("GetUserDetailById/{Id}")]
        public async Task<IActionResult> GetUserDetailById(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await UserRepository.GetUserDetailById(Id);
            return Ok(response);
        }

        [HttpPost("AddUserDetail")]
        public async Task<IActionResult> AddUserDetail([FromBody] AddUserDetailDTO addUserDetailRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data. Please recheck!");
            }
            var response = await UserRepository.AddUserDetail(addUserDetailRequestDTO);
            return Ok(response);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserDTO addUserRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data. Please recheck!");
            }
            var response = await UserRepository.AddUser(addUserRequestDTO);
            return Ok(response);
        }
    }
}
