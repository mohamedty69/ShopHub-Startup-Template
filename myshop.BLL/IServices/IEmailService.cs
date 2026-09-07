using myshop.BLL.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.IServices
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string email,string subject ,string body);
        public Task SendWelcomeEmailAsync (string email,string userName);
        public Task SendOrderConfirmationEmailAsync(string email, string userName, SummaryOrderDTO order);
    }
}
