using FlightTicketShop.Domain.DomainModels;
using FlightTicketShop.Domain.DTO;
using FlightTicketShop.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlightTicketShop.Web.Controllers.Api
{
    [Route("api/account")]
    [ApiController]
    public class AccountRestController : ControllerBase
    {

        private readonly UserManager<FlightTicketApplicationUser> userManager;
        private readonly SignInManager<FlightTicketApplicationUser> signInManager;
        public AccountRestController(UserManager<FlightTicketApplicationUser> userManager, SignInManager<FlightTicketApplicationUser> signInManager)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
        }

       

        [HttpPost("register"), AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationDto request)
        {
          
                var userCheck = await userManager.FindByEmailAsync(request.Email);
                if (userCheck == null)
                {
                    var user = new FlightTicketApplicationUser
                    {
                        FirstName = request.Name,
                        LastName = request.LastName,
                        UserName = request.Email,
                        NormalizedUserName = request.Email,
                        Email = request.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        PhoneNumber = request.PhoneNumber,
                        UserCart = new ShoppingCart(),
                        Role = Role.STANDARD
                    };
                    var result = await userManager.CreateAsync(user, request.Password);
                    if (result.Succeeded)
                    {
                        return Ok("Registration Succesfull");
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return BadRequest("Registration unsuccsefull");
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Email already exists.");
                    return BadRequest("Email already exists");
                }
            
            

        }

       

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return BadRequest("Email not confirmed yet");

                }
                if (await userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return BadRequest("Invalid credentials");

            }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                    string userId = this.userManager.FindByEmailAsync(model.Email).Result.Id;
                    var userDetails=new {userId=userId};
                    return Ok(userDetails);
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return BadRequest("Invalid login attempt");
                }
            
           
        }


        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok("Log Out Successfull");
        }
    }
}
