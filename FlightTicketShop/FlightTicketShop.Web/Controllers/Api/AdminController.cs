using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightTicketShop.Domain.DomainModels;
using FlightTicketShop.Domain.DTO;
using FlightTicketShop.Domain.Identity;
using FlightTicketShop.Repository.Interface;
using FlightTicketShop.Services.Interface;

namespace FlightTicketShop.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly UserManager<FlightTicketApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        
        public AdminController(ITicketService ticketService, UserManager<FlightTicketApplicationUser> userManager, Repository.Interface.IUserRepository userRepository)
        {
            _ticketService = ticketService;
            _userManager = userManager;
            _userRepository = userRepository;
            
        }


        [HttpGet("[action]")]
        public List<FlightTicket> GetTickets(string? genre)
        {
           
            return this._ticketService.GetAllTickets();

        }



        [HttpGet("[action]")]
        public List<AdminRegistrationDto> GetUsers()
        {

            var users= this._userRepository.GetAll();

            return users.Select(user => new AdminRegistrationDto 
            { Email=user.Email.ToString(),
              Role=user.Role
            }).ToList();
        }


        [HttpPost("[action]")]
        public async Task<bool> ChangeRoleAsync(AdminRegistrationDto model)
        {
            var userCheck = _userManager.FindByEmailAsync(model.Email).Result;
            if(userCheck==null)
                return false;
            userCheck.Role = model.Role;
            var result = await _userManager.UpdateAsync(userCheck);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        [HttpPost("[action]")]
        public bool ImportAllUsers(List<AdminRegistrationDto> model)
        {
            bool status = true;
            foreach (var item in model)
            {
                var userCheck = _userManager.FindByEmailAsync(item.Email).Result;
                if (userCheck == null)
                {
                    var user = new FlightTicketApplicationUser
                    {
                        
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        Role = item.Role,
                        UserCart = new ShoppingCart()
                    };
                    var result = _userManager.CreateAsync(user, item.Password).Result;

                    status = status & result.Succeeded;
                }
                else
                {
                    continue;
                }
            }

            return status;
        }

        


    }
}
