namespace Picklr.Models
{
    public class HomeViewModel
    {
        // Filter parameters — bound from the GET query string
        public int? ClubId { get; set; }
        public DateTime? Date { get; set; }

        // Next 7 dates for the date dropdown (populated by controller)
        public List<DateTime> AvailableDates { get; set; } = new();

        // Data for the club dropdown (populated by controller)
        public List<Club> Clubs { get; set; } = new();

        // Query results (populated by controller)
        public List<PicklProgram> Programs { get; set; } = new();
    }
}
