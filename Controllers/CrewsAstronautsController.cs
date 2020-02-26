using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpaceAgenciesDatabaseApp;

namespace SpaceAgenciesDatabaseApp.Controllers
{
    public class CrewsAstronautsController : Controller
    {
        private readonly SpaceAgenciesDbContext _context;

        public CrewsAstronautsController(SpaceAgenciesDbContext context)
        {
            _context = context;
        }

        // GET: CrewsAstronauts
        public async Task<IActionResult> Index()
        {
            var spaceAgenciesDbContext = _context.CrewsAstronauts.Include(c => c.Astronaut).Include(c => c.Crew);
            return View(await spaceAgenciesDbContext.ToListAsync());
        }

        // GET: CrewsAstronauts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crewsAstronauts = await _context.CrewsAstronauts
                .Include(c => c.Astronaut)
                .Include(c => c.Crew)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crewsAstronauts == null)
            {
                return NotFound();
            }

            return View(crewsAstronauts);
        }

        // GET: CrewsAstronauts/Create
        public IActionResult Create()
        {
            ViewData["AstronautId"] = new SelectList(_context.Astronauts, "Id", "Duty");
            ViewData["CrewId"] = new SelectList(_context.Crews, "Id", "Id");
            return View();
        }

        // POST: CrewsAstronauts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CrewId,AstronautId")] CrewsAstronauts crewsAstronauts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crewsAstronauts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AstronautId"] = new SelectList(_context.Astronauts, "Id", "Duty", crewsAstronauts.AstronautId);
            ViewData["CrewId"] = new SelectList(_context.Crews, "Id", "Id", crewsAstronauts.CrewId);
            return View(crewsAstronauts);
        }

        // GET: CrewsAstronauts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crewsAstronauts = await _context.CrewsAstronauts.FindAsync(id);
            if (crewsAstronauts == null)
            {
                return NotFound();
            }
            ViewData["AstronautId"] = new SelectList(_context.Astronauts, "Id", "Duty", crewsAstronauts.AstronautId);
            ViewData["CrewId"] = new SelectList(_context.Crews, "Id", "Id", crewsAstronauts.CrewId);
            return View(crewsAstronauts);
        }

        // POST: CrewsAstronauts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CrewId,AstronautId")] CrewsAstronauts crewsAstronauts)
        {
            if (id != crewsAstronauts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crewsAstronauts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrewsAstronautsExists(crewsAstronauts.Id))
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
            ViewData["AstronautId"] = new SelectList(_context.Astronauts, "Id", "Duty", crewsAstronauts.AstronautId);
            ViewData["CrewId"] = new SelectList(_context.Crews, "Id", "Id", crewsAstronauts.CrewId);
            return View(crewsAstronauts);
        }

        // GET: CrewsAstronauts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crewsAstronauts = await _context.CrewsAstronauts
                .Include(c => c.Astronaut)
                .Include(c => c.Crew)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crewsAstronauts == null)
            {
                return NotFound();
            }

            return View(crewsAstronauts);
        }

        // POST: CrewsAstronauts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crewsAstronauts = await _context.CrewsAstronauts.FindAsync(id);
            _context.CrewsAstronauts.Remove(crewsAstronauts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrewsAstronautsExists(int id)
        {
            return _context.CrewsAstronauts.Any(e => e.Id == id);
        }
    }
}
