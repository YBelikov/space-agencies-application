

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
    [Authorize(Roles="admin, user")]
    public class AdministratorsController : Controller
    {
        private readonly SpaceAgenciesDbContext _context;

        public AdministratorsController(SpaceAgenciesDbContext context)
        {
            _context = context;
        }

        // GET: Administrators
        public async Task<IActionResult> Index()
        {
            var spaceAgenciesDbContext = _context.Administrators.Include(a => a.SpaceAgency);
            return View(await spaceAgenciesDbContext.ToListAsync());
        }

        // GET: Administrators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrators = await _context.Administrators
                .Include(a => a.SpaceAgency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrators == null)
            {
                return NotFound();
            }

            return View(administrators);
        }

        // GET: Administrators/Create
        public IActionResult Create()
        {
            ViewData["Agency"] = new SelectList(_context.SpaceAgencies, "Id", "Name");
            return View();
        }

        // POST: Administrators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,BirthDate,SpaceAgencyId")] Administrators administrators)
        {
            if (ModelState.IsValid)
            {
                _context.Add(administrators);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Agency"] = new SelectList(_context.SpaceAgencies, "Id", "Name", administrators.SpaceAgencyId);
            return View(administrators);
        }

        // GET: Administrators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrators = await _context.Administrators.FindAsync(id);
            if (administrators == null)
            {
                return NotFound();
            }
            ViewData["Agency"] = new SelectList(_context.SpaceAgencies, "Id", "Name", administrators.SpaceAgencyId);
            return View(administrators);
        }

        // POST: Administrators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,BirthDate,SpaceAgencyId")] Administrators administrators)
        {
            if (id != administrators.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administrators);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministratorsExists(administrators.Id))
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
            ViewData["Agency"] = new SelectList(_context.SpaceAgencies, "Id", "Name", administrators.SpaceAgencyId);
            return View(administrators);
        }

        // GET: Administrators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrators = await _context.Administrators
                .Include(a => a.SpaceAgency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrators == null)
            {
                return NotFound();
            }

            return View(administrators);
        }

        // POST: Administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var administrators = await _context.Administrators.FindAsync(id);
            _context.Administrators.Remove(administrators);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministratorsExists(int id)
        {
            return _context.Administrators.Any(e => e.Id == id);
        }
    }
}
