using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ABZVehicleInsuranceMvcProject.Models;
using Claim = ABZVehicleInsuranceMvcProject.Models.Claim;
using System.Data;

namespace ABZVehicleInsuranceMvcProject.Controllers
{
    public class ClaimController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://abzclaimwebapi-akshitha.azurewebsites.net/api/claim/") };
        static string token;

        public async Task<ActionResult> Index()
        {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            HttpClient client2 = new HttpClient();
            token = await client2.GetStringAsync("https://authenticationwebapi-akshitha.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey);
           // token = await client2.GetStringAsync("http://localhost:5042/api/Auth/" + userName + "/" + role + "/" + secretKey);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            // GET: ClaimController
            List<Claim> claims = await client.GetFromJsonAsync<List<Claim>>("");
            return View(claims);
        }

        // GET: ClaimController/Details/5
        public async Task<ActionResult> Details(string claimNo)
        {
            Claim claims = await client.GetFromJsonAsync<Claim>("" + claimNo);
            return View(claims);
        }

        // GET: ClaimController/Create
        public ActionResult Create()
        {
            ViewData["token"] = token;
            Claim claim = new Claim();
            return View(claim);
        }

        // POST: ClaimController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<ActionResult> Create(Claim claim)
        {
            try
            {
                await client.PostAsJsonAsync<Claim>(""+token, claim);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("Claim/Edit/{claimNo}")]
        // GET: ClaimController/Edit/5
        public async Task<ActionResult> Edit(string claimNo)
        {
            ViewData["token"] = token;
            Claim claim = await client.GetFromJsonAsync<Claim>("" + claimNo);
            return View(claim);
        }

        // POST: ClaimController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Claim/Edit/{claimNo}")]
        public async Task<ActionResult> Edit(string claimNo,Claim claim)
        {
            try
            {
                await client.PutAsJsonAsync<Claim>("" + claimNo, claim);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("Claim/Delete/{claimNo}")]
        // GET: ClaimController/Delete/5
        public async Task<ActionResult> Delete(string claimNo)
        {
            Claim claim=await client.GetFromJsonAsync<Claim>(""+claimNo);
            return View(claim);
        }

        // POST: ClaimController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Claim/Delete/{claimNo}")]
        public async Task<ActionResult> Delete(string claimNo, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync(""+claimNo);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> ByPolicy(string policyNo)
        {
            List<Claim> claims = await client.GetFromJsonAsync<List<Claim>>("ByPolicy/"+policyNo);
            return View(claims);
        }

    }
}
