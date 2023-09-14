using FlightTicketShop.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Claims;
using System;
using System.IO;
using System.Threading.Tasks;
using FlightTicketShop.Domain.DTO;

namespace FlightTicketShop.Web.Controllers.Api
{
    [Route("api/shoppingcart")]
    [ApiController]
    public class ShoppingCartRestController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartRestController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpPost("index")]
        public IActionResult Index([FromBody] UserDTO model)
        {
            
                
            
            return Ok(this._shoppingCartService.getShoppingCartInfo(model.userId));

        }

        [HttpPost("delete/{id}")]
        public IActionResult DeleteFromShoppingCart([FromBody] DeleteFromShoppingCartDTO model)
        {
           

            var result = _shoppingCartService.deleteProductFromShoppingCart(model.userId, model.id);

            if (result)
            {
                return Ok("Product deleted from shopping cart.");
            }
            else
            {
                return NotFound("Product not found in shopping cart.");
            }
        }


       
        public Boolean Order(string userId)
        {
            

            var result = this._shoppingCartService.order(userId);
            return result;
        }


        [HttpPost("payorder")]
        public IActionResult PayOrder([FromBody] StripeDTO model)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = this._shoppingCartService.getShoppingCartInfo(model.userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
               
                Source = model.stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Description = "Ticket Shop Application Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                var result = this.Order(model.userId);

                if (result)
                {
                    return Ok("Orded has been successfully payed");
                }
                else
                {
                    return BadRequest("Order payment is unsuccessfull");
                }
            }

            return BadRequest("Order payment is unsuccessfull");
        }
    }
}
