using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System;
using System.Security.Claims;
using System.Text;
using FlightTicketShop.Domain.DomainModels;
using FlightTicketShop.Domain.Identity;
using GemBox.Document;
using FlightTicketShop.Services.Interface;

namespace FlightTicketShop.Web.Controllers
{
    public class OrderController : Controller
    {

        private readonly IOrderService _orderService;
        private readonly UserManager<FlightTicketApplicationUser> _userManager;

        public OrderController(IOrderService orderService)
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(this._orderService.getOrdersForUser(userId));
        }

        public IActionResult ExportOrder(Guid id)
        {


            var result = this._orderService.getOrderDetails(id);

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
