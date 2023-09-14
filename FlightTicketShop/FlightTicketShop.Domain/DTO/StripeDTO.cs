using System;
using System.Collections.Generic;
using System.Text;

namespace FlightTicketShop.Domain.DTO
{
    public class StripeDTO
    {

        public string stripeToken { get; set; }
        public string userId { get; set; }
    }
}
