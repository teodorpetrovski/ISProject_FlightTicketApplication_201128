using FlightTicketShop.Domain.Identity;
using FlightTicketShop.Services.Interface;
using GemBox.Document;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.AspNetCore.Identity;
using FlightTicketShop.Domain.DTO;

namespace FlightTicketShop.Web.Controllers.Api
{
    [Route("api/order")]
    [ApiController]
    public class OrderRestController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<FlightTicketApplicationUser> _userManager;

        public OrderRestController(IOrderService orderService)
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            _orderService = orderService;
        }


        [HttpPost("index")]
        public IActionResult Index([FromBody] UserDTO model)
        {
            
            return Ok(this._orderService.getOrdersForUser(model.userId));
        }


        [HttpPost("exportorder")]
        public IActionResult ExportOrder([FromBody] ExportOrderDTO model)
        {


            var result = this._orderService.getOrderDetails(model.id);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "template.docx");

            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{OrderNumber}}", result.Id.ToString());
            document.Content.Replace("{{CostumerEmail}}", result.User.Email);
            document.Content.Replace("{{CostumerInfo}}", (result.User.FirstName + " " + result.User.LastName));

            StringBuilder sb = new StringBuilder();

            var total = 0.0;

            foreach (var item in result.TicketInOrders)
            {
                total += item.Quantity * item.Ticket.TicketPrice;
                sb.AppendLine(item.Ticket.DepartureCity + "-" + item.Ticket.ArrivalCity + " with quantity of: " + item.Quantity + " and price of: $" + item.Ticket.TicketPrice);
            }

            document.Content.Replace("{{AllProducts}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", "$" + total.ToString());

            var stream = new MemoryStream();

            document.Save(stream, new PdfSaveOptions());


            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "Invoice.pdf");
        }
    }
}
