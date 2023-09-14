using System;
using System.Collections.Generic;
using System.Text;
using FlightTicketShop.Domain.Identity;
using FlightTicketShop.Domain.Relations;

namespace FlightTicketShop.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public FlightTicketApplicationUser User { get; set; }

        public virtual ICollection<TicketInOrder> TicketInOrders { get; set; }
    }
}
