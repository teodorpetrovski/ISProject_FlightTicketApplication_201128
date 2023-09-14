
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using FlightTicketShop.Domain.DomainModels;

namespace FlightTicketShop.Domain.Identity
{

   public enum Role
    {
        STANDARD,ADMIN
    }

    public class FlightTicketApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public Role Role { get; set; }

        public virtual ShoppingCart UserCart { get; set; }
    }
}
