using System.Net.Http.Json;
using ABZVehicleInsuranceMvcProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABZVehicleInsuranceMvcProject.Controllers
{
    public class PolicyAddonController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5007/api/PolicyAddon/") };
        // GET: PolicyAddonController
        public async Task<ActionResult> Index()
        {
            List<Policy> policies = await client.GetFromJsonAsync<List<Policy>>("");
            return View(policies);
        }

        // GET: PolicyAddonController/Details/5
        public async Task<ActionResult> Details(string policyId)
        {
            Policy policy = await client.GetFromJsonAsync<Policy>("" + policyId);
            return View(policy);
        }

        // GET: PolicyAddonController/Create
        public ActionResult Create()
        {
            Policy policy = new Policy();
            return View(policy);
        }

        // POST: PolicyAddonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Policy policy)
        {
            try
            {
                await client.PostAsJsonAsync<Policy>("", policy);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PolicyAddonController/Edit/5
        [Route("Policy/Edit/{policyId}")]
        public async Task<ActionResult> Edit(string policyId)
        {
            Policy policy = await client.GetFromJsonAsync<Policy>("" + policyId);
            return View(policy);
        }

        // POST: PolicyAddonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Policy/Edit/{policyId}")]
        public async Task<ActionResult> Edit(string policyId, Policy policy)
        {
            try
            {
                await client.PutAsJsonAsync("" + policyId, policy);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PolicyAddonController/Delete/5
        [Route("Policy/Delete/{policyId}")]
        public async Task<ActionResult> Delete(string policyId)
        {
            Policy policy = await client.GetFromJsonAsync<Policy>("" + policyId);
            return View(policy);
        }

        // POST: PolicyAddonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Policy/Delete/{policyId}")]
        public async Task<ActionResult> Delete(string policyId, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync("" + policyId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> BYPolicy(string policyId)
        {
            List<Policy> policies = await client.GetFromJsonAsync<List<Policy>>("BYPolicy/" + policyId);
            return View(policies);
        }
    }
}
