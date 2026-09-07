using myshop.BLL.DTOs.Order;
using myshop.BLL.IServices;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MimeKit;
using Microsoft.Extensions.Options;
using myshop.BLL.DTOs.Email;
using Microsoft.AspNetCore.Identity;
using myshop.Entities.Models;

namespace myshop.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<StmpSettings> _options;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmailService(UserManager<ApplicationUser> userManager, IOptions<StmpSettings> options)
        {
            _options = options;
            _userManager = userManager;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            if (email == null)
                throw new ArgumentNullException("Email is not found");
            var user = await _userManager.FindByEmailAsync(email);
            var mime = new MimeMessage();
            mime.From.Add(new MailboxAddress("Mohamed Eltabey", _options.Value.SenderEmail));
            mime.To.Add(new MailboxAddress(user?.UserName?? "",email));
            mime.Subject = subject;
            var builder = new BodyBuilder
            {
                HtmlBody = body
            };
            mime.Body = builder.ToMessageBody();
            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync(_options.Value.Server, _options.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_options.Value.SenderEmail, _options.Value.Password);
            await client.SendAsync(mime);
            await client.DisconnectAsync(true);
        }

        public async Task SendOrderConfirmationEmailAsync(string email, string userName, SummaryOrderDTO order)
        {
            
            
             var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "OrderConfirmationEmail.html");
            
             if (!File.Exists(filePath)) 
                throw new FileNotFoundException("Could not find the email template.", filePath);

            var html = await File.ReadAllTextAsync(filePath);

            html = html.Replace("{{CustomerName}}", userName);
            html = html.Replace("{{OrderId}}", order.Id.ToString());
            html = html.Replace("{{OrderDate}}", order.OrderDate.ToString("dd MMM yyyy"));
            html = html.Replace("{{ShippingAddress}}", $"{order.Address}, {order.City}");
            html = html.Replace("{{OrderTrackingUrl}}", $"https://localhost:5001/Order/Track/{order.Id}"); // Example URL
            html = html.Replace("{{CurrentYear}}", DateTime.Now.Year.ToString());
            html = html.Replace("{{TotalAmount}}", order.TotalPrice.ToString("C")); // Formats as currency

            var itemsHtml = new StringBuilder();
            if (order.Items != null)
            {
                foreach (var item in order.Items)
                {
                    itemsHtml.Append($"<tr><td>{item.ProductName}</td><td>{item.Count}</td><td>{item.Price:C}</td></tr>");
                }
            }

            var loopStart = html.IndexOf("{{#each OrderItems}}");
            var loopEnd = html.IndexOf("{{/each}}") + "{{/each}}".Length;
            if (loopStart != -1 && loopEnd != -1)
            {
                var originalLoopBlock = html.Substring(loopStart, loopEnd - loopStart);
                html = html.Replace(originalLoopBlock, itemsHtml.ToString());
            }

            await SendEmailAsync(email, $"Order Confirmation #{order.Id}", html);
        }

        public async Task SendWelcomeEmailAsync(string email, string userName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "WelcomeEmail.html");
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Could not find the email template.", filePath);
            var emailBody = await File.ReadAllTextAsync(filePath);
            emailBody = emailBody.Replace("{{CustomerName}}",userName);
            emailBody = emailBody.Replace("{{ShopUrl}}", "https://localhost:7020/");
            emailBody = emailBody.Replace("{{CurrentYear}}", DateTime.Now.Year.ToString());
            await SendEmailAsync(email, $"Welcome Email to {userName}", emailBody);
        }
    }
}
