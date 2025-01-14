using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockManagement.Models.StockDTO
{
    public class ProductResponseDTO
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public double Price { get; set; }

    }
}
