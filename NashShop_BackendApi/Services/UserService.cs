using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NashShop_BackendApi.Data.Entities;
using NashShop_BackendApi.Interfaces;
using NashShop_ViewModel;
using NashShop_ViewModel.Shared;
using NashShop_ViewModel.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, 
            RoleManager<Role> roleManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<string> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                throw new Exception($"Cannot find username {request.UserName}");
            var result = await _signInManager.PasswordSignInAsync(user, request.Password,true,true);
            if (!result.Succeeded)
            {
                throw new Exception("Password is incorrect ");
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.GivenName,user.LastName),
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim("UserId",user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(45),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)); ;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<List<UserVM>> GetAllUser()
        {
            var users = await _userManager.Users.Select(x => new UserVM()
            {
                UserName = x.UserName,
                FirstName = x.FirstName,
                Id = x.Id,
                LastName = x.LastName,
                Dob = x.Dob

            }).ToListAsync();
            return users;
        }

        public async Task<PagedResult<UserVM>> GetUsersPaging(PagingRequest request)
        {
            var query = _userManager.Users;

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserVM()
                {
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    Id = x.Id,
                    LastName = x.LastName
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<UserVM>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var user = new User()
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dob = request.Dob

            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if(result.Succeeded)
            {
                return true;
            }
            return false;

        }
    }
}
