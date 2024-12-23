using System.Net.Http.Json;
using ABZVehicleInsuranceMvcProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABZVehicleInsuranceMvcProject.Controllers
{
    public class CustomerController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5151/api/Customer/") };
        // GET: CustomerController
        public async Task<ActionResult> Index()
        {
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
        public async Task<ActionResult> Create(string customerId)
        {
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
                await client.PostAsJsonAsync<Customer>("", customer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public async Task<ActionResult> Edit(string customerId)
        {
            Customer customer = await client.GetFromJsonAsync<Customer>("" + customerId);
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public async Task<ActionResult> Delete(string customerId)
        {
            Customer customer = await client.GetFromJsonAsync<Customer>("" + customerId);
            return View(customer);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Customer/Delete/{customerId}")]
        public async Task<ActionResult> Delete(string customerId, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync("" + customerId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
