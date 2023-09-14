using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlightTicketShop.Domain.DomainModels;
using FlightTicketShop.Domain.DTO;
using FlightTicketShop.Domain.Relations;
using FlightTicketShop.Repository.Interface;
using FlightTicketShop.Services.Interface;

namespace FlightTicketShop.Services.Implementation
{
    public class TicketService : ITicketService
    {

        private readonly IRepository<FlightTicket> _ticketRepository;
        private readonly IRepository<TicketInShoppingCart> _ticketInShoppingCartRepository;
        private readonly IUserRepository _userRepository;


        public TicketService(IRepository<FlightTicket> ticketRepository, IRepository<TicketInShoppingCart> ticketInShoppingCartRepository, IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _ticketInShoppingCartRepository = ticketInShoppingCartRepository;
        }


        public bool AddToShoppingCart(AddToShoppingCartDto item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userShoppingCard = user.UserCart;

            if (item.SelectedTicketId != null && userShoppingCard != null)
            {
                var ticket = this.GetDetailsForTicket(item.SelectedTicketId);
                

                if (ticket != null)
                {
                    TicketInShoppingCart itemToAdd = new TicketInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        Ticket = ticket,
                        TicketId = ticket.Id,
                        UserCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    var existing = userShoppingCard.TicketInShoppingCarts.Where(z => z.ShoppingCartId == userShoppingCard.Id && z.TicketId == itemToAdd.TicketId).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.Quantity += itemToAdd.Quantity;
                        this._ticketInShoppingCartRepository.Update(existing);

                    }
                    else
                    {
                        this._ticketInShoppingCartRepository.Insert(itemToAdd);
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewTicket(FlightTicket p)
        {
            this._ticketRepository.Insert(p);
        }

        public void DeleteTicket(Guid id)
        {
            var ticket = this.GetDetailsForTicket(id);
            this._ticketRepository.Delete(ticket);
        }

        public List<FlightTicket> GetAllTickets()
        {
            return this._ticketRepository.GetAll().ToList();
        }

        public List<FlightTicket> SearchTickets(string arrivalCity, string departureCity, DateTime? departureDate)
        {
            var query = GetAllTickets().AsQueryable();

            if (!string.IsNullOrEmpty(arrivalCity))
            {
                query = query.Where(f => f.ArrivalCity == arrivalCity);
            }

            if (!string.IsNullOrEmpty(departureCity))
            {
                query = query.Where(f => f.DepartureCity == departureCity);
            }

            if (departureDate.HasValue)
            {
                query = query.Where(f => f.DepartureDateTime.Date == departureDate.Value.Date);
            }

            return query.ToList();
        }

        public FlightTicket GetDetailsForTicket(Guid? id)
        {
            return this._ticketRepository.Get(id);
        }

        public AddToShoppingCartDto GetShoppingCartInfo(Guid? id)
        {
            var ticket = this.GetDetailsForTicket(id);
            AddToShoppingCartDto model = new AddToShoppingCartDto
            {
                SelectedTicket = ticket,
                SelectedTicketId = ticket.Id,
                Quantity = 1
            };

            return model;
        }

        public void UpdateExistingTicket(FlightTicket p)
        {
            this._ticketRepository.Update(p);
        }
    }
}
