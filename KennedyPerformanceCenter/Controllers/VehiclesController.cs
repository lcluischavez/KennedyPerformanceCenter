using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KennedyPerformanceCenter.Data;
using KennedyPerformanceCenter.Models;
using KennedyPerformanceCenter.Models.ViewModels;

namespace KennedyPerformanceCenter.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public VehiclesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)

        {

            _context = context;
            _userManager = userManager;

        }


        // GET: Vehicles
        public async Task<ActionResult> Index(string searchString, string filter)
        {
            var user = await GetCurrentUserAsync();
            var vehicles = await _context.Vehicle
                .Include(tdi => tdi.ApplicationUser)
                .ToListAsync();

            switch (filter)
            {
                case "N54":
                    vehicles = await _context.Vehicle
                        //.Where(ti => ti.UserId == user.Id)
                        .Where(ti => ti.EngineTypeId == 1)
                        //.Where(p => p.Quantity > 0)
                        .Include(ti => ti.EngineType)
                        .ToListAsync();
                    break;
                case "N55":
                    vehicles = await _context.Vehicle
                        .Where(ti => ti.EngineTypeId == 2)
                        .Include(ti => ti.EngineType)
                        .ToListAsync();
                    break;
                case "Other":
                   vehicles = await _context.Vehicle
                        .Where(ti => ti.EngineTypeId == 3 )
                        .Include(ti => ti.EngineType)
                        .ToListAsync();
                    break;
                case "All":
                    vehicles = await _context.Vehicle
                        .Include(ti => ti.EngineType)
                        .ToListAsync();
                    break;
                default:
                    vehicles = await _context.Vehicle
                        .Include(ti => ti.EngineType)
                        .ToListAsync();
                    break;
            }

            if (searchString != null)
            {
                var filteredVehicles = _context.Vehicle.Where(s => s.Make.Contains(searchString) || s.Model.Contains(searchString));
                return View(filteredVehicles);
            };

            return View(vehicles);
        }


        // GET: Vehicles/Details/1
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiclee = await _context.Vehicle
               //.Where(p => p.UserId == user.Id)
               .Include(p => p.ApplicationUser)
               .Include(p => p.EngineType)
               .Include(p => p.TransmissionType)
               .Include(p => p.DrivetrainType)
               .FirstOrDefaultAsync(p => p.Id == id);

            if (vehiclee == null)
            {
                return NotFound();
            }

            return View(vehiclee);
        }



        // GET: Vehicles/Create
        public async Task<ActionResult> Create()
        {
            var drivetrainTypeOptions = await _context.DrivetrainType.Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.Id.ToString()
            })
                .ToListAsync();
            var engineTypeOptions = await _context.EngineType.Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.Id.ToString()
            })
                .ToListAsync();
            var transmissionTypeOptions = await _context.TransmissionType.Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.Id.ToString()
            })
                .ToListAsync();
            var viewModel = new VehicleFormViewModel();
            viewModel.DrivetrainTypeOptions = drivetrainTypeOptions;
            viewModel.EngineTypeOptions = engineTypeOptions;
            viewModel.TransmissionTypeOptions = transmissionTypeOptions;
            return View(viewModel);
        }


        // POST: Vehicles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VehicleFormViewModel vehicleFormView)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                var vehicles = new Vehicle()
                {
                    Model = vehicleFormView.Model,
                    Make = vehicleFormView.Make,
                    Year = vehicleFormView.Year,
                    Price = vehicleFormView.Price,
                    Color = vehicleFormView.Color,
                    Miles = vehicleFormView.Miles,
                    VIN = vehicleFormView.VIN,
                    StockNumber = vehicleFormView.StockNumber,
                    Description = vehicleFormView.Description,
                    ImagePath = vehicleFormView.ImagePath,
                    ApplicationUserId = user.Id,
                    EngineTypeId = vehicleFormView.EngineTypeId,
                    TransmissionTypeId = vehicleFormView.TransmissionTypeId,
                    DrivetrainTypeId = vehicleFormView.DrivetrainTypeId,
                };


                _context.Vehicle.Add(vehicles);
                await _context.SaveChangesAsync();

                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // GET: Vehicles/Edit/1
        public async Task<ActionResult> Edit(int id)
        {
            var loggedInUser = await GetCurrentUserAsync();
            var engineTypes = await _context.EngineType.Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.Id.ToString()
            })
               .ToListAsync();
            var transmissionTypes = await _context.TransmissionType.Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.Id.ToString()
            })
               .ToListAsync();
            var drivetrainTypes = await _context.DrivetrainType.Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.Id.ToString()
            })
               .ToListAsync();

            var vehicle = await _context.Vehicle.FirstOrDefaultAsync(p => p.Id == id);
            var viewModel = new VehicleFormViewModel()
            {
                Make = vehicle.Make,
                Model = vehicle.Model,
                Price = vehicle.Price,
                Year = vehicle.Year,
                Color = vehicle.Color,
                Miles = vehicle.Miles,
                VIN = vehicle.VIN,
                StockNumber = vehicle.StockNumber,
                Description = vehicle.Description,
                ImagePath = vehicle.ImagePath,
                EngineTypeId = vehicle.EngineTypeId,
                EngineTypeOptions = engineTypes,
                TransmissionTypeId = vehicle.TransmissionTypeId,
                TransmissionTypeOptions = transmissionTypes,
                DrivetrainTypeId = vehicle.DrivetrainTypeId,
                DrivetrainTypeOptions = drivetrainTypes,
            };

            if (vehicle.ApplicationUserId != loggedInUser.Id)
            {
                return NotFound();
            }

            return View(viewModel);
        }


        // POST: Vehicles/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, VehicleFormViewModel vehicleFormView)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                var vehicles = new Vehicle()
                {
                    Id = id,
                    Make = vehicleFormView.Make,
                    Model = vehicleFormView.Model,
                    Year = vehicleFormView.Year,
                    Price = vehicleFormView.Price,
                    Color = vehicleFormView.Color,
                    Miles = vehicleFormView.Miles,
                    VIN = vehicleFormView.VIN,
                    StockNumber = vehicleFormView.StockNumber,
                    Description = vehicleFormView.Description,
                    ImagePath = vehicleFormView.ImagePath,
                    ApplicationUserId = user.Id,
                    EngineTypeId = vehicleFormView.EngineTypeId,
                    TransmissionTypeId = vehicleFormView.TransmissionTypeId,
                    DrivetrainTypeId = vehicleFormView.DrivetrainTypeId,
                };

                _context.Vehicle.Update(vehicles);
                await _context.SaveChangesAsync();
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // GET: Vehicles/Delete/1
        public async Task<ActionResult> Delete(int id)
        {
            var loggedInUser = await GetCurrentUserAsync();
            var vehicle = await _context.Vehicle
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            if (vehicle.ApplicationUserId != loggedInUser.Id)
            {
                return NotFound();
            }

            return View(vehicle);
        }


        // POST: Vehicles/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                var vehicle = await _context.Vehicle.FindAsync(id);
                _context.Vehicle.Remove(vehicle);
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