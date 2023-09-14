using System;
using System.Collections.Generic;
using System.Text;
using FlightTicketShop.Domain.Identity;
using FlightTicketShop.Domain.Relations;

namespace FlightTicketShop.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }
        public virtual FlightTicketApplicationUser Owner { get; set; }

        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }

    }
}
