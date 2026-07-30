namespace Picklr.Models
{
    public static class PicklrSession
    {
        private const string CartKey    = "cart";
        private const string CountKey   = "cartcount";
        private const string ClubIdKey  = "filterClubId";
        private const string DateKey    = "filterDate";

        public static void SetCart(ISession session, List<CartItem> cart)
        {
            session.SetObject<List<CartItem>>(CartKey, cart);
            session.SetInt32(CountKey, cart.Count);
        }

        public static List<CartItem> GetCart(ISession session)
        {
            return session.GetObject<List<CartItem>>(CartKey) ?? new List<CartItem>();
        }

        public static int GetCartCount(ISession session)
        {
            return session.GetInt32(CountKey) ?? 0;
        }

        public static void ClearCart(ISession session)
        {
            session.Remove(CartKey);
            session.Remove(CountKey);
        }

        // ── Filter state (ClubId + Date) ──────────────────────────────────

        public static void SetFilterClubId(ISession session, int? clubId)
        {
            if (clubId.HasValue)
                session.SetInt32(ClubIdKey, clubId.Value);
            else
                session.Remove(ClubIdKey);
        }

        public static int? GetFilterClubId(ISession session) =>
            session.GetInt32(ClubIdKey);

        public static void SetFilterDate(ISession session, DateTime? date) =>
            session.SetString(DateKey, date?.ToString("yyyy-MM-dd") ?? string.Empty);

        public static DateTime? GetFilterDate(ISession session)
        {
            string? s = session.GetString(DateKey);
            return string.IsNullOrEmpty(s) ? null
                   : DateTime.TryParse(s, out DateTime d) ? d : (DateTime?)null;
        }

        public static void ClearFilters(ISession session)
        {
            session.Remove(ClubIdKey);
            session.Remove(DateKey);
        }
    }
}
