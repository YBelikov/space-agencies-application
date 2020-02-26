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
    public class ProgramsStatesController : Controller
    {
        private readonly SpaceAgenciesDbContext _context;

        public ProgramsStatesController(SpaceAgenciesDbContext context)
        {
            _context = context;
        }

        // GET: ProgramsStates
        public async Task<IActionResult> Index()
        {
            var spaceAgenciesDbContext = _context.ProgramsStates.Include(p => p.Program).Include(p => p.State);
            return View(await spaceAgenciesDbContext.ToListAsync());
        }

        // GET: ProgramsStates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programsStates = await _context.ProgramsStates
                .Include(p => p.Program)
                .Include(p => p.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (programsStates == null)
            {
                return NotFound();
            }

            return View(programsStates);
        }

        // GET: ProgramsStates/Create
        public IActionResult Create()
        {
            ViewData["ProgramId"] = new SelectList(_context.SpacePrograms, "Id", "Target");
            ViewData["StateId"] = new SelectList(_context.States, "Id", "StateName");
            return View();
        }

        // POST: ProgramsStates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StateId,ProgramId")] ProgramsStates programsStates)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programsStates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProgramId"] = new SelectList(_context.SpacePrograms, "Id", "Target", programsStates.ProgramId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "StateName", programsStates.StateId);
            return View(programsStates);
        }

        // GET: ProgramsStates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programsStates = await _context.ProgramsStates.FindAsync(id);
            if (programsStates == null)
            {
                return NotFound();
            }
            ViewData["ProgramId"] = new SelectList(_context.SpacePrograms, "Id", "Target", programsStates.ProgramId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "StateName", programsStates.StateId);
            return View(programsStates);
        }

        // POST: ProgramsStates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StateId,ProgramId")] ProgramsStates programsStates)
        {
            if (id != programsStates.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programsStates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramsStatesExists(programsStates.Id))
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
            ViewData["ProgramId"] = new SelectList(_context.SpacePrograms, "Id", "Target", programsStates.ProgramId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "StateName", programsStates.StateId);
            return View(programsStates);
        }

        // GET: ProgramsStates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programsStates = await _context.ProgramsStates
                .Include(p => p.Program)
                .Include(p => p.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (programsStates == null)
            {
                return NotFound();
            }

            return View(programsStates);
        }

        // POST: ProgramsStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programsStates = await _context.ProgramsStates.FindAsync(id);
            _context.ProgramsStates.Remove(programsStates);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramsStatesExists(int id)
        {
            return _context.ProgramsStates.Any(e => e.Id == id);
        }
    }
}
