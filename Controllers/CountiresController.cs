using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SpaceAgenciesDatabaseApp;

namespace SpaceAgenciesDatabaseApp.Controllers
{
    [Authorize(Roles="admin")]
    public class CountiresController : Controller
    {
        private readonly SpaceAgenciesDbContext _context;

        public CountiresController(SpaceAgenciesDbContext context)
        {
            _context = context;
        }

        // GET: Countires
        public async Task<IActionResult> Index()
        {
            return View(await _context.Countires.ToListAsync());
        }

        // GET: Countires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countires = await _context.Countires
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countires == null)
            {
                return NotFound();
            }

            return View(countires);
        }

        // GET: Countires/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CountryName,Gdp,Population")] Countires countires)
        {
            if (ModelState.IsValid)
            {
                _context.Add(countires);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(countires);
        }

        // GET: Countires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countires = await _context.Countires.FindAsync(id);
            if (countires == null)
            {
                return NotFound();
            }
            return View(countires);
        }

        // POST: Countires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CountryName,Gdp,Population")] Countires countires)
        {
            if (id != countires.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(countires);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountiresExists(countires.Id))
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
            return View(countires);
        }

        // GET: Countires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countires = await _context.Countires
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countires == null)
            {
                return NotFound();
            }

            return View(countires);
        }

        // POST: Countires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var countires = await _context.Countires.FindAsync(id);
            var agenciesInTheCountry = await _context.SpaceAgencies.FirstOrDefaultAsync(a => a.HeadquarterCountryId == id);
            if (agenciesInTheCountry == null)
            {
                _context.Countires.Remove(countires);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("Agency", "You can't delete a country which has still agencies");
            }

            return View(countires);
        }

        private bool CountiresExists(int id)
        {
            return _context.Countires.Any(e => e.Id == id);
        }
    }
}
