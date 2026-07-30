using System.ComponentModel.DataAnnotations;

namespace Picklr.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }

        // FK to PicklProgram
        public int ProgramID { get; set; }
        public PicklProgram? Program { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }
    }
}
