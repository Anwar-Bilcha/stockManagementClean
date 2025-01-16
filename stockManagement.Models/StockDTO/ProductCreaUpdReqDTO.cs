using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockManagement.Models.StockDTO
{
    public class ProductCreaUpdReqDTO
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public bool IsExpiring { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string UnitOfMesaure { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
    }
}
