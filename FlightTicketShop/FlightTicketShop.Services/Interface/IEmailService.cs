using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FlightTicketShop.Domain.DomainModels;

namespace FlightTicketShop.Services.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(List<EmailMessage> allMails);
    }
}
