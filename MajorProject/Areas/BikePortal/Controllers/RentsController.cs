using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MajorProject.Data;
using MajorProject.Models;

namespace MajorProject.Areas.BikePortal.Controllers
{
    [Area("BikePortal")]
    public class RentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BikePortal/Rents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rents.Include(r => r.Bike);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> GetPurchases(string filterPhn)
        {
           
            var viewmodel = await _context.Rents
                                          .Where(r => r.CustomerPhone == filterPhn)
                                          .Include(r => r.Bike)
                                          .ToListAsync();

            return View(viewName: "Index", model: viewmodel);
        }

        // GET: BikePortal/Rents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rents
                .Include(r => r.Bike)
                .FirstOrDefaultAsync(m => m.RentId == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // GET: BikePortal/Rents/Create
        public IActionResult Create(int filterBikeId)
        {
            ViewData["BikeId"] = new SelectList(_context.Bikes, "BikeId", "BikeName",filterBikeId);           
            return View();
        }

        // POST: BikePortal/Rents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentId,CustomerName,BikeId,RentDate,CustomerPhone")] Rent rent)
        {
            if(rent != null)
            {

                if (ModelState.IsValid)
                {
                    _context.Add(rent);
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
                    return RedirectToAction(
               actionName: "Index",
               controllerName: "Companies",
               routeValues: new { area = "BikePortal" });

                }
                ViewData["BikeId"] = new SelectList(_context.Bikes, "BikeId", "BikeName", rent.BikeId);
                return View(rent);
            }
            return RedirectToAction(
               actionName: "Index",
               controllerName: "Companies",
               routeValues: new { area = "BikePortal" });
        }

        // GET: BikePortal/Rents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rents.FindAsync(id);
            if (rent == null)
            {
                return NotFound();
            }
            ViewData["BikeId"] = new SelectList(_context.Bikes, "BikeId", "BikeName", rent.BikeId);
            return View(rent);
        }

        // POST: BikePortal/Rents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentId,CustomerName,BikeId,RentDate")] Rent rent)
        {
            if (id != rent.RentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentExists(rent.RentId))
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
            ViewData["BikeId"] = new SelectList(_context.Bikes, "BikeId", "BikeName", rent.BikeId);
            return View(rent);
        }

        // GET: BikePortal/Rents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rents
                .Include(r => r.Bike)
                .FirstOrDefaultAsync(m => m.RentId == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // POST: BikePortal/Rents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rent = await _context.Rents.FindAsync(id);
            _context.Rents.Remove(rent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentExists(int id)
        {
            return _context.Rents.Any(e => e.RentId == id);
        }
    }
}
