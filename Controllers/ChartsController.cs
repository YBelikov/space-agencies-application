using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SpaceAgenciesDatabaseApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly SpaceAgenciesDbContext _context;
        public ChartsController(SpaceAgenciesDbContext context)
        {
            _context = context;
        }
        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var agencies = _context.SpaceAgencies.Include(a => a.AgenciesPrograms).ThenInclude(ap => ap.SpaceProgram).ToList();
           // var programs = agenciesAndPrograms.AgenciesPrograms.Select(ap => ap.SpaceProgram).ToList();
            List<object> agenciesAndProgramsCount = new List<object>();
            agenciesAndProgramsCount.Add(new[] { "Agency", "Number of programs" });
            foreach(var a in agencies)
            {
                agenciesAndProgramsCount.Add(new object[] { a.Name, a.AgenciesPrograms.Count() });
            }
            return new JsonResult(agenciesAndProgramsCount);
        }
        [HttpGet("ProgramsJsonData")]
        public JsonResult ProgramsJsonData()
        {
            var programs = _context.SpacePrograms.Include(p => p.Missions).ToList();
            List<object> missionsCount = new List<object>();
            missionsCount.Add(new[] { "Programs", "Number of missions" });
            foreach(var p in programs)
            {
                missionsCount.Add(new object[] { p.Title, p.Missions.Count });
            }
            return new JsonResult(missionsCount);
        }
    }
}