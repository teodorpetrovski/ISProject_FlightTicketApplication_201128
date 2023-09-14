using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FlightTicketShop.Domain.Relations;

namespace FlightTicketShop.Domain.DomainModels
{
    public class FlightTicket : BaseEntity
    {
        [Required]
        public string DepartureCity { get; set; }
        [Required]
        public string ArrivalCity { get; set; }

        [Required]
        public string DestinationImage { get; set; }

        [Required]
        public string FlightClass { get; set; }
        [Required]
        public string FlightDescription { get; set; }
        [Required]
        public double TicketPrice { get; set; }
        [Required]
        public int FlightDuration { get; set; }

        [Required]
        public DateTime DepartureDateTime { get; set; }


        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public virtual ICollection<TicketInOrder> TicketInOrders { get; set; }

    }
}
