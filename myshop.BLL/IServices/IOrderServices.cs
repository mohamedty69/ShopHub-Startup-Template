using myshop.BLL.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.IServices
{
    public interface IOrderServices
    {
       public Task<bool> PlaceOrderAsync(string userId, CheckOutOrderDTO checkoutInfo);
       public Task<IEnumerable<SummaryOrderDTO>> GetUserOrdersAsync(string userId);
    }
}
