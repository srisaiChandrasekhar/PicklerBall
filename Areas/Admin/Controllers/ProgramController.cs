using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Picklr.Models;

namespace Picklr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProgramController : Controller
    {
        private readonly PicklrContext context;

        public ProgramController(PicklrContext ctx)
        {
            context = ctx;
        }

        // GET /Admin/Program/List
        public IActionResult List()
        {
            var programs = context.Programs
                .Include(p => p.Club)
                .OrderBy(p => p.Name)
                .ToList();
            return View(programs);
        }

        // GET /Admin/Program/AddEdit        — blank form (Add)
        // GET /Admin/Program/AddEdit/2      — loads existing record (Edit)
        [HttpGet]
        public IActionResult AddEdit(int? id)
        {
            var program = (id == null)
                ? new PicklProgram()
                : context.Programs.Find(id) ?? new PicklProgram();

            ViewBag.Action = (id == null) ? "Add" : "Edit";
            ViewBag.Clubs = new SelectList(
                context.Clubs.OrderBy(c => c.Name).ToList(),
                "ClubID", "Name", program.ClubID);
            return View(program);
        }

        [HttpPost]
        public IActionResult AddEdit(PicklProgram program)
        {
            if (ModelState.IsValid)
            {
                if (program.ProgramID == 0)
                    context.Programs.Add(program);
                else
                    context.Programs.Update(program);

                context.SaveChanges();
                TempData["message"] = $"'{program.Name}' was saved successfully.";
                return RedirectToAction("List"); // PRG
            }

            ViewBag.Action = (program.ProgramID == 0) ? "Add" : "Edit";
            ViewBag.Clubs = new SelectList(
                context.Clubs.OrderBy(c => c.Name).ToList(),
                "ClubID", "Name", program.ClubID);
            return View(program);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var program = context.Programs.Find(id) ?? new PicklProgram();
            return View(program);
        }

        [HttpPost]
        public IActionResult Delete(PicklProgram program)
        {
            context.Programs.Remove(program);
            context.SaveChanges();
            TempData["message"] = $"'{program.Name}' was deleted.";
            return RedirectToAction("List"); // PRG
        }
    }
}
