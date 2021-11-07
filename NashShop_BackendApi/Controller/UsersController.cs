using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NashShop_BackendApi.Interfaces;
using NashShop_ViewModel;
using NashShop_ViewModel.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Authenticate(request);
            if (string.IsNullOrEmpty(result.Token))
            {
                return BadRequest("Username or password is incorrect");
            }

            return Ok(result.Token);
        }
        [HttpPost("admin/login")]
        [AllowAnonymous]
        public async Task<IActionResult> AdminAuthenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Authenticate(request);

            if (string.IsNullOrEmpty(result.Token))
            {
                return BadRequest("Username or password is incorrect");
            }
            if(!result.IsAdmin)
                return BadRequest("You must be admin");
            return Ok(result.Token);
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Register(request);
          
            return Ok(result);
        }
        [HttpPost("paging")]
        public async Task<IActionResult> GetUsersPaging([FromQuery] PagingRequest request)
        {
            var users = await _userService.GetUsersPaging(request);
            return Ok(users);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userService.GetAllUser();
            return Ok(users);

        }
    }
}
