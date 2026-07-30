using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Picklr.Models;

namespace Picklr.Controllers
{
    public class CartController : Controller
    {
        private readonly PicklrContext _context;

        public CartController(PicklrContext context)
        {
            _context = context;
        }

        // GET: Cart/Reserve/5?date=2026-07-15  — add program to session cart
        public IActionResult Reserve(int id, DateTime? date)
        {
            var program = _context.Programs
                .Include(p => p.Club)
                .FirstOrDefault(p => p.ProgramID == id);

            if (program == null)
                return NotFound();

            // Use the date passed from the home page; fall back to today
            DateTime reservationDate = date?.Date ?? DateTime.Today;

            var cart = PicklrSession.GetCart(HttpContext.Session);

            // Avoid duplicate entries for the same program on the same date
            if (!cart.Any(c => c.ProgramID == id && c.ReservationDate.Date == reservationDate))
            {
                cart.Add(new CartItem
                {
                    ProgramID = program.ProgramID,
                    ProgramName = program.Name,
                    ClubName = program.Club?.Name ?? string.Empty,
                    Fee = program.Fee,
                    ReservationDate = reservationDate
                });
                PicklrSession.SetCart(HttpContext.Session, cart);
                TempData["message"] = $"{program.Name} on {reservationDate:MMM d} added to your cart.";
            }
            else
            {
                TempData["message"] = $"{program.Name} on {reservationDate:MMM d} is already in your cart.";
            }

            // Pass current filter values back as route data so HomeController.Index()
            // receives and re-saves them — mirrors the NFLTeams FavoritesController pattern.
            return RedirectToAction("Index", "Home", new {
                clubId = PicklrSession.GetFilterClubId(HttpContext.Session),
                date   = PicklrSession.GetFilterDate(HttpContext.Session)?.ToString("yyyy-MM-dd")
            });
        }

        // GET: Cart/Index
        public IActionResult Index()
        {
            var cart = PicklrSession.GetCart(HttpContext.Session);
            return View(cart);
        }

        // GET: Cart/Cancel/5  — remove one item from cart
        public IActionResult Cancel(int id)
        {
            var cart = PicklrSession.GetCart(HttpContext.Session);
            var item = cart.FirstOrDefault(c => c.ProgramID == id);
            if (item != null)
            {
                cart.Remove(item);
                PicklrSession.SetCart(HttpContext.Session, cart);
                TempData["message"] = $"{item.ProgramName} removed from your cart.";
            }
            return RedirectToAction("Index");
        }

        // GET: Cart/ClearAll — empty the cart
        public IActionResult ClearAll()
        {
            PicklrSession.ClearCart(HttpContext.Session);
            TempData["message"] = "Cart cleared.";
            return RedirectToAction("Index");
        }

        // POST: Cart/Pay — save all reservations to DB, then clear cart
        [HttpPost]
        public IActionResult Pay()
        {
            var cart = PicklrSession.GetCart(HttpContext.Session);

            if (!cart.Any())
            {
                TempData["message"] = "Your cart is empty.";
                return RedirectToAction("Index");
            }

            foreach (var item in cart)
            {
                _context.Reservations.Add(new Reservation
                {
                    ProgramID = item.ProgramID,
                    UserName = "Guest",
                    Date = item.ReservationDate
                });
            }

            _context.SaveChanges();
            PicklrSession.ClearCart(HttpContext.Session);
            TempData["message"] = $"Payment confirmed! {cart.Count} reservation(s) saved.";

            // Restore filter context after payment — same NFLTeams redirect pattern.
            return RedirectToAction("Index", "Home", new {
                clubId = PicklrSession.GetFilterClubId(HttpContext.Session),
                date   = PicklrSession.GetFilterDate(HttpContext.Session)?.ToString("yyyy-MM-dd")
            });
        }
    }
}
