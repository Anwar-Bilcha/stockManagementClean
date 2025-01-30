using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace stockManagement.Models.Entity
{

    public class ProductRegisteredEvent : INotification
    {
        public string ProductName { get; }
        public string OwnerPhoneNumber { get; }

        public ProductRegisteredEvent(string productName, string ownerPhoneNumber)
        {
            ProductName = productName;
            OwnerPhoneNumber = ownerPhoneNumber;
        }
    }
}
