using System.Net.Http.Json;
using ABZVehicleInsuranceMvcProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace ABZVehicleInsuranceMvcProject.Controllers
{
    [Authorize]
    public class PolicyAddonController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://abzpolicywebapi-akshitha.azurewebsites.net/api/policyaddon/") };
       // static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5007/api/PolicyAddon/") };
        static string token;
        // GET: PolicyAddonController
        public async Task<ActionResult> Index( string pid)
        {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            HttpClient client2 = new HttpClient();
            token = await client2.GetStringAsync("https://authenticationwebapi-akshitha.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey);
          // token = await client2.GetStringAsync("http://localhost:5042/api/Auth/" + userName + "/" + role + "/" + secretKey);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            List<PolicyAddon> policyAddons = await client.GetFromJsonAsync<List<PolicyAddon>>("" + pid);
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
            ViewData["token"] = token;
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
                await client.PostAsJsonAsync<PolicyAddon>(""+token, policyaddon);
                TempData["AlertMessage"] = "Created Successfully.....!";
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
                TempData["AlertMessage"] = "Edited Successfully.....!";
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
                TempData["AlertMessage"] = "Deleted Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //public async task<actionresult> bypolicy(string policyno)
        //{
        //    list<policyaddon> policyaddons = await client.getfromjsonasync<list<policyaddon>>("bypolicy/" + policyno);
        //    return view(policyaddons);
        //}
    }
}
