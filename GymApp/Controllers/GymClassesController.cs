using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymApp.Data;
using GymApp.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GymApp.Controllers
{
    public class GymClassesController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public GymClassesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            db = context;
            this.userManager = userManager;
        }

        // GET: GymClasses
        public async Task<IActionResult> Index()
        {
              return db.GymClasses != null ? 
                          View(await db.GymClasses.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GymClasses'  is null.");
        }

        public async Task<IActionResult> SearchPass()
        {
            //var pass = db.GymClasses
            //   .Select(item => item.Name)
            //   .ToList();
            //var pass = from e in db.GymClasses select e.Name
            //           .ToList();
            //return View("SearchPass",pass);
            return View("SearchPass", await db.GymClasses.ToListAsync());
        }

        public async Task<IActionResult> Booking(int? id)
        {
            if (id is null) return BadRequest();

            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = userManager.GetUserId(User);
            if (userId == null) return BadRequest();
            var attending = await db.AppUserGyms.FindAsync(userId, id);
            if (attending == null)
            {
                var booking = new ApplicationUserGymClass{ApplicationUserId = userId, GymClassId = (int)id};
                db.AppUserGyms.Add(booking);
            }
            await db.SaveChangesAsync();
            TempData["Message"] = "Done ";
            return RedirectToAction("Index");
        }

            // GET: GymClasses/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.GymClasses == null)
            {
                return NotFound();
            }

            var gymClass = await db.GymClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        // GET: GymClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,Duration,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                db.Add(gymClass);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gymClass);
        }

        // GET: GymClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.GymClasses == null)
            {
                return NotFound();
            }

            var gymClass = await db.GymClasses.FindAsync(id);
            if (gymClass == null)
            {
                return NotFound();
            }
            return View(gymClass);
        }

        // POST: GymClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,Duration,Description")] GymClass gymClass)
        {
            if (id != gymClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(gymClass);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymClassExists(gymClass.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gymClass);
        }

        [Authorize]
        // GET: GymClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.GymClasses == null)
            {
                return NotFound();
            }

            var gymClass = await db.GymClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }
        
        [Authorize]
        // POST: GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.GymClasses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GymClasses'  is null.");
            }
            var gymClass = await db.GymClasses.FindAsync(id);
            if (gymClass != null)
            {
                db.GymClasses.Remove(gymClass);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymClassExists(int id)
        {
          return (db.GymClasses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
