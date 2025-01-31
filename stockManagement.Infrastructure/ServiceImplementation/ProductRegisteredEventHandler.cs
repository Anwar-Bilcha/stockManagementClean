using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using stockManagement.Application.Interfaces;
using stockManagement.Models.Entity;
using System.Threading;
using System.Threading.Tasks;


namespace stockManagement.Infrastructure.ServiceImplementation
{
    public class ProductRegisteredEventHandler : INotificationHandler<ProductRegisteredEvent>
    {
        private readonly ISmsService _smsService;

        public ProductRegisteredEventHandler(ISmsService smsService)
        {
            _smsService = smsService;
        }
        public async Task Handle(ProductRegisteredEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Sending SMS for {notification.ProductName} to {notification.OwnerPhoneNumber}");
            var message = $"'{notification.UserName.ToUpper()}' Has registered a new product '{notification.ProductName.ToUpper()}' at '{DateTime.Now.ToShortDateString()}'.";
            await _smsService.SendSmsAsync(notification.OwnerPhoneNumber, message);
            Console.WriteLine("SMS sent (or attempted). Check Twilio logs.");
        }
    }
}
