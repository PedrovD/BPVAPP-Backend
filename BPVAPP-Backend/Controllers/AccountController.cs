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
using BPVAPP_Backend.Response;

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

        [HttpPost]
        [Route("register")]
        public async Task<object> CreateAccount([FromBody]RegisterModel model)
        {
            var rs = new ResponseModel();

            if(string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.UserName))
            {
                rs.Message = "Velden niet ingevuld";
                return Json(rs);
            }

            var EmailSplitStrings = model.Email.Split('@');
            if (EmailSplitStrings[1] != "student.roc-nijmegen.nl")
            {
                rs.Message = $"{model.Email} Is geen juiste school email";
                rs.StatusCode = 401;
                Response.StatusCode = 401;
                return Json(rs);
            }

            var exist = await _userManager.FindByEmailAsync(model.Email);

            if (exist != null)
            {
                rs.Message = $"{model.Email} bestaat all";
                rs.StatusCode = 401;
                Response.StatusCode = 401;
                return Json(rs);
            }

            var identity = new IdentityUser
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(identity, model.Password);

            if (result.Succeeded)
            {
                rs.Message = "Gebruiker is toegevoegd";
                return Json(rs);
            }

            rs.Message = "Registratie is mislukt";
            rs.StatusCode = 401;
            Response.StatusCode = 401;
            return Json(rs);
        }

        [HttpPost]
        [Route("login")]
        public async Task<object> Login([FromBody]LoginModel model)
        {
            var rs = new ResponseModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                rs.StatusCode = 404;
                rs.Message = "Gebruiker niet gevonden";
                Response.StatusCode = 404;
                return Json(rs);
            }

            var login = await _signInManager.PasswordSignInAsync(user, model.Password,true,false);

            if (login.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                rs.Add("authToken", await GenerateJwtToken(model.Email, appUser));
                rs.Add("userName", user.UserName);
                rs.Message = "Gebruiker gevonden";
                return Json(rs);
            }
            
            rs.Message = "Inloggen is mislukt";
            rs.StatusCode = 401;
            Response.StatusCode = 401;
            return Json(rs);
        }

        /// <summary>
        /// Generates an api token that can be used by the front-end to request data
        /// </summary>
        /// <param name="email"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [NonAction]
        private async Task<string> GenerateJwtToken(string email, IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

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