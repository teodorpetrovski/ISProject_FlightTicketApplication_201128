using System;
using System.Collections.Generic;
using System.Text;
using FlightTicketShop.Domain.Relations;

namespace FlightTicketShop.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<TicketInShoppingCart> Tickets { get; set; }

        public double TotalPrice { get; set; }
    }
}
