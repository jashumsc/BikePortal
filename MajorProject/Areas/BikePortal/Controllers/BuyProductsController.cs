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
    public class BuyProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BuyProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BikePortal/BuyProducts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BuyProducts.Include(b => b.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> GetProducts(string filterPhn)
        {
           
            var viewmodel = await _context.BuyProducts
                                          .Where(b => b.CustomerPhone == filterPhn)
                                          .Include(b => b.Product)
                                          .ToListAsync();

            return View(viewName: "Index", model: viewmodel);
        }

        // GET: BikePortal/BuyProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyProduct = await _context.BuyProducts
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.BuyId == id);
            if (buyProduct == null)
            {
                return NotFound();
            }

            return View(buyProduct);
        }

        // GET: BikePortal/BuyProducts/Create
        public IActionResult Create(int filterProductId)
        {
            ViewData["ProcudtId"] = new SelectList(_context.Products, "ProductId", "ProductName", filterProductId);
           
            return View();
        }

        // POST: BikePortal/BuyProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BuyId,CustomerName,CustomerPhone,ProcudtId,Quantity")] BuyProduct buyProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buyProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProcudtId"] = new SelectList(_context.Products, "ProductId", "ProductName", buyProduct.ProcudtId);
            return View(buyProduct);
        }

        // GET: BikePortal/BuyProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyProduct = await _context.BuyProducts.FindAsync(id);
            if (buyProduct == null)
            {
                return NotFound();
            }
            ViewData["ProcudtId"] = new SelectList(_context.Products, "ProductId", "ProductName", buyProduct.ProcudtId);
            return View(buyProduct);
        }

        // POST: BikePortal/BuyProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BuyId,CustomerName,CustomerPhone,ProcudtId,Quantity")] BuyProduct buyProduct)
        {
            if (id != buyProduct.BuyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buyProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuyProductExists(buyProduct.BuyId))
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
            ViewData["ProcudtId"] = new SelectList(_context.Products, "ProductId", "ProductName", buyProduct.ProcudtId);
            return View(buyProduct);
        }

        // GET: BikePortal/BuyProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyProduct = await _context.BuyProducts
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.BuyId == id);
            if (buyProduct == null)
            {
                return NotFound();
            }

            return View(buyProduct);
        }

        // POST: BikePortal/BuyProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buyProduct = await _context.BuyProducts.FindAsync(id);
            _context.BuyProducts.Remove(buyProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuyProductExists(int id)
        {
            return _context.BuyProducts.Any(e => e.BuyId == id);
        }
    }
}
