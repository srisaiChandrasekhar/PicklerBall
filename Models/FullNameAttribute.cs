using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Picklr.Models
{
    public class FullNameAttribute : ValidationAttribute, IClientModelValidator
    {
        // Letters only, with optional internal apostrophes or hyphens
        private const string NamePart = @"^[A-Za-z]+(['\-][A-Za-z]+)*$";

        public FullNameAttribute()
        {
            ErrorMessage = "You must enter a first and last name using letters only.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            var input = value as string;

            // Let [Required] handle empty values
            if (string.IsNullOrWhiteSpace(input))
                return ValidationResult.Success;

            var parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Rule 1: every part must be letters only
            foreach (var part in parts)
            {
                if (!Regex.IsMatch(part, NamePart))
                    return new ValidationResult(
                        "Name can only contain letters, apostrophes, and hyphens.");
            }

            // Rule 2: both a first and a last name are required
            if (parts.Length < 2)
                return new ValidationResult(
                    "You must enter both a first and a last name.");

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-fullname"] = ErrorMessage!;
        }
    }
}