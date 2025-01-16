using stockManagement.Models.Enums;
using stockManagement.Models.Shared;
using stockManagement.Models.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockManagement.Models.Entity
{
    [ExpiryDateValidation]
    public class Product : EntityParent
    {
        [Key]
        [StringLength(20)]
        public string ProductId { get; set; } 

        [StringLength(20, ErrorMessage = "Maximum Allowed Length for ProductName is 20")]
        public string ProductName { get; set; } = string.Empty;
        [StringLength(100, ErrorMessage = "Maximum Allowed Length for Product Description is 100")]
        public string ProductDescription { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;
        public string UnitOfMesaure { get; set; } = UnitOfMesaures.Piece.ToString();

        public double Price { get; set; }
        public bool IsExpiring { get; set; }
        public DateTime ExpiryDate { get; set; } 

        public double Quantity { get; set; }

    }
}
