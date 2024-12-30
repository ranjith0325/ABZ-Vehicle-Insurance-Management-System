﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ABZVehicleInsuranceMvcProject.Models;
using System.Runtime.InteropServices;


namespace ABZVehicleInsuranceMvcProject.Controllers
{
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
        public async Task<ActionResult> Create()
        {
            ViewData["token"] = token;
            Policy policy = new Policy();
            return View(policy);
        }

        // POST: PolicyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Policy policy)
        {
            try
            {
                await client.PostAsJsonAsync<Policy>(""+token, policy);
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
        public async Task<ActionResult> Delete(string policyNo)
        {
            Policy policy = await client.GetFromJsonAsync<Policy>("" + policyNo);
            return View(policy);
        }

        // POST: PolicyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Policy/Delete/{policyNo}")]
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