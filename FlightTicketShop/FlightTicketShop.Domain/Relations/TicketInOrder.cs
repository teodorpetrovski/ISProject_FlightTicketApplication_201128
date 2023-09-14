using System;
using System.Collections.Generic;
using System.Text;
using FlightTicketShop.Domain.DomainModels;

namespace FlightTicketShop.Domain.Relations
{
    public class TicketInOrder : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid TicketId { get; set; }
        public FlightTicket Ticket { get; set; }
        public int Quantity { get; set; }
    }
}
