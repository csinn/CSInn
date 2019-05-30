using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CSInn.Presentation.Blazor.Pages
{
    [Route("auth")]
    public class AuthController : Controller
    {
        [Route("login"), HttpGet]
        public IActionResult Login(string redirectUrl = "/")
        {
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl });
        }

        [Route("logout"), HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }

            return RedirectToPage("/Index");
        }
    }
}