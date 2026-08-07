using System.ComponentModel.DataAnnotations;

namespace Picklr.Models
{
    public class Club
    {
        [Key]
        public int ClubID { get; set; }

        [Required(ErrorMessage = "Please enter a club name.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Club name must be between 3 and 100 characters.")]
        [RegularExpression(@"^[A-Za-z0-9 '\-&.]+$",
            ErrorMessage = "Club name can only contain letters, numbers, spaces, and basic punctuation.")]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a location.")]
        [StringLength(150, ErrorMessage = "Location cannot exceed 150 characters.")]
        [Display(Name = "Location")]
        public string Location { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;
    }
}