using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KennedyPerformanceCenter.Data;
using KennedyPerformanceCenter.Models;

namespace KennedyPerformanceCenter.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ServicesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)

        {

            _context = context;
            _userManager = userManager;

        }


        // GET: Services
        public async Task<ActionResult> Index(string searchString)
        {
            var user = await GetCurrentUserAsync();
            var services = await _context.Service
                //.Where(ti => ti.ApplicationUserId == user.Id)
                .Include(tdi => tdi.ApplicationUser)
                .ToListAsync();

            if (searchString != null)
            {
                var filteredServices = _context.Service.Where(s => s.Name.Contains(searchString));
                return View(filteredServices);
            };

            return View(services);
        }


        // GET: Services/Details/1
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Service
               //.Where(p => p.UserId == user.Id)
               .Include(p => p.ApplicationUser)
               .FirstOrDefaultAsync(p => p.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }



        // GET: Services/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Services/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Service service)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                service.ApplicationUserId = user.Id;

                _context.Service.Add(service);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // GET: Services/Edit/1
        public async Task<ActionResult> Edit(int id)
        {
            var service = await _context.Service.FirstOrDefaultAsync(p => p.Id == id);
            var loggedInUser = await GetCurrentUserAsync();

            if (service.ApplicationUserId != loggedInUser.Id)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Service service)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                service.ApplicationUserId = user.Id;

                _context.Service.Update(service);
                await _context.SaveChangesAsync();
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // GET: Services/Delete/1
        public async Task<ActionResult> Delete(int id)
        {
            var loggedInUser = await GetCurrentUserAsync();
            var service = await _context.Service
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            if (service.ApplicationUserId != loggedInUser.Id)
            {
                return NotFound();
            }

            return View(service);
        }


        // POST: Services/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                var service = await _context.Service.FindAsync(id);
                _context.Service.Remove(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}