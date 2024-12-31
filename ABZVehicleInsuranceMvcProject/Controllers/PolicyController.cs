using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ABZVehicleInsuranceMvcProject.Models;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ABZVehicleInsuranceMvcProject.Controllers
{
    [Authorize]
    public class PolicyController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://abzpolicywebapi-akshitha.azurewebsites.net/api/policy/") };
        //static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5007/api/Policy/") };
        static string token;

        // GET: PolicyController
        public async Task<ActionResult> Index()
        {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            HttpClient client2 = new HttpClient();
            token = await client2.GetStringAsync("https://authenticationwebapi-akshitha.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey);
            // token = await client2.GetStringAsync("http://localhost:5042/api/Auth/" + userName + "/" + role + "/" + secretKey);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            List<Policy> policies = await client.GetFromJsonAsync<List<Policy>>("");
            return View(policies);
        }

        // GET: PolicyController/Details/5
        public async Task<ActionResult> Details(string policyNo)
        {
            Policy policy = await client.GetFromJsonAsync<Policy>("" + policyNo);
            return View(policy);
        }

        // GET: PolicyController/Create
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            ViewData["token"] = token;
            List<SelectListItem> fuelTypes = new List<SelectListItem>
             {
                 new SelectListItem { Text = "Cash", Value = "C" },
                 new SelectListItem { Text = "Cheque", Value = "Q" },
                 new SelectListItem { Text = "Credit or Debit", Value = "U" },
                 new SelectListItem { Text = "Digital Payment", Value = "D" }
              };

            // Passing the fuelTypes list to the View using ViewBag
            ViewBag.FuelTypes = fuelTypes;
            Policy policy = new Policy();
            return View(policy);
        }

        // POST: PolicyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(Policy policy)
        {
            try
            {
                await client.PostAsJsonAsync<Policy>("" + token, policy);
                TempData["AlertMessage"] = "Created Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PolicyController/Edit/5
        [Route("Policy/Edit/{policyNo}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string policyNo)
        {
            ViewData["token"] = token;
            Policy policy = await client.GetFromJsonAsync<Policy>("" + policyNo);
            return View(policy);
        }

        // POST: PolicyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Policy/Edit/{policyNo}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string policyNo, Policy policy)
        {
            try
            {
                await client.PutAsJsonAsync<Policy>("" + policyNo, policy);
                TempData["AlertMessage"] = "Edited Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PolicyController/Delete/5
        [Route("Policy/Delete/{policyNo}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string policyNo)
        {
            Policy policy = await client.GetFromJsonAsync<Policy>("" + policyNo);
            return View(policy);
        }

        // POST: PolicyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Policy/Delete/{policyNo}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string policyNo, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync("" + policyNo);
                TempData["AlertMessage"] = "Deleted Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> ByProposal(string proposalNo)
        {
            List<Proposal> proposals = await client.GetFromJsonAsync<List<Proposal>>("ByProposal/" + proposalNo);
            return View(proposals);
        }

    }
}