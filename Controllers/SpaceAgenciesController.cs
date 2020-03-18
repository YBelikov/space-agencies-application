using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpaceAgenciesDatabaseApp;
using Microsoft.AspNetCore.Http;
using System.IO;
using ClosedXML.Excel;

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
            var spaceAgenciesDbContext = _context.SpaceAgencies.Include(s => s.HeadquarterCountry).Include(s => s.Administrators);
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
            return RedirectToAction("Index", "SpacePrograms", new { Id = spaceAgencies.Id });
        }

        // GET: SpaceAgencies/Create
        public IActionResult Create()
        {
            ViewData["HeadquarterCountry"] = new SelectList(_context.Countires, "Id", "CountryName");
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
            ViewData["HeadquarterCountry"] = new SelectList(_context.Countires, "Id", "CountryName", spaceAgencies.HeadquarterCountryId);
            return View(spaceAgencies);
        }

        // GET: SpaceAgencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spaceAgencies = await _context.SpaceAgencies.Include(a => a.Administrators).Include(a => a.HeadquarterCountry).Include(a => a.AgenciesPrograms)
                                .ThenInclude(ap => ap.SpaceProgram).FirstOrDefaultAsync(a => a.Id == id);
            if (spaceAgencies == null)
            {
                return NotFound();
            }
            ViewData["HeadquarterCountry"] = new SelectList(_context.Countires, "Id", "CountryName", spaceAgencies.HeadquarterCountryId);
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
            ViewData["HeadquarterCountry"] = new SelectList(_context.Countires, "Id", "CountryName", spaceAgencies.HeadquarterCountryId);
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
        private void DeleteAgencyAdmin(int? id)
        {
            var administrator =  _context.Administrators.FirstOrDefault(a => a.SpaceAgencyId == id);
            _context.Administrators.Remove(administrator);

        }
        private void DeleteProgram(SpacePrograms program)
        {
            var agencyAndProgram =  _context.AgenciesPrograms.FirstOrDefault(ap => ap.SpaceProgramId == program.Id);
            _context.AgenciesPrograms.Remove(agencyAndProgram);
            var programAndState =  _context.ProgramsStates.FirstOrDefault(ps => ps.ProgramId == program.Id);
            _context.ProgramsStates.Remove(programAndState);
            _context.SpacePrograms.Remove(program);
        }
        public async Task<ActionResult> Export()
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var agencies = await _context.SpaceAgencies.Include(a => a.HeadquarterCountry).Include(a => a.AgenciesPrograms)
                              .ThenInclude(ap => ap.SpaceProgram)
                               .ThenInclude(p => p.ProgramsStates).ThenInclude(ps => ps.State).ToListAsync();
                foreach(var agency in agencies)
                {
                    var worksheet = workbook.Worksheets.Add(agency.Name);
                    InitializeWorksheetHeaders(worksheet);
                    WriteAgencyToWorksheet(agency, worksheet);
                    var country = agency.HeadquarterCountry;
                    WriteCountryToWorksheet(country, worksheet);
                    worksheet.Row(1).Style.Font.Bold = true;
                    var programs = (from agenciesPrograms in _context.AgenciesPrograms where 
                                    agenciesPrograms.SpaceAgencyId == agency.Id select agenciesPrograms.SpaceProgram).ToList();
                    WriteProgramsToWorksheet(programs, worksheet);
                    
                }
                return CreateFileWithContent(workbook);
            }
            
        }
         
        private FileContentResult CreateFileWithContent(IXLWorkbook workbook)
        {
            using(var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                stream.Flush();
                return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = $"agencies_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                };
            }
        }
        private void WriteProgramsToWorksheet(List<SpacePrograms> programs, IXLWorksheet worksheet)
        {
            int indexOfFirstProgramCell = 6;
            for(int i = 0; i < programs.Count; ++i)
            {
                worksheet.Cell(i + 2, indexOfFirstProgramCell).Value = programs[i].Title;
                worksheet.Cell(i + 2, indexOfFirstProgramCell + 1).Value = programs[i].StartDate.ToString();
                worksheet.Cell(i + 2, indexOfFirstProgramCell + 2).Value = programs[i].EndDate.ToString();
                worksheet.Cell(i + 2, indexOfFirstProgramCell + 3).Value = programs[i].ProgramsStates.First().State.StateName;
                worksheet.Cell(i + 2, indexOfFirstProgramCell + 4).Value = programs[i].Target;
            }
        }
        private void WriteAgencyToWorksheet(SpaceAgencies agency, IXLWorksheet worksheet)
        {
            int indexOfFirstAgencyCell = 1;
            worksheet.Cell(2, indexOfFirstAgencyCell).Value = agency.Budget;
            worksheet.Cell(2, indexOfFirstAgencyCell + 1).Value = agency.DateOfEstablishment;
        }
        private void WriteCountryToWorksheet(Countires country, IXLWorksheet worksheet)
        {
            int indexOfFirstCountryCell = 3;
            worksheet.Cell(2, indexOfFirstCountryCell).Value = country.CountryName;
            worksheet.Cell(2, indexOfFirstCountryCell + 1).Value = country.Gdp;
            worksheet.Cell(2, indexOfFirstCountryCell + 2).Value = country.Population;
        }
        private void InitializeWorksheetHeaders(IXLWorksheet worksheet)
        {
            worksheet.Cell("A1").Value = "Budget";
            worksheet.Cell("B1").Value = "Date of Est.";
            worksheet.Cell("C1").Value = "Country";
            worksheet.Cell("D1").Value = "GDP";
            worksheet.Cell("E1").Value = "Population in mln";
            worksheet.Cell("F1").Value = "Program";
            worksheet.Cell("G1").Value = "Start";
            worksheet.Cell("H1").Value = "End";
            worksheet.Cell("I1").Value = "Status";
            worksheet.Cell("J1").Value = "Target";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile excelFile)
        {
            if (ModelState.IsValid)
            {
               
                if (excelFile != null)
                {
                    using (var stream = new FileStream(excelFile.FileName, FileMode.Create))
                    {
                        await excelFile.CopyToAsync(stream);
                        using (XLWorkbook workbook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        {
                            foreach (IXLWorksheet worksheet in workbook.Worksheets)
                            {
                                CreateAgencyAndCountry(worksheet);
                            }
                        }
                    }

                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        private void CreateAgencyAndCountry(IXLWorksheet worksheet)
        {
            SpaceAgencies newAgency;
            var a = (from agency in _context.SpaceAgencies
                     where agency.Name.Contains(worksheet.Name)
                     select agency).ToList();
            if (a.Count > 0)
            {
                newAgency = a[0];
            }
            else
            {
                newAgency = CreateAgency(worksheet);
                var row = RowWithIndexInWorksheet(worksheet, 0, 1);
                var country =  _context.Countires.FirstOrDefault(c => c.CountryName.Contains(row.Cell(3).Value.ToString()));
                if (country != null)
                {
                    AttachExistingCountryToAgency(newAgency, country);
                }
                else
                {
                    country = CreateCountry(worksheet);
                    _context.Countires.Add(country);
                    AttachExistingCountryToAgency(newAgency, country);
                }
                _context.SpaceAgencies.Add(newAgency);
            }
            foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
            {
                CreateProgramsForAgency(newAgency, row);

            }
        }

        private  void CreateProgramsForAgency(SpaceAgencies newAgency, IXLRow row)
        {
            int indexOfFirstProgramField = 6;
            try
            { 
                SpacePrograms program;
                var existingPrograms = (from programs in _context.SpacePrograms
                                        where programs.Title.Contains(row.Cell(indexOfFirstProgramField).Value.ToString())
                                        select programs).ToList();
                if (existingPrograms.Count > 0)
                {
                    program = existingPrograms[0];
                }
                else
                {
                    program = CreateProgram(row, indexOfFirstProgramField);
                    States state =  _context.States.FirstOrDefault(s => s.StateName == row.Cell(indexOfFirstProgramField + 3)
                                    .Value.ToString());
                    _context.SpacePrograms.Add(program);
                    AddNewProgramAndStatePairToContext(program, state);
                    AddNewAgencyAndProgramPairToContext(newAgency, program);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private SpaceAgencies CreateAgency(IXLWorksheet worksheet)
        {
            SpaceAgencies agency = new SpaceAgencies();
            agency.Name = worksheet.Name;
            var row = RowWithIndexInWorksheet(worksheet, 0, 1);
            agency.Budget = Double.Parse(row.Cell(1).Value.ToString());
            agency.DateOfEstablishment = DateTime.Parse(row.Cell(2).Value.ToString());
            return agency;
        }
        private IXLRow RowWithIndexInWorksheet(IXLWorksheet worksheet, int index, int numberOfSkipedRows)
        {
            return worksheet.RowsUsed().Skip(numberOfSkipedRows).ElementAt(index);
        }
        private void AttachExistingCountryToAgency(SpaceAgencies agency, Countires country) 
        {
            agency.HeadquarterCountry = country;
            agency.HeadquarterCountryId = country.Id;
        }
        private Countires CreateCountry(IXLWorksheet worksheet)
        {
            Countires country = new Countires();
            var row = RowWithIndexInWorksheet(worksheet, 0, 1);
            country.CountryName = row.Cell(3).Value.ToString();
            country.Gdp = Decimal.Parse(row.Cell(4).Value.ToString());
            country.Population = Double.Parse(row.Cell(5).Value.ToString());
            return country;
        }
        private SpacePrograms CreateProgram(IXLRow row, int indexOfFirstProgramField)
        {
            SpacePrograms program = new SpacePrograms();
            program.Title = row.Cell(indexOfFirstProgramField).Value.ToString();
            program.StartDate = DateTime.Parse(row.Cell(indexOfFirstProgramField + 1).Value.ToString());
            string endDate = row.Cell(indexOfFirstProgramField + 2).Value.ToString();
            if (endDate.Length > 0)
            {
                program.EndDate = DateTime.Parse(endDate);
            }
            else
            {
                program.EndDate = null;
            }
            program.Target = row.Cell(indexOfFirstProgramField + 4).Value.ToString();
            return program;
        }
        private void AddNewProgramAndStatePairToContext(SpacePrograms program, States state)
        {
            ProgramsStates newProgramAndStateRecord = new ProgramsStates();
            newProgramAndStateRecord.ProgramId = program.Id;
            newProgramAndStateRecord.StateId = state.Id;
            newProgramAndStateRecord.Program = program;
            newProgramAndStateRecord.State = state;
            _context.ProgramsStates.Add(newProgramAndStateRecord);
        }
        private void AddNewAgencyAndProgramPairToContext(SpaceAgencies agency, SpacePrograms program)
        {
            AgenciesPrograms newAgencyAndProgramRecord = new AgenciesPrograms();
            newAgencyAndProgramRecord.SpaceAgencyId = agency.Id;
            newAgencyAndProgramRecord.SpaceProgramId = program.Id;
            newAgencyAndProgramRecord.SpaceProgram = program;
            newAgencyAndProgramRecord.SpaceAgency = agency;
            _context.AgenciesPrograms.Add(newAgencyAndProgramRecord);

        }

    }
}
