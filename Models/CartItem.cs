namespace Picklr.Models
{
    public class CartItem
    {
        public int ProgramID { get; set; }
        public string ProgramName { get; set; } = string.Empty;
        public string ClubName { get; set; } = string.Empty;
        public decimal Fee { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
