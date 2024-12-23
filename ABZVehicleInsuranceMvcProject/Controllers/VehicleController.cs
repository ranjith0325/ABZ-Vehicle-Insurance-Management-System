using System.Net.Http.Json;
using ABZVehicleInsuranceMvcProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABZVehicleInsuranceMvcProject.Controllers
{
    public class VehicleController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5083/api/Vehicle/") };
        // GET: VehicleController
        public async Task<ActionResult> Index()
        {
            List<Vehicle> vehicles = await client.GetFromJsonAsync<List<Vehicle>>("");
            return View(vehicles);
        }

        // GET: VehicleController/Details/5
        public async Task<ActionResult> Details(string regNo)
        {
            Vehicle vehicle = await client.GetFromJsonAsync<Vehicle>(""+regNo);
            return View(vehicle);
        }

        // GET: VehicleController/Create
        public async Task<ActionResult> Create()
        {
            Vehicle vehicle=new Vehicle();
            return View(vehicle);
        }

        // POST: VehicleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Vehicle vehicle)
        {
            try
            {
                await client.PostAsJsonAsync<Vehicle>("",vehicle);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("Vehicle/Edit/{regNo}")]
        // GET: VehicleController/Edit/5
        public async Task<ActionResult> Edit(string regNo)
        {
            Vehicle vehicle = await client.GetFromJsonAsync<Vehicle>(""+ regNo);
            return View(vehicle);
        }

        // POST: VehicleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Vehicle/Edit/{regNo}")]
        public async Task<ActionResult> Edit(string regNo, Vehicle vehicle)
        {
            try
            { 
                await client.PutAsJsonAsync<Vehicle>(""+regNo,vehicle);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("Vehicle/Delete/{regNo}")]
        // GET: VehicleController/Delete/5
        public async Task<ActionResult> Delete(string regNo)
        {
            Vehicle vehicle = await client.GetFromJsonAsync<Vehicle>("" + regNo);
            return View(vehicle);
        }

        // POST: VehicleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Vehicle/Delete/{regNo}")]
        public async Task<ActionResult> Delete(string regNo, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync(""+regNo);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
