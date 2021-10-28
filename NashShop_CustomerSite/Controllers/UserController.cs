using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using NashShop_CustomerSite.ApiClient;
using NashShop_ViewModel;
using NashShop_ViewModel.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;

        public UserController(IUserApiClient userApiClient, IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            var sessions = HttpContext.Session.GetString("Token");
            var request = new PagingRequest()
            {
                Bearer = sessions,
                PageIndex = 0,
                PageSize = 10
            };
            var users =await _userApiClient.GetUsersPaging(request);
            return View(users);

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {

            if (!ModelState.IsValid)
                return View(request);

            var token = await _userApiClient.Authenticate(request);
            var principal = this.ValidateToken(token);
            var auth = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                //IsPersistent = true
            };
            HttpContext.Session.SetString("Token", token);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, principal, auth);

            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(registerRequest);
            }

            var result = await _userApiClient.Register(registerRequest);
            if (!result)
            {
                ModelState.AddModelError("", "Đăng ký thất bại ");
                return View();
            }
            var token = await _userApiClient.Authenticate(new LoginRequest()
            {
                UserName = registerRequest.UserName,
                Password = registerRequest.Password,
            });

            var userPrincipal = this.ValidateToken(token);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = false
            };
            //HttpContext.Session.SetString(SystemConstants.AppSettings.Token, loginResult.ResultObj);
            await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        userPrincipal,
                        authProperties);

            return RedirectToAction("Index", "Home");
        }
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Jwt:Issuer"];
            validationParameters.ValidIssuer = _configuration["Jwt:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }
    }
}
