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
    [Authorize(Roles = "admin, user")]
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
            if (id == null)
            {
                var spaceAgenciesDbContext = _context.Missions.Include(m => m.Program);
                return View(await spaceAgenciesDbContext.ToListAsync());
            }
            var missions = _context.Missions.Where(m => m.ProgramId == id).Include(m => m.Program);
            return View(await missions.ToListAsync());
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
            if (!missions.IsRobotic)
            {
                if (missions == null)
                {
                    return NotFound();
                }

                //  return View(missions);
                return RedirectToAction("Index", "Crews", id);
            }
            else
            {
                return View(missions);
            }
        }

        // GET: Missions/Create
        public IActionResult Create()
        {
            ViewData["Program"] = new SelectList(_context.SpacePrograms, "Id", "Title");
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
            ViewData["Program"] = new SelectList(_context.SpacePrograms, "Id", "Title", missions.ProgramId);
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
            ViewData["Program"] = new SelectList(_context.SpacePrograms, "Id", "Title", missions.ProgramId);
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
            ViewData["Program"] = new SelectList(_context.SpacePrograms, "Id", "Title", missions.ProgramId);
            return View(missions);
        }
        public async Task<Missions> findMissionWithTheSameName(Missions missions)
        {
            return await _context.Missions.FirstOrDefaultAsync(m => m.Title.Contains(missions.Title));
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
            var crew = await _context.Crews.FirstOrDefaultAsync(c => c.MissionId == id);
            var crewsAndAstronauts = await _context.CrewsAstronauts.Where(ca => ca.CrewId == crew.Id).ToListAsync();
            var astronauts = new List<Astronauts>();
            foreach (var pair in crewsAndAstronauts)
            {
                astronauts.Add(await _context.Astronauts.FirstOrDefaultAsync(a => a.Id == pair.AstronautId));
                _context.CrewsAstronauts.Remove(pair);
            }
            foreach(var astronaut in astronauts)
            {
                _context.Remove(astronaut);
            }
            _context.Crews.Remove(crew);
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
