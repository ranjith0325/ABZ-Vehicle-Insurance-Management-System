using System.Buffers;
using System.Data;
using ABZVehicleInsuranceMvcProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABZVehicleInsuranceMvcProject.Controllers
{
    [Authorize]
    public class AgentController : Controller
    {
        // GET: AgentController
        //static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://abzagentwebapi-akshitha.azurewebsites.net/api/Agent/") };
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5002/AgentSvc/") };
        static string token;

        public async Task<ActionResult> Index(string searchBy,string searchValue)
        {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            HttpClient client2 = new HttpClient();
           // token = await client2.GetStringAsync("https://authenticationwebapi-akshitha.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey);
            token = await client2.GetStringAsync("http://localhost:5002/AuthSvc/" + userName + "/" + role + "/" + secretKey);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            List<Agent> agents = await client.GetFromJsonAsync<List<Agent>>("");
            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchValue))
            {
                return View(agents);
            }

            // Initialize search results
            IEnumerable<Agent> searchResults = null;

            // Search by AgentName
            if (searchBy.ToLower() == "agentname")
            {
                searchResults = agents.Where(a => a.AgentName != null && a.AgentName.ToLower().Contains(searchValue.ToLower())).ToList();
            }

            if (searchResults != null && searchResults.Any())
            {
                return View(searchResults); // Return filtered results
            }
            else
            {
                TempData["InfoMessage"] = "No matching agents found.";
                return View(agents); // Return all agents if no match is found
            }
            return View(agents);
        }

        // GET: AgentController/Details/5
        public async Task<ActionResult> Details(string agentId)
        {
            Agent agent = await client.GetFromJsonAsync<Agent>("" + agentId);
            return View(agent);
        }
        [Authorize(Roles = "Admin")]
        // GET: AgentController/Create
        public ActionResult Create()
        {
            ViewData["token"] = token;
            Agent agent = new Agent();
            return View(agent);
        }
        [Authorize(Roles = "Admin")]
        // POST: AgentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Agent agent)
        {
            try
            {
                await client.PostAsJsonAsync<Agent>(""+token, agent);
                TempData["AlertMessage"] = "Created Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("Agent/Edit/{agentId}")]
        // GET: AgentController/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string agentId)
        {
            ViewData["token"] = token;
            Agent agent = await client.GetFromJsonAsync<Agent>("" + agentId);
            return View(agent);
        }

        // POST: AgentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Agent/Edit/{agentId}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> Edit(string agentId, Agent agent)
        {
            try
            {
                await client.PutAsJsonAsync<Agent>("" + agentId, agent);
                TempData["AlertMessage"] = "Edited Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("Agent/Delete/{agentId}")]
        [Authorize(Roles = "Admin")]
        // GET: AgentController/Delete/5
        public async Task<ActionResult> Delete(string agentId)
        {
            Agent agent = await client.GetFromJsonAsync<Agent>("" + agentId);
            return View(agent);
        }

        // POST: AgentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Agent/Delete/{agentId}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> Delete(string agentId, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync("" + agentId);
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
