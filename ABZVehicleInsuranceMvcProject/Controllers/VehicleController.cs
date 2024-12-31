using System.Net.Http.Json;
using ABZVehicleInsuranceMvcProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Common;

namespace ABZVehicleInsuranceMvcProject.Controllers
{
    [Authorize]
    public class VehicleController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://abzvehiclewebapi-akshitha.azurewebsites.net/api/vehicle/") };
       // static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5083/api/Vehicle/") };
        static string token;
        // GET: VehicleController
        public async Task<ActionResult> Index(string selectedFuelType)
        {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            HttpClient client2 = new HttpClient();
            token = await client2.GetStringAsync("https://authenticationwebapi-akshitha.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey);
         //  token = await client2.GetStringAsync("http://localhost:5042/api/Auth/" + userName + "/" + role + "/" + secretKey);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            List<Vehicle> vehicles = await client.GetFromJsonAsync<List<Vehicle>>("");
            ViewBag.FuelTypes = new List<SelectListItem>
 {
     new SelectListItem { Text = "Petrol", Value = "P" },
     new SelectListItem { Text = "Diesel", Value = "D" },
     new SelectListItem { Text = "CNG", Value = "C" },
     new SelectListItem { Text = "LPG", Value = "L" },
     new SelectListItem { Text = "Electric", Value = "E" }
 };

            // Filter vehicles based on fuel type
            if (!string.IsNullOrEmpty(selectedFuelType))
            {
                vehicles = vehicles.Where(v => v.FuelType == selectedFuelType).ToList();
            }
            return View(vehicles);
        }

        // GET: VehicleController/Details/5
        public async Task<ActionResult> Details(string regNo)
        {
            Vehicle vehicle = await client.GetFromJsonAsync<Vehicle>("" + regNo);
            return View(vehicle);
        }
        [Authorize(Roles = "Admin")]
        // GET: VehicleController/Create
        public async Task<ActionResult> Create()
        {
            ViewData["token"] = token;
            List<SelectListItem> fuelTypes = new List<SelectListItem>
  {
      new SelectListItem { Text = "Petrol", Value = "P" },
      new SelectListItem { Text = "Diesel", Value = "D" },
      new SelectListItem { Text = "CNG", Value = "C" },
      new SelectListItem { Text = "LPG", Value = "L" },
      new SelectListItem { Text = "Electric", Value = "E" }
   };

            // Passing the fuelTypes list to the View using ViewBag
            ViewBag.FuelTypes = fuelTypes;
            Vehicle vehicle = new Vehicle();
            return View(vehicle);
        }
        [Authorize(Roles = "Admin")]
        // POST: VehicleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Vehicle vehicle)
        {
            try
            {
                await client.PostAsJsonAsync<Vehicle>("" + token, vehicle);
                TempData["AlertMessage"] = "Created Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        [Route("Vehicle/Edit/{regNo}")]

        // GET: VehicleController/Edit/5
        public async Task<ActionResult> Edit(string regNo)
        {
            ViewData["token"] = token;
            Vehicle vehicle = await client.GetFromJsonAsync<Vehicle>("" + regNo);
            return View(vehicle);
        }
        [Authorize(Roles = "Admin")]
        // POST: VehicleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Vehicle/Edit/{regNo}")]

        public async Task<ActionResult> Edit(string regNo, Vehicle vehicle)
        {
            try
            {
                await client.PutAsJsonAsync<Vehicle>("" + regNo, vehicle);
                TempData["AlertMessage"] = "Edited Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        [Route("Vehicle/Delete/{regNo}")]

        // GET: VehicleController/Delete/5
        public async Task<ActionResult> Delete(string regNo)
        {
            Vehicle vehicle = await client.GetFromJsonAsync<Vehicle>("" + regNo);
            return View(vehicle);
        }
        [Authorize(Roles = "Admin")]
        // POST: VehicleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Vehicle/Delete/{regNo}")]

        public async Task<ActionResult> Delete(string regNo, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync("" + regNo);
                TempData["AlertMessage"] = "Deleted Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> ByCustomer(string customerId)
        {
            List<Vehicle> vehicles = await client.GetFromJsonAsync<List<Vehicle>>("ByCustomer/" + customerId);
            return View(vehicles);
        }
    }
}
