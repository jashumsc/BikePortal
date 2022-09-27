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
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace MajorProject.Areas.BikePortal.Controllers
{
    [Area("BikePortal")]
 

    public class UpcomingEventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _WebHostEnvironment;

        public UpcomingEventsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _WebHostEnvironment = webHostEnvironment;
        }
        

        // GET: BikePortal/UpcomingEvents
        public async Task<IActionResult> Index()
        {
            return View(await _context.UpcomingEvents.ToListAsync());
        }

        // GET: BikePortal/UpcomingEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upcomingEvent = await _context.UpcomingEvents
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (upcomingEvent == null)
            {
                return NotFound();
            }

            return View(upcomingEvent);
        }

        // GET: BikePortal/UpcomingEvents/Create
        [Authorize(Roles = "PortalAdmin")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: BikePortal/UpcomingEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "PortalAdmin")]
        public async Task<IActionResult> Create([Bind("EventId,EventName,EventDescription,EventDate,EventPhoto")] UpcomingEvent upcomingEvent)
        {
            if (upcomingEvent.EventPhoto != null)
            {
                string folder = "Events/Photo/";
                folder += Guid.NewGuid().ToString() + "_" + upcomingEvent.EventPhoto.FileName;
                string serverFolder = Path.Combine(_WebHostEnvironment.WebRootPath, folder);
                upcomingEvent.EventImage = "/" + folder;
                await upcomingEvent.EventPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            }
            if (ModelState.IsValid)
            {
                _context.Add(upcomingEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(upcomingEvent);
        }

        // GET: BikePortal/UpcomingEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upcomingEvent = await _context.UpcomingEvents.FindAsync(id);
            if (upcomingEvent == null)
            {
                return NotFound();
            }
            return View(upcomingEvent);
        }

        // POST: BikePortal/UpcomingEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,EventDescription,EventDate")] UpcomingEvent upcomingEvent)
        {
            if (id != upcomingEvent.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(upcomingEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UpcomingEventExists(upcomingEvent.EventId))
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
            return View(upcomingEvent);
        }

        // GET: BikePortal/UpcomingEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upcomingEvent = await _context.UpcomingEvents
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (upcomingEvent == null)
            {
                return NotFound();
            }

            return View(upcomingEvent);
        }

        // POST: BikePortal/UpcomingEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var upcomingEvent = await _context.UpcomingEvents.FindAsync(id);
            _context.UpcomingEvents.Remove(upcomingEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UpcomingEventExists(int id)
        {
            return _context.UpcomingEvents.Any(e => e.EventId == id);
        }
    }
}
