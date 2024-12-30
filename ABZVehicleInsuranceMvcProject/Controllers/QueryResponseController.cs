using ABZVehicleInsuranceMvcProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ABZVehicleInsuranceMvcProject.Controllers
{
    public class QueryResponseController : Controller
    {
        // GET: QueryReponseController
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://abzcustomerquerywebapi-akshitha.azurewebsites.net/api/QueryResponse/") };
       // static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5091/api/QueryResponse/") };
        static string token;

        public async Task<ActionResult> Index()
        {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            HttpClient client2 = new HttpClient();
            token = await client2.GetStringAsync("https://authenticationwebapi-akshitha.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey);
         //   token = await client2.GetStringAsync("http://localhost:5042/api/Auth/" + userName + "/" + role + "/" + secretKey);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            List<QueryResponse> queries = await client.GetFromJsonAsync<List<QueryResponse>>("");
            return View(queries);
        }

        public async Task<ActionResult> Details(string queryID, string srNo)
        {
            QueryResponse query = await client.GetFromJsonAsync<QueryResponse>($"{queryID}/{srNo}");
            return View(query);
        }

        public async Task<ActionResult> Create()
        {
            ViewData["token"] = token;
            QueryResponse query = new QueryResponse();
            return View(query);
        }

        // POST: CustomerQueryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(QueryResponse queryresponse)
        {
            try
            {
                await client.PostAsJsonAsync<QueryResponse>("", queryresponse);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerQueryController/Edit/5
        [Route("QueryResponse/Edit/{queryID}/{srNo}")]
        public async Task<ActionResult> Edit(string queryID, string srNo)
        {
            QueryResponse query = await client.GetFromJsonAsync<QueryResponse>($"{queryID}/{srNo}");
            return View(query);
        }

        // POST: CustomerQueryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("QueryResponse/Edit/{queryID}/{srNo}")]
        public async Task<ActionResult> Edit(string queryID, string srNo, QueryResponse qr)
        {
            try
            {
                await client.PutAsJsonAsync<QueryResponse>($"{queryID}/{srNo}", qr);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerQueryController/Delete/5
        [Route("QueryResponse/Delete/{queryID}/{srNo}")]
        public async Task<ActionResult> Delete(string queryID, string srNo)
        {
            QueryResponse query = await client.GetFromJsonAsync<QueryResponse>($"{queryID}/{srNo}");
            return View(query);
        }

        // POST: CustomerQueryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("QueryResponse/Delete/{queryID}/{srNo}")]
        public async Task<ActionResult> Delete(string queryID, string srNo, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync($"{queryID}/{srNo}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> ByCustomerQuery(string queryID)
        {
            List<QueryResponse> qr = await client.GetFromJsonAsync<List<QueryResponse>>("ByCustomerQuery/" + queryID);
            return View(qr);
        }
        public async Task<ActionResult> ByAgent(string agentID)
        {
            List<QueryResponse> qr = await client.GetFromJsonAsync<List<QueryResponse>>("ByAgent/" + agentID);
            return View(qr);
        }
    }
}
