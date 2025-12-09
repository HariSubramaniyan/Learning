using Learning.Data.DTO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Learning.Repo.Iservices;

namespace Learning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager;
        private readonly ITokenRepo tokenRepo;
        public AuthController(Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager,ITokenRepo tokenRepo) {

            this.userManager = userManager;
            this.tokenRepo = tokenRepo;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegDTO RegReq)
        {
            var identityUser = new IdentityUser
            {
                UserName = RegReq.UserName,
                Email = RegReq.UserName,
            };
            var result = await  userManager.CreateAsync(identityUser, RegReq.Password);

            if(identityUser != null && result.Succeeded)
            {
                return Ok("User Registered");
            }
            else
            {
                return BadRequest("User Registration Failed");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO obj)
        {
            var user = await userManager.FindByEmailAsync(obj.UserName);

            if (user != null)
            {
                var res = await userManager.CheckPasswordAsync(user, obj.Password);

                if(res)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                        // Generate JWT Token
                       var jwt =  tokenRepo.CreateJWTToken(user, new List<string>());
                        return Ok(jwt);

                    }
                    return BadRequest();

                    
                }
            }
            return BadRequest("UserName or Password INcorrect");


        }
    }
}
