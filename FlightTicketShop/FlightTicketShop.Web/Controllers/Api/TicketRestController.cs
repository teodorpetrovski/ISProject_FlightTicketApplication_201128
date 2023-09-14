using FlightTicketShop.Domain.DomainModels;
using FlightTicketShop.Domain.DTO;
using FlightTicketShop.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace FlightTicketShop.Web.Controllers.Api
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketRestController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        

        public TicketRestController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("[action]")]
        public List<FlightTicket> Index([FromBody]SearchDTO? search)
        {
            if (search != null)
                return this._ticketService.SearchTickets(search.arrival, search.departure, search.date);

            return this._ticketService.GetAllTickets();
        }

        // GET: Products/Details/5
        [HttpGet("details/{id}")]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("Ticket ID is required.");
            }

            var ticket = this._ticketService.GetDetailsForTicket(id);
            if (ticket == null)
            {
                return NotFound("Ticket not found.");
            }

            return Ok(ticket);
        }



        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        public IActionResult Create([FromBody] FlightTicket ticket)
        {
            if (ticket == null)
            {
                return BadRequest("Ticket data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ticket.Id = Guid.NewGuid();
            _ticketService.CreateNewTicket(ticket);

            return Ok(ticket);
        }

        // GET: Products/Edit/5
        [HttpGet("find/{id}")]
        public IActionResult FindById(Guid id)
        {
            var ticket = _ticketService.GetDetailsForTicket(id);
            if (ticket == null)
            {
                return NotFound("Ticket not found.");
            }

            return Ok(ticket);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("edit/{id}")]
        public IActionResult Edit([FromBody] FlightTicket ticket)
        {
            

            try
            {
                _ticketService.UpdateExistingTicket(ticket);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_ticketService.GetDetailsForTicket(ticket.Id) == null)
                {
                    return NotFound("Ticket not found.");
                }
                else
                {
                    throw;
                }
            }

            return Ok(ticket);
        }




        






        // POST: Products/Delete/5
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            _ticketService.DeleteTicket(new Guid(id));

            return Ok("Ticket deleted successfully.");
        }



        [HttpGet("cart/{id}")]
        public IActionResult AddTicketToCart(Guid id)
        {
            var result = this._ticketService.GetShoppingCartInfo(id);
            return Ok(result);
        }


        [HttpPost("addtocart")]
        public IActionResult AddTicketToCart([FromBody] AddToShoppingCartDto model)
        {
            


            var result = _ticketService.AddToShoppingCart(model, model.userId);

            if (result)
            {
                return Ok("Ticket added to cart successfully.");
            }
            return BadRequest("Failed to add ticket to cart.");
        }


        

        [HttpGet("productexists/{id}")]
        private bool ProductExists(Guid id)
        {
            return this._ticketService.GetDetailsForTicket(id) != null;
        }
    }
}
