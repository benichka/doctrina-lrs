using Doctrina.Domain.Identity;
using Doctrina.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Doctrina.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<DoctrinaUser> _signInManager;
        private readonly UserManager<DoctrinaUser> _userManager;

        public AuthController(SignInManager<DoctrinaUser> signInManager, UserManager<DoctrinaUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginViewModel>> Login(LoginViewModel model)
        {
            DoctrinaUser user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
                if (signInResult.Succeeded)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        [HttpGet("check")]
        public IActionResult Check()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
