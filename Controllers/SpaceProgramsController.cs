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
    public class SpaceProgramsController : Controller
    {
        private readonly SpaceAgenciesDbContext _context;
        private SelectList agencies;
        public SpaceProgramsController(SpaceAgenciesDbContext context)
        {
            _context = context;
        }

        // GET: SpacePrograms
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                var allPrograms = _context.SpacePrograms;
                return View(allPrograms);
            }
            ViewBag.SpaceAgencyId = id;
            var programsAndAgenices = _context.SpaceAgencies.Include(a => a.AgenciesPrograms)
                .ThenInclude(ap => ap.SpaceProgram).FirstOrDefault(a => a.Id == id);
            var programs = programsAndAgenices.AgenciesPrograms.Select(ap => ap.SpaceProgram).ToList();
            return View(programs);
        }

        // GET: SpacePrograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spacePrograms = await _context.SpacePrograms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spacePrograms == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Missions", new { Id = spacePrograms.Id});
            //return View(spacePrograms);
        }

        // GET: SpacePrograms/Create
        public IActionResult Create()
        {
            CreateAgenciesDropDownList();
            return View();
        }

        // POST: SpacePrograms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,StartDate,EndDate,Target")] SpacePrograms spacePrograms)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spacePrograms);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(spacePrograms);
        }

    
        // GET: SpacePrograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spacePrograms = await _context.SpacePrograms.FindAsync(id);
            if (spacePrograms == null)
            {
                return NotFound();
            }
            return View(spacePrograms);
        }

        // POST: SpacePrograms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,StartDate,EndDate,Target,AgencyId")] SpacePrograms spacePrograms)
        {
            if (id != spacePrograms.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spacePrograms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpaceProgramsExists(spacePrograms.Id))
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
            CreateAgenciesDropDownList();
           
            return View(spacePrograms);
        }

        // GET: SpacePrograms/Delete/5
        private void CreateAgenciesDropDownList(object selectedAgency = null)
        {

            agencies = new SelectList(_context.SpaceAgencies, "Id", "Name", selectedAgency);
            ViewData["Agencies"] = agencies;
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spacePrograms = await _context.SpacePrograms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spacePrograms == null)
            {
                return NotFound();
            }

            return View(spacePrograms);
        }

        // POST: SpacePrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var spacePrograms = await _context.SpacePrograms.FindAsync(id);
            _context.SpacePrograms.Remove(spacePrograms);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpaceProgramsExists(int id)
        {
            return _context.SpacePrograms.Any(e => e.Id == id);
        }
    }
}
