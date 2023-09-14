using System;
using System.Collections.Generic;
using System.Text;
using FlightTicketShop.Domain.DomainModels;

namespace FlightTicketShop.Domain.Relations
{
    public class TicketInShoppingCart : BaseEntity
    {
        public Guid TicketId { get; set; }
        public virtual FlightTicket Ticket { get; set; }

        public Guid ShoppingCartId { get; set; }
        public virtual ShoppingCart UserCart { get; set; }

        public int Quantity { get; set; }
    }
}
