using DemoProject1.API.Model.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProject1.API.IService;
using TestProject1.API.Model.DTO;
using TestProject1.API.Service;

namespace DemoProject1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        public readonly IUserDetailService _userDetailService;
        public UserController(IUserDetailService userDetailService)
        {
            _userDetailService = userDetailService;
        }

        [HttpGet("GetUserDetails")]
        public async Task<IActionResult> GetUserDetails()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _userDetailService.GetUserDetails();
            return Ok(response);
        }

        [HttpGet("GetUserDetailById/{Id}")]
        public async Task<IActionResult> GetUserDetailById(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _userDetailService.GetUserDetailById(Id);
            return Ok(response);
        }

        [HttpPost("AddUserDetail")]
        public async Task<IActionResult> AddUserDetail([FromBody] AddUserDetailDTO addUserDetailRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data. Please recheck!");
            }
            var response = await _userDetailService.AddUserDetail(addUserDetailRequestDTO);
            return Ok(response);
        }
    }
}
