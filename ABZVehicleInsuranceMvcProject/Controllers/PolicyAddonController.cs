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
            List<PolicyAddon> policyAddons = await client.GetFromJsonAsync<List<PolicyAddon>>("");
            return View(policyAddons);
        }

        // GET: PolicyAddonController/Details/5
        public async Task<ActionResult> Details(string policyNo,string addonId)
        {
            PolicyAddon policyAddon = await client.GetFromJsonAsync<PolicyAddon>($"{policyNo}/{addonId}");
            return View(policyAddon);
        }

        // GET: PolicyAddonController/Create
        public ActionResult Create()
        {
            PolicyAddon policyAddon = new PolicyAddon();
            return View(policyAddon);
        }

        // POST: PolicyAddonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PolicyAddon policyaddon)
        {
            try
            {
                await client.PostAsJsonAsync<PolicyAddon>("", policyaddon);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PolicyAddonController/Edit/5
        [Route("PolicyAddon/Edit/{policyNo}/{addonId}")]
        public async Task<ActionResult> Edit(string policyNo, string addonId)
        {
            PolicyAddon policyAddon = await client.GetFromJsonAsync<PolicyAddon>($"{policyNo}/{addonId}");
            return View(policyAddon);
        }

        // POST: PolicyAddonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("PolicyAddon/Edit/{policyNo}/{addonId}")]
        public async Task<ActionResult> Edit(string policyNo,string addonId, PolicyAddon policyaddon)
        {
            try
            {
                await client.PutAsJsonAsync($"{policyNo}/{addonId}/",policyaddon);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PolicyAddonController/Delete/5
        [Route("PolicyAddon/Delete/{policyNo}/{addonId}")]
        public async Task<ActionResult> Delete(string policyNo, string addonId)
        {
            PolicyAddon policyaddon = await client.GetFromJsonAsync<PolicyAddon>($"{policyNo}/{addonId}");
            return View(policyaddon);
        }

        // POST: PolicyAddonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("PolicyAddon/Delete/{policyNo}/{addonId}")]
        public async Task<ActionResult> Delete(string policyNo, string addonId, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync($"{policyNo}/{addonId}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> BYPolicy(string policyNo)
        {
            List<PolicyAddon> policyAddons = await client.GetFromJsonAsync<List<PolicyAddon>>("ByPolicy/" + policyNo);
            return View(policyAddons);
        }
    }
}
