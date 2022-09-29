﻿using System;
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

    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly ILogger<CompaniesController> _logger;


        public CompaniesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, ILogger<CompaniesController> logger)
        {
            _context = context;
            _WebHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        // GET: BikePortal/Companies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Companies.ToListAsync());
        }

        // GET: BikePortal/Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: BikePortal/Companies/Create
        [Authorize(Roles = "PortalAdmin")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: BikePortal/Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "PortalAdmin")]

        public async Task<IActionResult> Create([Bind("CompanyId,CompanyName,CompanyPhoto,CompanyDescription")] Company company)
        {
            company.CompanyName = company.CompanyName.Trim();

            // Validation Checks - Server-side validation
            bool duplicateExists = _context.Companies.Any(c => c.CompanyName == company.CompanyName);
            if (duplicateExists)
            {
                ModelState.AddModelError("CompanyName", "Duplicate Company Found!");
            }
            if (ModelState.IsValid)
            {
                if(company.CompanyPhoto != null)
                {
                    string folder = "companies/Symbol/";
                    folder += Guid.NewGuid().ToString() + "_" + company.CompanyPhoto.FileName;
                    string serverFolder = Path.Combine(_WebHostEnvironment.WebRootPath , folder);
                    company.CompanyImage = "/" + folder;
                    await company.CompanyPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: BikePortal/Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: BikePortal/Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyId,CompanyName,CompanyDescription")] Company company)
        {
            if (id != company.CompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.CompanyId))
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
            return View(company);
        }

        // GET: BikePortal/Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: BikePortal/Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.CompanyId == id);
        }
    }
}
