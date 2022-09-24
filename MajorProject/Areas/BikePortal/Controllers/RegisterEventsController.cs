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
    public class RegisterEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegisterEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BikePortal/RegisterEvents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EventParticipants.Include(r => r.UpcomingEvent);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BikePortal/RegisterEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerEvent = await _context.EventParticipants
                .Include(r => r.UpcomingEvent)
                .FirstOrDefaultAsync(m => m.ParticipateId == id);
            if (registerEvent == null)
            {
                return NotFound();
            }

            return View(registerEvent);
        }

        // GET: BikePortal/RegisterEvents/Create
        public IActionResult Create(int filterEventId)
        {
            ViewData["EventId"] = new SelectList(_context.UpcomingEvents, "EventId", "EventDescription", filterEventId);
            return View();
        }

        // POST: BikePortal/RegisterEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParticipateId,ParticipateName,PhoneNumber,EventId")] RegisterEvent registerEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registerEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.UpcomingEvents, "EventId", "EventDescription", registerEvent.EventId);
            return View(registerEvent);
        }

        // GET: BikePortal/RegisterEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerEvent = await _context.EventParticipants.FindAsync(id);
            if (registerEvent == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.UpcomingEvents, "EventId", "EventDescription", registerEvent.EventId);
            return View(registerEvent);
        }

        // POST: BikePortal/RegisterEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParticipateId,ParticipateName,PhoneNumber,EventId")] RegisterEvent registerEvent)
        {
            if (id != registerEvent.ParticipateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registerEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegisterEventExists(registerEvent.ParticipateId))
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
            ViewData["EventId"] = new SelectList(_context.UpcomingEvents, "EventId", "EventDescription", registerEvent.EventId);
            return View(registerEvent);
        }

        // GET: BikePortal/RegisterEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerEvent = await _context.EventParticipants
                .Include(r => r.UpcomingEvent)
                .FirstOrDefaultAsync(m => m.ParticipateId == id);
            if (registerEvent == null)
            {
                return NotFound();
            }

            return View(registerEvent);
        }

        // POST: BikePortal/RegisterEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registerEvent = await _context.EventParticipants.FindAsync(id);
            _context.EventParticipants.Remove(registerEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegisterEventExists(int id)
        {
            return _context.EventParticipants.Any(e => e.ParticipateId == id);
        }
    }
}
