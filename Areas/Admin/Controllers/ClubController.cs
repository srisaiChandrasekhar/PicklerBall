using Microsoft.AspNetCore.Mvc;
using Picklr.Models;

namespace Picklr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClubController : Controller
    {
        private PicklrContext context;

        public ClubController(PicklrContext ctx)
        {
            context = ctx;
        }

        public IActionResult List()
        {
            var clubs = context.Clubs.OrderBy(c => c.Name).ToList();
            return View(clubs);
        }

        [HttpGet]
        public IActionResult AddEdit(int? id)
        {
            var club = (id == null)
                ? new Club()
                : context.Clubs.Find(id) ?? new Club();

            ViewBag.Action = (id == null) ? "Add" : "Edit";
            return View(club);
        }

        [HttpPost]
        public IActionResult AddEdit(Club club)
        {
            if (ModelState.IsValid)
            {
                if (club.ClubID == 0)
                    context.Clubs.Add(club);
                else
                    context.Clubs.Update(club);

                context.SaveChanges();
                TempData["message"] = $"'{club.Name}' was saved successfully.";
                return RedirectToAction("List");
            }

            ViewBag.Action = (club.ClubID == 0) ? "Add" : "Edit";
            return View(club);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var club = context.Clubs.Find(id) ?? new Club();
            return View(club);
        }

        [HttpPost]
        public IActionResult Delete(Club club)
        {
            context.Clubs.Remove(club);
            context.SaveChanges();
            TempData["message"] = $"'{club.Name}' was deleted.";
            return RedirectToAction("List");
        }
    }
}