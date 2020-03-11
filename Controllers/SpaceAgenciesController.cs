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
    public class SpaceAgenciesController : Controller
    {
        private readonly SpaceAgenciesDbContext _context;

        public SpaceAgenciesController(SpaceAgenciesDbContext context)
        {
            _context = context;
        }

        // GET: SpaceAgencies
        public async Task<IActionResult> Index()
        {
            var spaceAgenciesDbContext = _context.SpaceAgencies.Include(s => s.HeadquarterCountry).Include(s=>s.Administrators);
            return View(await spaceAgenciesDbContext.ToListAsync());
        }

        // GET: SpaceAgencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spaceAgencies = await _context.SpaceAgencies.Include(a => a.HeadquarterCountry)
                .Include(a => a.Administrators).FirstOrDefaultAsync(m => m.Id == id);
            if (spaceAgencies == null)
            {
                return NotFound();
            }

           // return View(spaceAgencies); 
            return RedirectToAction("Index", "SpacePrograms", new {Id = spaceAgencies.Id});
        }

        // GET: SpaceAgencies/Create
        public IActionResult Create()
        {
            ViewData["HeadquarterCountryId"] = new SelectList(_context.Countires, "Id", "CountryName");
            return View();
        }

        // POST: SpaceAgencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateOfEstablishment,Budget,HeadquarterCountryId")] SpaceAgencies spaceAgencies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spaceAgencies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HeadquarterCountryId"] = new SelectList(_context.Countires, "Id", "CountryName", spaceAgencies.HeadquarterCountryId);
            return View(spaceAgencies);
        }

        // GET: SpaceAgencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spaceAgencies = await _context.SpaceAgencies.FindAsync(id);
            if (spaceAgencies == null)
            {
                return NotFound();
            }
            ViewData["HeadquarterCountryId"] = new SelectList(_context.Countires, "Id", "CountryName", spaceAgencies.HeadquarterCountryId);
            return View(spaceAgencies);
        }

        // POST: SpaceAgencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateOfEstablishment,Budget,HeadquarterCountryId")] SpaceAgencies spaceAgencies)
        {
            if (id != spaceAgencies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spaceAgencies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpaceAgenciesExists(spaceAgencies.Id))
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
            ViewData["HeadquarterCountryId"] = new SelectList(_context.Countires, "Id", "CountryName", spaceAgencies.HeadquarterCountryId);
            return View(spaceAgencies);
        }

        // GET: SpaceAgencies/Delete/5
        public async Task<IActionResult> Delete(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }

            var spaceAgencies = await _context.SpaceAgencies
                .Include(s => s.HeadquarterCountry)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spaceAgencies == null)
            {
                return NotFound();
            }
           return View(spaceAgencies);
        }

        // POST: SpaceAgencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spaceAgencies = _context.SpaceAgencies.Include(a => a.AgenciesPrograms)
                .ThenInclude(ap => ap.SpaceProgram).FirstOrDefault(a => a.Id == id);
               var programs = spaceAgencies.AgenciesPrograms.Select(ap => ap.SpaceProgram).ToList();
            
            foreach (var program in programs)
            { 
                DeleteProgram(program);
            }
            DeleteAgencyAdmin(id);
            _context.SpaceAgencies.Remove(spaceAgencies); 
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }

        private bool SpaceAgenciesExists(int id)
        {
            return _context.SpaceAgencies.Any(e => e.Id == id);
        }
        private async void DeleteAgencyAdmin(int? id)
        {
            var administrator = await _context.Administrators.FirstOrDefaultAsync(a => a.SpaceAgencyId == id);
            _context.Administrators.Remove(administrator);

        }
        private async void DeleteProgram(SpacePrograms program)
        {
            var agencyAndProgram = await _context.AgenciesPrograms.FirstOrDefaultAsync(ap => ap.SpaceProgramId == program.Id);
            _context.AgenciesPrograms.Remove(agencyAndProgram);
            var programAndState = await _context.ProgramsStates.FirstOrDefaultAsync(ps => ps.ProgramId == program.Id);
            _context.ProgramsStates.Remove(programAndState);
            _context.SpacePrograms.Remove(program);
        }
    }
}
