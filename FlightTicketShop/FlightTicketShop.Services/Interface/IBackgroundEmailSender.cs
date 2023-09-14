using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketShop.Services.Interface
{
    public interface IBackgroundEmailSender
    {
        Task DoWork();
    }
}
