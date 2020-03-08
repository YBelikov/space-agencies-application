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
    public class AstronautsController : Controller
    {
        private readonly SpaceAgenciesDbContext _context;

        public AstronautsController(SpaceAgenciesDbContext context)
        {
            _context = context;
        }

        // GET: Astronauts
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                var spaceAgenciesDbContext = _context.Astronauts.Include(a => a.Country);
                return View(await spaceAgenciesDbContext.ToListAsync());
            }
            ViewBag.CrewId = id;
            var astronautsAndCrews = _context.Crews.Include(crews => crews.CrewsAstronauts).ThenInclude(crew => crew.Astronaut)
                .Include(crews => crews.CrewsAstronauts).ThenInclude(crew => crew.Astronaut.Country)
               .FirstOrDefault(crews => crews.Id == id);
           // var astronautsAndCrews = _context.Astronauts.Include(a => a.Country).Include(a => a.CrewsAstronauts)
            //    .ThenInclude(ca => ca.Crew).FirstOrDefault(a => a.CrewId == id);
                var astronauts = astronautsAndCrews.CrewsAstronauts.Select(c => c.Astronaut).ToList();
                return View(astronauts);
        }

        // GET: Astronauts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var astronauts = await _context.Astronauts
                .Include(a => a.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (astronauts == null)
            {
                return NotFound();
            }

            return View(astronauts);
        }

        // GET: Astronauts/Create
        public IActionResult Create(int crewId)
        {

            ViewData["CountryId"] = new SelectList(_context.Countires, "Id", "CountryName");
            ViewBag.CrewId = crewId;
             
            return View();
        }

        // POST: Astronauts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int crewId, [Bind("Id,Name,Surname,BirthDate,Duty,CrewId,CountryId")] Astronauts astronaut)
        {

            if (ModelState.IsValid)
            {
                CrewsAstronauts newPair = new CrewsAstronauts();
                newPair.CrewId = crewId;
                newPair.AstronautId = astronaut.Id;
                newPair.Crew = _context.Crews.Where(c => c.Id == crewId).FirstOrDefault();
                newPair.Astronaut = astronaut;
                _context.Add(astronaut);
                _context.Add(newPair);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countires, "Id", "CountryName", astronaut.CountryId);
            return View(astronaut);
        }

        // GET: Astronauts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var astronauts = await _context.Astronauts.FindAsync(id);
            if (astronauts == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countires, "Id", "CountryName", astronauts.CountryId);
            return View(astronauts);
        }

        // POST: Astronauts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,BirthDate,Duty,CrewId,CountryId")] Astronauts astronauts)
        {
            if (id != astronauts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(astronauts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AstronautsExists(astronauts.Id))
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
            ViewData["CountryId"] = new SelectList(_context.Countires, "Id", "CountryName", astronauts.CountryId);
            return View(astronauts);
        }

        // GET: Astronauts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var astronauts = await _context.Astronauts
                .Include(a => a.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (astronauts == null)
            {
                return NotFound();
            }

            return View(astronauts);
        }

        // POST: Astronauts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var astronauts = await _context.Astronauts.FindAsync(id);
            _context.Astronauts.Remove(astronauts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AstronautsExists(int id)
        {
            return _context.Astronauts.Any(e => e.Id == id);
        }
    }
}
