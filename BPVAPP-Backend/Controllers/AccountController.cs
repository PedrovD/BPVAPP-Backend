using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BPVAPP_Backend.Database.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;

namespace BPVAPP_Backend.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        //JWT: https://medium.com/@ozgurgul/asp-net-core-2-0-webapi-jwt-authentication-with-identity-mysql-3698eeba6ff8
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet]
        [Route("verify")]
        public string IsAuthenticated()
        {
            return "Je hebt toegang";
        }

        [HttpGet]
        [Route("register")]
        public async Task<string> CreateAccount(RegisterModel model)
        {
            try
            {
                var identity = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await _userManager.CreateAsync(identity, model.Password);

                if (result.Succeeded)
                {
                    return await GenerateJwtToken(model.Email, identity);
                }

                return "Error failed to create user";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        [HttpGet]
        [Route("login")]
        public async Task<string> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return "User not found";

            var login = await _signInManager.PasswordSignInAsync(user, model.Password,true,false);

            if (login.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);



                return await GenerateJwtToken(model.Email, appUser);
            }

            return "Error failed to login";
        }

        private async Task<string> GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}