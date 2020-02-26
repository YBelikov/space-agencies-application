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
    public class MissionsController : Controller
    {
        private readonly SpaceAgenciesDbContext _context;

        public MissionsController(SpaceAgenciesDbContext context)
        {
            _context = context;
        }

        // GET: Missions
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return  View(_context.Missions);
            var spaceAgenciesDbContext = _context.Missions.Where(m => m.ProgramId == id).Include(m => m.Program);
            return View(await spaceAgenciesDbContext.ToListAsync());
        }

        // GET: Missions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var missions = await _context.Missions
                .Include(m => m.Program)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (missions == null)
            {
                return NotFound();
            }

            //  return View(missions);
            return RedirectToAction("Index", "Crews", id);
        }

        // GET: Missions/Create
        public IActionResult Create()
        {
            ViewData["ProgramId"] = new SelectList(_context.SpacePrograms, "Id", "Target");
            return View();
        }

        // POST: Missions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate,Title,IsRobotic,ProgramId")] Missions missions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(missions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProgramId"] = new SelectList(_context.SpacePrograms, "Id", "Target", missions.ProgramId);
            return View(missions);
        }

        // GET: Missions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var missions = await _context.Missions.FindAsync(id);
            if (missions == null)
            {
                return NotFound();
            }
            ViewData["ProgramId"] = new SelectList(_context.SpacePrograms, "Id", "Target", missions.ProgramId);
            return View(missions);
        }

        // POST: Missions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,Title,IsRobotic,ProgramId")] Missions missions)
        {
            if (id != missions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(missions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MissionsExists(missions.Id))
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
            ViewData["ProgramId"] = new SelectList(_context.SpacePrograms, "Id", "Target", missions.ProgramId);
            return View(missions);
        }

        // GET: Missions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var missions = await _context.Missions
                .Include(m => m.Program)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (missions == null)
            {
                return NotFound();
            }

            return View(missions);
        }

        // POST: Missions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var missions = await _context.Missions.FindAsync(id);
            _context.Missions.Remove(missions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MissionsExists(int id)
        {
            return _context.Missions.Any(e => e.Id == id);
        }
    }
}
