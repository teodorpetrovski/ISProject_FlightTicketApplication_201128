using System;
using System.Collections.Generic;
using System.Text;
using FlightTicketShop.Domain.DomainModels;
using FlightTicketShop.Domain.DTO;

namespace FlightTicketShop.Services.Interface
{
    public interface ITicketService
    {
        List<FlightTicket> GetAllTickets();

       List<FlightTicket> SearchTickets(string arrivalCity, string departureCity, DateTime? departureDate);

     
        FlightTicket GetDetailsForTicket(Guid? id);
        void CreateNewTicket(FlightTicket p);
        void UpdateExistingTicket(FlightTicket p);
        AddToShoppingCartDto GetShoppingCartInfo(Guid? id);
        void DeleteTicket(Guid id);
        bool AddToShoppingCart(AddToShoppingCartDto item, string userID);
    }
}
