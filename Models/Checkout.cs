using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Picklr.Models
{
    public class Checkout
    {
        [Required(ErrorMessage = "Please enter your name.")]
        [StringLength(60, ErrorMessage = "Name cannot exceed 60 characters.")]
        [FullName(ErrorMessage = "Please enter both a first and last name.")]
        [Display(Name = "Full Name")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Remote("CheckEmail", "Cart",
            ErrorMessage = "No Picklr account is registered with that email.")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your phone number.")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$",
            ErrorMessage = "Phone must be in the format 999-999-9999.")]
        [Display(Name = "Phone")]
        public string Phone { get; set; } = string.Empty;
    }
}