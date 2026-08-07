using System.ComponentModel.DataAnnotations;

namespace Picklr.Models
{
    // Named AppUser to avoid future conflicts if ASP.NET Identity is added.
    public class AppUser
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "You must enter a first name.")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "First name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[A-Za-z]+(['\-][A-Za-z]+)*$",
            ErrorMessage = "First name can only contain letters, apostrophes, and hyphens.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "You must enter a last name.")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Last name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[A-Za-z]+(['\-][A-Za-z]+)*$",
            ErrorMessage = "Last name can only contain letters, apostrophes, and hyphens.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(150, ErrorMessage = "Email cannot exceed 150 characters.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a role.")]
        [Display(Name = "Role")]
        public string Role { get; set; } = "Client";

        // Computed display name for use in views
        public string FullName => $"{FirstName} {LastName}";
    }
}