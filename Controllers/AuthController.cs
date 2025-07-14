using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {

            var identityUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, request.Password);


            if (identityResult.Succeeded)
            {
                if (request.Roles != null && request.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, request.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok(new { message = "User registered successfully" });
                    }

                }
            }

            return BadRequest(new { message = "User registration failed", errors = identityResult.Errors.Select(e => e.Description) });
        }

        // POST: api/auth/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await userManager.FindByEmailAsync(request.Username);

            if (user != null)
            {
                var passwordCheck = await userManager.CheckPasswordAsync(user, request.Password);
                if (passwordCheck)
                {
                    // create a token or session here
                    return Ok(new { message = "Login successful" });
                }
            }

            return BadRequest(new { message = "Invalid username or password" });
         }
    }
}
