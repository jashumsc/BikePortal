using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MajorProject.Data;
using MajorProject.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace MajorProject.Areas.BikePortal.Controllers
{
    [Area("BikePortal")]

    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, ILogger<ProductsController> logger)
        {
            _context = context;
            _WebHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        // GET: BikePortal/Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        public async Task<IActionResult> Offer(string filterProductName)
        {
            var viewmodel = await _context.Products
                                         .Where(p => p.ProductName == filterProductName)
                                         .ToListAsync();

            return View(viewName: "Index", model: viewmodel);
        }

        // GET: BikePortal/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: BikePortal/Products/Create
        [Authorize(Roles = "PortalAdmin")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: BikePortal/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "PortalAdmin")]

        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductPrice,ProductPhoto")] Product product)
        {
            product.ProductName = product.ProductName.Trim();

            // Validation Checks - Server-side validation
            bool duplicateExists = _context.Products.Any(b => b.ProductName == product.ProductName);
            if (duplicateExists)
            {
                ModelState.AddModelError("CompanyName", "Duplicate Company Found!");
            }
            if (product.ProductPhoto != null)
            {
                string folder = "Offers/Photo/";
                folder += Guid.NewGuid().ToString() + "_" + product.ProductPhoto.FileName;
                string serverFolder = Path.Combine(_WebHostEnvironment.WebRootPath, folder);
                product.ProductImage = "/" + folder;
                await product.ProductPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: BikePortal/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: BikePortal/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductPrice")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            return View(product);
        }

        // GET: BikePortal/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: BikePortal/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
