using System;
using System.Collections.Generic;
using System.Text;

namespace FlightTicketShop.Domain.DTO
{
    public class DeleteFromShoppingCartDTO
    {
        public string userId { get; set; }
        public Guid id { get; set; }
    }
}
