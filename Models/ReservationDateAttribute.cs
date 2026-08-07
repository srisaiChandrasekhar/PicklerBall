using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Picklr.Models
{
    public class ReservationDateAttribute : ValidationAttribute, IClientModelValidator
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext context)
        {
            DateTime date = (DateTime)value;
            DateTime today = DateTime.Today;

            if (date < today || date > today.AddDays(30))
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-reservationdate", ErrorMessage);
        }
    }
}