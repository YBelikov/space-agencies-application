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
    public class CrewsController : Controller
    {
        private readonly SpaceAgenciesDbContext _context;

        public CrewsController(SpaceAgenciesDbContext context)
        {
            _context = context;
        }

        // GET: Crews
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                var spaceAgenciesDbContext = _context.Crews.Include(c => c.Mission);
                return View(await spaceAgenciesDbContext.ToListAsync());
            }
            var crews = _context.Crews.Where(crew => crew.MissionId == id).Include(crew => crew.Mission);
            return View(await crews.ToListAsync());
        }

        // GET: Crews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crews = await _context.Crews
                .Include(c => c.Mission)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crews == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Astronauts", new { Id = crews.Id});
           // return View(crews);
        }

        // GET: Crews/Create
        public IActionResult Create()
        {
            ViewData["Mission"] = new SelectList(_context.Missions, "Id", "Title");
            return View();
        }

        // POST: Crews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumberOfMembers,MissionId")] Crews crews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Mission"] = new SelectList(_context.Missions, "Id", "Title", crews.MissionId);
            return View(crews);
        }

        // GET: Crews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crews = await _context.Crews.FindAsync(id);
            if (crews == null)
            {
                return NotFound();
            }
            ViewData["Mission"] = new SelectList(_context.Missions, "Id", "Title", crews.MissionId);
            return View(crews);
        }

        // POST: Crews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumberOfMembers,MissionId")] Crews crews)
        {
            if (id != crews.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrewsExists(crews.Id))
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
            ViewData["Mission"] = new SelectList(_context.Missions, "Id", "Title", crews.MissionId);
            return View(crews);
        }

        // GET: Crews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crews = await _context.Crews
                .Include(c => c.Mission)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crews == null)
            {
                return NotFound();
            }

            return View(crews);
        }

        // POST: Crews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            DeleteCrewAndAstronautRecordFromJoinTable(id);
            DeleteCrew(id);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async void DeleteCrewAndAstronautRecordFromJoinTable(int id)
        {
            var crewAndItAstronauts =  await _context.CrewsAstronauts.Where(ca => ca.CrewId == id).ToListAsync();
            foreach(var record in crewAndItAstronauts)
            {
                _context.CrewsAstronauts.Remove(record);
            }
        }
        private async void DeleteCrew(int id)
        {
            var crew = await _context.Crews.FindAsync(id);
            _context.Crews.Remove(crew);
        }
        private bool CrewsExists(int id)
        {
            return _context.Crews.Any(e => e.Id == id);
        }
    }
}
