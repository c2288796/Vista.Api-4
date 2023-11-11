using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vista.Web.Data;

namespace Vista.Web.Controllers
{
    public class WorkshopStaffController : Controller
    {
        private readonly WorkshopsContext _context;

        public WorkshopStaffController(WorkshopsContext context)
        {
            _context = context;
        }

        // GET: WorkshopStaff
        public async Task<IActionResult> Index(int? id = null)
        {
            var workshopsContext = _context.WorkshopStaff.AsQueryable(); // AsQueryable turns the DBSet into a searchable list type

            if (id != null)
            {
                workshopsContext = workshopsContext.Where(ws => ws.WorkshopId == id);
            }

            workshopsContext = workshopsContext
                .Include(w => w.Staff)
                .Include(w => w.Workshop);

            return View(await workshopsContext.ToListAsync());
        }

        // GET: WorkshopStaff/Create
        public IActionResult Create()
        {
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "FirstName");
            ViewData["WorkshopId"] = new SelectList(_context.Workshops, "WorkshopId", "Name");
            return View();
        }

        // POST: WorkshopStaff/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkshopId,StaffId")] WorkshopStaff workshopStaff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workshopStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "FirstName", workshopStaff.StaffId);
            ViewData["WorkshopId"] = new SelectList(_context.Workshops, "WorkshopId", "Name", workshopStaff.WorkshopId);
            return View(workshopStaff);
        }

        // GET: WorkshopStaff/Delete?workshopId=2&staffId=1
        public async Task<IActionResult> Delete(int? workshopId, int? staffId)
        {
            if (workshopId == null || staffId == null || _context.WorkshopStaff == null)
            {
                return NotFound();
            }

            var workshopStaff = await _context.WorkshopStaff
                .Include(w => w.Staff)
                .Include(w => w.Workshop)
                .FirstOrDefaultAsync(m => m.WorkshopId == workshopId && m.StaffId == staffId);
            if (workshopStaff == null)
            {
                return NotFound();
            }

            return View(workshopStaff);
        }

        // POST: WorkshopStaff/Delete?workshopId=2&staffId=1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? workshopId, int? staffId)
        {
            if (_context.WorkshopStaff == null)
            {
                return Problem("Entity set 'WorkshopsContext.WorkshopStaff'  is null.");
            }
            var workshopStaff = await _context.WorkshopStaff.FindAsync(workshopId, staffId);
            if (workshopStaff != null)
            {
                _context.WorkshopStaff.Remove(workshopStaff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
