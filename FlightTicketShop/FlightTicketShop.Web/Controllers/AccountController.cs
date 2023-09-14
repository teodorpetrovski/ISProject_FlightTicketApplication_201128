﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FlightTicketShop.Domain.DomainModels;
using FlightTicketShop.Domain.DTO;
using FlightTicketShop.Domain.Identity;

namespace FlightTicketShop.Web.Controllers
{
    
        public class AccountController : Controller
        {
            private readonly UserManager<FlightTicketApplicationUser> userManager;
            private readonly SignInManager<FlightTicketApplicationUser> signInManager;
            public AccountController(UserManager<FlightTicketApplicationUser> userManager, SignInManager<FlightTicketApplicationUser> signInManager)
            {

                this.userManager = userManager;
                this.signInManager = signInManager;
            }

            public IActionResult Register()
            {
                UserRegistrationDto model = new UserRegistrationDto();
                return View(model);
            }

            [HttpPost, AllowAnonymous]
            public async Task<IActionResult> Register(UserRegistrationDto request)
            {
                if (ModelState.IsValid)
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
                            return RedirectToAction("Login");
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
                            return View(request);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("message", "Email already exists.");
                        return View(request);
                    }
                }
                return View(request);

            }

            [HttpGet]
            [AllowAnonymous]
            public IActionResult Login()
            {
                UserLoginDto model = new UserLoginDto();
                return View(model);
            }

            [HttpPost]
            [AllowAnonymous]
            public async Task<IActionResult> Login(UserLoginDto model)
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    if (user != null && !user.EmailConfirmed)
                    {
                        ModelState.AddModelError("message", "Email not confirmed yet");
                        return View(model);

                    }
                    if (await userManager.CheckPasswordAsync(user, model.Password) == false)
                    {
                        ModelState.AddModelError("message", "Invalid credentials");
                        return View(model);

                    }

                    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                    if (result.Succeeded)
                    {
                        await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("message", "Invalid login attempt");
                        return View(model);
                    }
                }
                return View(model);
            }


            public async Task<IActionResult> Logout()
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("Login", "Account");
            }
        }
    }
