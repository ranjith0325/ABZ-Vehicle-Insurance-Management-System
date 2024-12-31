using System.Net.Http.Json;
using ABZVehicleInsuranceMvcProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace ABZVehicleInsuranceMvcProject.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://abzcustomerwebapi-akshitha.azurewebsites.net/api/customer/") };
        //static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5151/api/Customer/") };
        static string token;

        // GET: CustomerController
        public async Task<ActionResult> Index()
        {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            HttpClient client2 = new HttpClient();
            token = await client2.GetStringAsync("https://authenticationwebapi-akshitha.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey);
           // token = await client2.GetStringAsync("http://localhost:5042/api/Auth/" + userName + "/" + role + "/" + secretKey);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            
            List<Customer> customers = await client.GetFromJsonAsync<List<Customer>>("");
            return View(customers);
        }

        // GET: CustomerController/Details/5
        public async Task<ActionResult> Details(string customerId)
        {
            Customer customer = await client.GetFromJsonAsync<Customer>("" + customerId);
            return View(customer);
        }

        // GET: CustomerController/Create
        public async Task<ActionResult> Create()
        {
            ViewData["token"] = token;
            Customer customer = new Customer();
            return View(customer);
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer customer)
        {
            try
            {
                await client.PostAsJsonAsync<Customer>(""+token, customer);
                TempData["AlertMessage"] = "Created Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        [Route("Customer/Edit/{customerID}")]

        public async Task<ActionResult> Edit(string customerID)
        {
            ViewData["token"] = token;
            Customer customer = await client.GetFromJsonAsync<Customer>("" + customerID);
            return View(customer);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Customer/Edit/{customerId}")]
        public async Task<ActionResult> Edit(string customerId, Customer customer)
        {
            try
            {
                await client.PutAsJsonAsync<Customer>("" + customerId, customer);
                TempData["AlertMessage"] = "Edited Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        // GET: CustomerController/Delete/5
        [Route("Customer/Delete/{customerId}")]
        public async Task<ActionResult> Delete(string customerId)
        {
            Customer customer = await client.GetFromJsonAsync<Customer>("" + customerId);
            return View(customer);
        }
        [Authorize(Roles = "Admin")]
        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Customer/Delete/{customerId}")]
        public async Task<ActionResult> Delete(string customerId, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync("" + customerId);
                TempData["AlertMessage"] = "Deleted Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
