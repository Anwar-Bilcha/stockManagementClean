using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using stockManagement.Application.Interfaces;
using stockManagement.Models.Entity;

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
            var message = $"New product '{notification.ProductName}' has been registered.";
            await _smsService.SendSmsAsync(notification.OwnerPhoneNumber, message);
        }
    }
}
