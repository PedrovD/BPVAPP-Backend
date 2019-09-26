using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BPVAPP_Backend.Controllers
{
    [Route("api/[controller]")]
    public class TempDataController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public TempDataController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("companies")]
        public string Get()
        {
            var data = System.IO.File.ReadAllText("data.json");
            return data;
        }

        [HttpGet]
        [Route("students")]
        public string Students()
        {
            var data = System.IO.File.ReadAllText("student.json");
            return data;
        }
    }
}