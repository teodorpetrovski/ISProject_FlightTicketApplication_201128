using System;
using System.Collections.Generic;
using System.Text;

namespace FlightTicketShop.Domain.DTO
{
    public class SearchDTO
    {
        public DateTime? date { get; set; }

        public string departure { get; set; }

        public string arrival { get; set; }
    }
}
