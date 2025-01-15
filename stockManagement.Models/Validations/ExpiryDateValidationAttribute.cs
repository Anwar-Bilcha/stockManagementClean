using stockManagement.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockManagement.Models.Validations
{
    public class ExpiryDateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var product = (Product)validationContext.ObjectInstance;

            // Validation logic
            if (validationContext.ObjectInstance is not Product prod)
            {
                return new ValidationResult($"Invalid object type for validation.{validationContext.ObjectInstance.GetType().Name}");
            }
            if (product.IsExpiring && product.ExpiryDate == default)
            {
                return new ValidationResult("ExpiryDate must be set if IsExpiring is true.");
            }

            if (!product.IsExpiring && product.ExpiryDate != default)
            {
                return new ValidationResult("ExpiryDate must not have a value if IsExpiring is false.");
            }

            return ValidationResult.Success;
        }

    }
}
