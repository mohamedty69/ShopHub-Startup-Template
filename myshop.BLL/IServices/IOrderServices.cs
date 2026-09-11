using myshop.BLL.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.IServices
{
    public interface IOrderServices
    {
       public Task<int> PlaceOrderAsync(string userId, CheckOutOrderDTO checkoutInfo);
       public Task<SummaryOrderDTO> GetUserOrdersAsync(int orderId);
        public Task<IEnumerable<SummaryOrderDTO>> GetAllUsersOrder(string userId);
    }
}
