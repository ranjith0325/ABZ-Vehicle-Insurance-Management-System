using System.Net.Http.Json;
using ABZVehicleInsuranceMvcProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABZVehicleInsuranceMvcProject.Controllers
{
    [Authorize]
    public class ProposalController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://abzproposalwebapi-akshitha.azurewebsites.net/api/proposal/") };
       // static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5273/api/Proposal/") };
        static string token;

        // GET: ProposalMvcController
        public async Task<ActionResult> Index()
        {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            HttpClient client2 = new HttpClient();
            token = await client2.GetStringAsync("https://authenticationwebapi-akshitha.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey);
           // token = await client2.GetStringAsync("http://localhost:5042/api/Auth/" + userName + "/" + role + "/" + secretKey);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Proposal> proposals = await client.GetFromJsonAsync<List<Proposal>>("");
            return View(proposals);
        }

        // GET: ProposalMvcController/Details/5
        public async Task<ActionResult> Details(string proposalNo)
        {
            Proposal proposal = await client.GetFromJsonAsync<Proposal>("" + proposalNo);
            return View(proposal);
        }

        // GET: ProposalMvcController/Create
        
        public ActionResult Create()
        {
            ViewData["token"] = token;
            Proposal proposal = new Proposal();
            return View(proposal);
        }

        // POST: ProposalMvcController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Proposal proposal)
        {
            try
            {
                await client.PostAsJsonAsync<Proposal>(""+token,proposal);
                TempData["AlertMessage"] = "Created Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProposalMvcController/Edit/5
        [Route("Proposal/Edit/{proposalNo}")]
        public async Task<ActionResult> Edit(string proposalNo)
        {
            ViewData["token"] = token;
            Proposal proposal = await client.GetFromJsonAsync<Proposal>("" + proposalNo);
            return View(proposal);
        }

        // POST: ProposalMvcController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Proposal/Edit/{proposalNo}")]
        public async Task<ActionResult> Edit(string proposalNo, Proposal proposal)
        {
            try
            {
                await client.PutAsJsonAsync<Proposal>("" + proposalNo, proposal);
                TempData["AlertMessage"] = "Edited Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProposalMvcController/Delete/5
        [Route("Proposal/Delete/{proposalNo}")]
        public async Task<ActionResult> Delete(string proposalNo)
        {
            Proposal proposal = await client.GetFromJsonAsync<Proposal>("" + proposalNo);
            return View(proposal);
        }

        // POST: ProposalMvcController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Proposal/Delete/{proposalNo}")]
        public async Task<ActionResult> Delete(string proposalNo, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync("" + proposalNo);
                TempData["AlertMessage"] = "Deleted Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            { 
                return View();
            }
        }
        public async Task<ActionResult> ByVehicle(string regNo)
        {
            List<Proposal> vehicles = await client.GetFromJsonAsync<List<Proposal>>("ByVehicle/" + regNo);
            return View(vehicles);
        }
        public async Task<ActionResult> ByProduct(string productId)
        {
            List<Proposal> products = await client.GetFromJsonAsync<List<Proposal>>("ByProduct/" + productId);
            return View(products);
        }
        public async Task<ActionResult> ByCustomer(string customerId)
        {
            List<Proposal> customers = await client.GetFromJsonAsync<List<Proposal>>("ByCustomer/" + customerId);
            return View(customers);
        }
        public async Task<ActionResult> ByAgent(string agentId)
        {
            List<Proposal> agents = await client.GetFromJsonAsync<List<Proposal>>("ByAgent/" + agentId);
            return View(agents);
        }
    }
}
