using System;
using System.Collections.Generic;
using System.Text;
using FlightTicketShop.Domain.DomainModels;
using FlightTicketShop.Domain;
using FlightTicketShop.Repository.Interface;
using FlightTicketShop.Services.Interface;
using System.Linq;

namespace FlightTicketShop.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<Order> getAllOrders()
        {
            return this._orderRepository.getAllOrders();
        }

        public Order getOrderDetails(Guid Id)
        {
            return this._orderRepository.getOrderDetails(Id);
        }

        public List<Order> getOrdersForUser(string userId)
        {
            return this._orderRepository.getAllOrders().Where(z => z.UserId == userId).ToList();
        }
    }
}
