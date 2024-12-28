using System.Net.Http.Json;
using ABZVehicleInsuranceMvcProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace ABZVehicleInsuranceMvcProject.Controllers
{
    public class VehicleController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://abzvehiclewebapi-akshitha.azurewebsites.net/api/vehicle/") };
       // static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5083/api/Vehicle/") };
        static string token;
        // GET: VehicleController
        public async Task<ActionResult> Index()
        {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            HttpClient client2 = new HttpClient();
            token = await client2.GetStringAsync("https://authenticationwebapi-akshitha.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey);
         //  token = await client2.GetStringAsync("http://localhost:5042/api/Auth/" + userName + "/" + role + "/" + secretKey);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            
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
            ViewData["token"] = token;
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
                await client.PostAsJsonAsync<Vehicle>(""+token,vehicle);
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
            ViewData["token"] = token;
            Vehicle vehicle = await client.GetFromJsonAsync<Vehicle>(""+regNo);
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
        public async Task<ActionResult> ByCustomer(string customerId)
        {
            List<Vehicle> vehicles = await client.GetFromJsonAsync<List<Vehicle>>("ByCustomer/" + customerId);
            return View(vehicles);
        }
    }
}
