using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SpaceAgenciesDatabaseApp;
using Microsoft.AspNetCore.Http;
using System.IO;
using ClosedXML.Excel;


namespace SpaceAgenciesDatabaseApp.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class SpaceProgramsController : Controller
    {
        private readonly SpaceAgenciesDbContext _context;
        public SpaceProgramsController(SpaceAgenciesDbContext context)
        {
            _context = context;
          
        }

        // GET: SpacePrograms
        public async Task<IActionResult> Index(int? id)
        {


            if (id == null)
            {
                var allPrograms = _context.SpacePrograms.Include(p => p.ProgramsStates).ThenInclude(ps => ps.State);
                return View(allPrograms);
            }
            ViewBag.AgencyId = id;
            ViewBag.AgencyName = _context.SpaceAgencies.Where(a => a.Id == id).FirstOrDefault().Name;
            var programsAndAgenices = _context.SpaceAgencies.Include(a => a.AgenciesPrograms)
                .ThenInclude(ap => ap.SpaceProgram).Include(a => a.AgenciesPrograms).ThenInclude(ap => ap.SpaceProgram)
                .ThenInclude(p => p.ProgramsStates).ThenInclude(ps => ps.State).FirstOrDefault(a => a.Id == id);
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
            return RedirectToAction("Index", "Missions", new { Id = spacePrograms.Id });
            //return View(spacePrograms);
        }

        // GET: SpacePrograms/Create
        public IActionResult Create(int agencyId)
        {
            ViewBag.AgencyId = agencyId;
            ViewBag.AgencyName = _context.SpaceAgencies.Where(a => a.Id == agencyId).FirstOrDefault().Name;
            
            ViewData["States"] = new SelectList(_context.States, "Id", "StateName");
            return View();
        }

        // POST: SpacePrograms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int agencyId, [Bind("Id,Title,StartDate,EndDate,Target,StateId")] SpacePrograms spacePrograms, IFormCollection collection)
        {
            if (await findProgramWithTheSameName(spacePrograms) != null) ModelState.AddModelError(String.Empty, "Program with this name already exists");
            if (!validateProgramDates(spacePrograms)) ModelState.AddModelError(String.Empty, "Program can't start after own end date");
            if (ModelState.IsValid)
            {

                AgenciesPrograms newPair = new AgenciesPrograms();
                
                var agency = _context.SpaceAgencies.Where(a => a.Id == agencyId).FirstOrDefault();
                newPair.SpaceAgency = agency;
                newPair.SpaceProgram = spacePrograms;
                newPair.SpaceAgencyId = agencyId;
                newPair.SpaceProgramId = spacePrograms.Id;
                spacePrograms.AgenciesPrograms.Add(newPair);
                agency.AgenciesPrograms.Add(newPair);
                _context.Add(spacePrograms);
                _context.Add(newPair);
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
            if (await findProgramWithTheSameName(spacePrograms) != null) ModelState.AddModelError(String.Empty, "Program with this name already exists");
            if (!validateProgramDates(spacePrograms)) ModelState.AddModelError(String.Empty, "Program can't start after own end date");

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


            return View(spacePrograms);
        }

        public async Task<SpacePrograms> findProgramWithTheSameName(SpacePrograms spacePrograms)
        {
            return await _context.SpacePrograms.FirstOrDefaultAsync(p => p.Title == spacePrograms.Title); 
        }

        public bool validateProgramDates(SpacePrograms spacePrograms)
        {
            return spacePrograms.StartDate < spacePrograms.EndDate;
            
        }
        // GET: SpacePrograms/Delete/5

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
            DeleteStateOfProgram(id);
            DeleteProgram(id);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private  void DeleteStateOfProgram(int id)
        {
            var programState = _context.ProgramsStates.Where(ps => ps.ProgramId == id).FirstOrDefault();
            _context.ProgramsStates.Remove(programState);
        }

        private void DeleteProgram(int id)
        {
            var program = _context.SpacePrograms.Find(id);
            var agencyAndProgram = _context.AgenciesPrograms.FirstOrDefault(ap => ap.SpaceProgramId == program.Id);
            _context.AgenciesPrograms.Remove(agencyAndProgram);
            DeleteMissions(program);
            var programAndState = _context.ProgramsStates.FirstOrDefault(ps => ps.ProgramId == program.Id);
            if (programAndState != null)
            {
                _context.ProgramsStates.Remove(programAndState);
            }
            _context.SpacePrograms.Remove(program);
        }
        private void DeleteMissions(SpacePrograms program)
        {
            var missions = _context.Missions.Where(m => m.ProgramId == program.Id);
            if (missions != null)
            {
                foreach (var mission in missions)
                {
                    DeleteCrew(mission);
                    _context.Missions.Remove(mission);
                }
            }
        }
        private void DeleteCrew(Missions mission)
        {
            var crews = _context.Crews.Where(c => c.MissionId == mission.Id).ToList();
            if (crews != null)
            {
                foreach (var crew in crews)
                {
                    DeleteAstronauts(crew);
                    _context.Crews.Remove(crew);
                }
            }
        }
        private void DeleteAstronauts(Crews crew)
        {
            var astronauts = _context.Astronauts.Where(a => a.CrewId == crew.Id).ToList();
            var crewAndAstronauts = _context.CrewsAstronauts.Where(ca => ca.CrewId == crew.Id).ToList();
            if (crewAndAstronauts != null)
            {
                foreach (var ca in crewAndAstronauts)
                {
                    _context.CrewsAstronauts.Remove(ca);
                }
            }
            if (astronauts != null)
            {
                foreach (var astronaut in astronauts)
                {
                    _context.Astronauts.Remove(astronaut);
                }
            }
        }
        private bool SpaceProgramsExists(int id)
        {
            return _context.SpacePrograms.Any(e => e.Id == id);
        }


    }

}
