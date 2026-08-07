using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Picklr.Models
{
    public class FullNameAttribute : ValidationAttribute, IClientModelValidator
    {
        protected override ValidationResult? IsValid(
            object? value, ValidationContext context)
        {
            string name = value as string ?? "";

            string[] parts = name.Trim().Split(' ',
                StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-fullname", ErrorMessage ?? "");
        }
    }
}