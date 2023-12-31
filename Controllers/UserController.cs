﻿using DemoProject1.API.Model.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProject1.API.IService;
using TestProject1.API.Model.DTO;
using TestProject1.API.Service;
using static TestProject1.API.Model.DTO.AddMarksheetDetailDTO;

namespace DemoProject1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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

        [HttpPost("AddMarksheetDetail")]
        public async Task<IActionResult> AddMarksheetDetail([FromBody] AddMarksheetDetailDTO addMarksheetDetailDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data. Please recheck!");
            }
            var response = await _userDetailService.AddMarksheetDetail(addMarksheetDetailDTO);
            return Ok(response);
        }

        [HttpGet("GetMarksheetList/{UserId}")]
        public async Task<IActionResult> GetMarksheetList(int UserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _userDetailService.GetMarksheetList(UserId);
            return Ok(response);
        }

        [HttpGet("GetSubjectList")]
        public async Task<IActionResult> GetSubjectList()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _userDetailService.GetSubjectList();
            return Ok(response);
        }

        [HttpGet("GetGradeList")]
        public async Task<IActionResult> GetGradeList()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _userDetailService.GetGradeList();
            return Ok(response);
        }
    }
}
