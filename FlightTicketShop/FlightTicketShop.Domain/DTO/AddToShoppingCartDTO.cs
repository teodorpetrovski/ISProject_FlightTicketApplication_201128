using System;
using System.Collections.Generic;
using System.Text;
using FlightTicketShop.Domain.DomainModels;

namespace FlightTicketShop.Domain.DTO
{
    public class AddToShoppingCartDto
    {
        public string userId { get; set; }
        public FlightTicket SelectedTicket { get; set; }
        public Guid SelectedTicketId { get; set; }
        public int Quantity { get; set; }
    }
}
