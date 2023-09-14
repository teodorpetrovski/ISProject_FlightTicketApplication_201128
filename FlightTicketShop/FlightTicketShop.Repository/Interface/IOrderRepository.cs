using System;
using System.Collections.Generic;
using System.Text;
using FlightTicketShop.Domain.DomainModels;
using FlightTicketShop.Domain;

namespace FlightTicketShop.Repository.Interface
{
    public interface IOrderRepository
    {
        public List<Order> getAllOrders();
        public Order getOrderDetails(Guid Id);

    }
}
