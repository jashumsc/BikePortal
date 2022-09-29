using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MajorProject.Data;
using MajorProject.Models;
using Microsoft.Extensions.Logging;

namespace MajorProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ServicesController> _logger;

        public ServicesController(ApplicationDbContext context,ILogger<ServicesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            

            try
            {
                var services = await _context.Services
                                     .ToListAsync();

                // Check if data exists in the Database
                if (services == null)
                {
                    return NotFound();          // RETURN: No data was found            HTTP 404
                }
                return Ok(services);          // RETURN: OkObjectResult - good result HTTP 200
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message); // RETURN: BadResult                    HTTP 400
            }
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetService(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            try
            {

                var service = await _context.Services.FindAsync(id);

                if (service == null)
                {
                    return NotFound();          // RETURN: No data was found            HTTP 404
                }

                return Ok(service);             // RETURN: OkObjectResult - good result HTTP 200
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, Service service)
        {
            if (id != service.ServiceId)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(service).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (DbUpdateConcurrencyException exp)
                {
                    if (!ServiceExists(id))
                    {
                        return NotFound();      // RETURN: No data was found            HTTP 404
                    }
                    else
                    {
                        ModelState.AddModelError("PUT", exp.Message);
                    }
                }
            }

            return BadRequest(ModelState);
        }

        // POST: api/Services
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostService(Service service)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Services.Add(service);
                    await _context.SaveChangesAsync();
                    var result = CreatedAtAction("GetService", new { id = service.ServiceId }, service);
                    return Ok(result);                                       // RETURN: OkObjectResult - good result HTTP 200

                }
                catch (System.Exception exp)
                {
                    ModelState.TryAddModelError("POST", exp.Message);
                   
                }
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int? id)
        {
            if (!id.HasValue) 
            {
                return BadRequest();
            }
            try
            {


                var service = await _context.Services.FindAsync(id);
                if (service == null)
                {
                    return NotFound();      // RETURN: No data was found            HTTP 404
                }

                _context.Services.Remove(service);
                await _context.SaveChangesAsync();

                return Ok(service);                      // RETURN: OkObjectResult - good result HTTP 200
            }
            catch(System.Exception exp)
            {
                ModelState.AddModelError("DELETE", exp.Message);
                return BadRequest(ModelState);
            }
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.ServiceId == id);
        }
    }
}
