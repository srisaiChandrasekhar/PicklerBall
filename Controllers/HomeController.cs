using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Picklr.Models;

namespace Picklr.Controllers
{
    public class HomeController : Controller
    {
        private readonly PicklrContext _context;

        public HomeController(PicklrContext context)
        {
            _context = context;
        }

        public IActionResult Index(HomeViewModel vm)
        {
            // Always store the incoming filter values in session (NFLTeams pattern).
            // On the initial visit both are null; CartController redirects pass
            // them back as route data so they survive the redirect cycle.
            PicklrSession.SetFilterClubId(HttpContext.Session, vm.ClubId);
            PicklrSession.SetFilterDate(HttpContext.Session, vm.Date);

            // Default to today if no date was supplied
            if (!vm.Date.HasValue)
                vm.Date = DateTime.Today;

            // Generate today + next 6 days for the date dropdown
            vm.AvailableDates = Enumerable.Range(0, 7)
                .Select(i => DateTime.Today.AddDays(i))
                .ToList();

            // Populate clubs for the club dropdown
            vm.Clubs = _context.Clubs.OrderBy(c => c.Name).ToList();

            // Derive the Weekdays flag from the selected calendar date
            Weekdays selectedDay = vm.Date.Value.DayOfWeek switch
            {
                DayOfWeek.Monday    => Weekdays.Monday,
                DayOfWeek.Tuesday   => Weekdays.Tuesday,
                DayOfWeek.Wednesday => Weekdays.Wednesday,
                DayOfWeek.Thursday  => Weekdays.Thursday,
                DayOfWeek.Friday    => Weekdays.Friday,
                DayOfWeek.Saturday  => Weekdays.Saturday,
                DayOfWeek.Sunday    => Weekdays.Sunday,
                _                   => Weekdays.None
            };

            // Build query with eager-loaded Club
            var query = _context.Programs.Include(p => p.Club).AsQueryable();

            if (vm.ClubId.HasValue)
                query = query.Where(p => p.ClubID == vm.ClubId.Value);

            if (selectedDay != Weekdays.None)
                query = query.Where(p => (p.AvailableDays & selectedDay) == selectedDay);

            vm.Programs = query.OrderBy(p => p.Club!.Name).ThenBy(p => p.Name).ToList();
            return View(vm);
        }

        // Clears the saved filter selections and redirects to a default Index
        public IActionResult Clear()
        {
            PicklrSession.ClearFilters(HttpContext.Session);
            return RedirectToAction("Index");
        }

        public ContentResult About()   => Content("About page — under construction.");
        public ContentResult Club()    => Content("Club page — under construction.");
        public ContentResult Program() => Content("Program page — under construction.");
        public ContentResult Shop()    => Content("Shop page — under construction.");
    }
}
