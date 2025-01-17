﻿using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ABZVehicleInsuranceMvcProject.Models;
using Claim = ABZVehicleInsuranceMvcProject.Models.Claim;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ABZVehicleInsuranceMvcProject.Controllers
{
    [Authorize]
    public class ClaimController : Controller
    {
        //static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://abzclaimwebapi-akshitha.azurewebsites.net/api/claim/") };
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5002/ClaimSvc/") };
        static string token;

        public async Task<ActionResult> Index()
        {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            HttpClient client2 = new HttpClient();
            //token = await client2.GetStringAsync("https://authenticationwebapi-akshitha.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey);
            token = await client2.GetStringAsync("http://localhost:5002/AuthSvc/" + userName + "/" + role + "/" + secretKey);
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
            List<SelectListItem> fuelTypes = new List<SelectListItem>
             {
                new SelectListItem { Text = "Submitted", Value = "S" },
                new SelectListItem { Text = "Approved", Value = "A" },
                new SelectListItem { Text = "Rejected", Value = "R" },
                new SelectListItem { Text = "Terminated", Value = "T" }
             };

            // Passing the fuelTypes list to the View using ViewBag
            ViewBag.FuelTypes = fuelTypes;
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
                await client.PostAsJsonAsync<Claim>("" + token, claim);
                TempData["AlertMessage"] = "Created Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("Claim/Edit/{claimNo}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string claimNo,Claim claim)
        {
            try
            {
                await client.PutAsJsonAsync<Claim>("" + claimNo, claim);
                TempData["AlertMessage"] = "Edited Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("Claim/Delete/{claimNo}")]
        [Authorize(Roles = "Admin")]
        // GET: ClaimController/Delete/5
        public async Task<ActionResult> Delete(string claimNo)
        {
            Claim claim = await client.GetFromJsonAsync<Claim>("" + claimNo);
            return View(claim);
        }

        // POST: ClaimController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Claim/Delete/{claimNo}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string claimNo, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync("" + claimNo);
                TempData["AlertMessage"] = "Deleted Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> ByPolicy(string policyNo)
        {
            List<Claim> claims = await client.GetFromJsonAsync<List<Claim>>("ByPolicy/" + policyNo);
            return View(claims);
        }

    }
}
