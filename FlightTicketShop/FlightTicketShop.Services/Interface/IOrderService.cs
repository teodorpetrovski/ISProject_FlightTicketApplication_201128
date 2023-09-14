using System;
using System.Collections.Generic;
using System.Text;
using FlightTicketShop.Domain.DomainModels;
using FlightTicketShop.Domain;

namespace FlightTicketShop.Services.Interface
{
    public interface IOrderService
    {
        public List<Order> getAllOrders();
        public List<Order> getOrdersForUser(string userId);
        public Order getOrderDetails(Guid Id);
    }
}
