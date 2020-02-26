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
    public class AgenciesProgramsController : Controller
    {
        private readonly SpaceAgenciesDbContext _context;

        public AgenciesProgramsController(SpaceAgenciesDbContext context)
        {
            _context = context;
        }

        // GET: AgenciesPrograms
        public async Task<IActionResult> Index()
        {
            var spaceAgenciesDbContext = _context.AgenciesPrograms.Include(a => a.SpaceAgency).Include(a => a.SpaceProgram);
            return View(await spaceAgenciesDbContext.ToListAsync());
        }

        // GET: AgenciesPrograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agenciesPrograms = await _context.AgenciesPrograms
                .Include(a => a.SpaceAgency)
                .Include(a => a.SpaceProgram)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agenciesPrograms == null)
            {
                return NotFound();
            }

            return View(agenciesPrograms);
        }

        // GET: AgenciesPrograms/Create
        public IActionResult Create()
        {
            ViewData["SpaceAgencyId"] = new SelectList(_context.SpaceAgencies, "Id", "Name");
            ViewData["SpaceProgramId"] = new SelectList(_context.SpacePrograms, "Id", "Target");
            return View();
        }

        // POST: AgenciesPrograms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SpaceAgencyId,SpaceProgramId")] AgenciesPrograms agenciesPrograms)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agenciesPrograms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpaceAgencyId"] = new SelectList(_context.SpaceAgencies, "Id", "Name", agenciesPrograms.SpaceAgencyId);
            ViewData["SpaceProgramId"] = new SelectList(_context.SpacePrograms, "Id", "Target", agenciesPrograms.SpaceProgramId);
            return View(agenciesPrograms);
        }

        // GET: AgenciesPrograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agenciesPrograms = await _context.AgenciesPrograms.FindAsync(id);
            if (agenciesPrograms == null)
            {
                return NotFound();
            }
            ViewData["SpaceAgencyId"] = new SelectList(_context.SpaceAgencies, "Id", "Name", agenciesPrograms.SpaceAgencyId);
            ViewData["SpaceProgramId"] = new SelectList(_context.SpacePrograms, "Id", "Target", agenciesPrograms.SpaceProgramId);
            return View(agenciesPrograms);
        }

        // POST: AgenciesPrograms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SpaceAgencyId,SpaceProgramId")] AgenciesPrograms agenciesPrograms)
        {
            if (id != agenciesPrograms.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agenciesPrograms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgenciesProgramsExists(agenciesPrograms.Id))
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
            ViewData["SpaceAgencyId"] = new SelectList(_context.SpaceAgencies, "Id", "Name", agenciesPrograms.SpaceAgencyId);
            ViewData["SpaceProgramId"] = new SelectList(_context.SpacePrograms, "Id", "Target", agenciesPrograms.SpaceProgramId);
            return View(agenciesPrograms);
        }

        // GET: AgenciesPrograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agenciesPrograms = await _context.AgenciesPrograms
                .Include(a => a.SpaceAgency)
                .Include(a => a.SpaceProgram)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agenciesPrograms == null)
            {
                return NotFound();
            }

            return View(agenciesPrograms);
        }

        // POST: AgenciesPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agenciesPrograms = await _context.AgenciesPrograms.FindAsync(id);
            _context.AgenciesPrograms.Remove(agenciesPrograms);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgenciesProgramsExists(int id)
        {
            return _context.AgenciesPrograms.Any(e => e.Id == id);
        }
    }
}
