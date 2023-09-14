using System;
using System.Collections.Generic;
using System.Text;
using FlightTicketShop.Domain.Identity;

namespace FlightTicketShop.Repository.Interface
{
    public interface IUserRepository
    {
        List<FlightTicketApplicationUser> GetAll();
        FlightTicketApplicationUser Get(string id);
        void Insert(FlightTicketApplicationUser entity);
        void Update(FlightTicketApplicationUser entity);
        void Delete(FlightTicketApplicationUser entity);
    }
}
