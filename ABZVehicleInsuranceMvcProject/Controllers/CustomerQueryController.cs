using ABZVehicleInsuranceMvcProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ABZVehicleInsuranceMvcProject.Controllers
{
    public class CustomerQueryController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://abzcustomerquerywebapi-akshitha.azurewebsites.net/CustomerQuery/") };
        //static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5091/api/CustomerQuery/") };
        static string token;
        // GET: CustomerQueryController
        public async Task<ActionResult> Index()
        {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            HttpClient client2 = new HttpClient();
            token = await client2.GetStringAsync("https://authenticationwebapi-akshitha.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey);
           // token = await client2.GetStringAsync("http://localhost:5042/api/Auth/" + userName + "/" + role + "/" + secretKey);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            List<CustomerQuery> cqs = await client.GetFromJsonAsync<List<CustomerQuery>>("");
            return View(cqs);
        }

        // GET: CustomerQueryController/Details/5
        public async Task<ActionResult> Details(string queryID)
        {
            CustomerQuery cq = await client.GetFromJsonAsync<CustomerQuery>(queryID);
            return View(cq);
        }

        // GET: CustomerQueryController/Create
        public ActionResult Create()
        {
            ViewData["token"] = token;
            CustomerQuery cq=new CustomerQuery();
            return View(cq);
        }

        // POST: CustomerQueryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerQuery customerQuery)
        {
            try
            {
                await client.PostAsJsonAsync<CustomerQuery>("", customerQuery);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("CustomerQuery/Edit/{queryID}")]
        // GET: CustomerQueryController/Edit/5
        public async Task<ActionResult> Edit(string queryID)
        {
            ViewData["token"] = token;
            CustomerQuery cq = await client.GetFromJsonAsync<CustomerQuery>("" + queryID);
            return View(cq);
        }

        // POST: CustomerQueryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CustomerQuery/Edit/{queryID}")]
        public async Task<ActionResult> Edit(string queryID, CustomerQuery cq)
        {
            try
            {
                await client.PutAsJsonAsync<CustomerQuery>(""+queryID, cq);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("CustomerQuery/Delete/{queryID}")]
        // GET: CustomerQueryController/Delete/5
        public async Task<ActionResult> Delete(string queryID)
        {
            CustomerQuery cq=await client.GetFromJsonAsync<CustomerQuery>(""+queryID);
            return View(cq);
        }

        // POST: CustomerQueryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CustomerQuery/Delete/{queryID}")]
        public async Task<ActionResult> Delete(string queryID, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync("" + queryID);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> ByCustomer(string customerID)
        {
            List<CustomerQuery> customerQueries = await client.GetFromJsonAsync<List<CustomerQuery>>("ByCustomer/" + customerID);
            return View(customerQueries);
        }
      
    }
}
