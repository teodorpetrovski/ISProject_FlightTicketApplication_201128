using System;
using System.Collections.Generic;
using System.Text;
using FlightTicketShop.Domain.DTO;

namespace FlightTicketShop.Services.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteProductFromShoppingCart(string userId, Guid productId);
        bool order(string userId);
    }
}
