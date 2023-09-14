using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FlightTicketShop.Domain.Identity;

namespace FlightTicketShop.Domain.DTO
{
    public class AdminRegistrationDto
    {
        

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
       
        public string Password { get; set; }


        public string ConfirmPassword { get; set; }

        public Role Role { get; set; }
    }
}
