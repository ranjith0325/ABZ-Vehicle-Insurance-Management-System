﻿using ABZVehicleInsuranceMvcProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace ABZVehicleInsuranceMvcProject.Controllers
{
    public class ProductAddonController : Controller
    {
        // GET: ProductAddonController
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5145/api/ProductAddon/") };
        static string token;
        public async Task<ActionResult> Index(string pid)
        {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            HttpClient client2 = new HttpClient();
            //token = await client2.GetStringAsync("https://authenticationwebapi-snrao.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey);
            token = await client2.GetStringAsync("http://localhost:5042/api/Auth/" + userName + "/" + role + "/" + secretKey);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<ProductAddon> productaddons = await client.GetFromJsonAsync<List<ProductAddon>>("" + pid);
            return View(productaddons);
        }

        // GET: ProductAddonController/Details/5
        public async Task<ActionResult> Details(string productID,string addonId)
        {
            ProductAddon productaddon = await client.GetFromJsonAsync<ProductAddon>($"{productID}/{addonId}");
            return View(productaddon);
        }

        // GET: ProductAddonController/Create
        public ActionResult Create()
        {
            ViewData["token"] = token;
            ProductAddon productaddon = new ProductAddon();
            return View(productaddon);
        }

        // POST: ProductAddonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductAddon productaddon)
        {
            try
            {
                await client.PostAsJsonAsync<ProductAddon>(""+token, productaddon);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("ProductAddon/Edit/{productID}/{addonId}")]

        // GET: ProductAddonController/Edit/5

        public async Task<ActionResult> Edit(string productID,string addonId)
        {
            ProductAddon productaddon = await client.GetFromJsonAsync<ProductAddon>($"{productID}/{addonId}/");
            return View(productaddon);
        }

        // POST: ProductAddonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ProductAddon/Edit/{productID}/{addonId}")]


        public async Task<ActionResult> Edit(string productID,string addonId, ProductAddon productaddon)
        {
            try
            {
                await client.PutAsJsonAsync<ProductAddon>($"{productID}/{addonId}/", productaddon);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductAddonController/Delete/5
        [Route("ProductAddon/Delete/{productID}/{addonId}")]

        public async Task<ActionResult> Delete(string productID,string addonId)
        {
            ProductAddon productaddon = await client.GetFromJsonAsync<ProductAddon>($"{productID}/{addonId}");
            return View(productaddon);
        }

        // POST: ProductAddonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ProductAddon/Delete/{productID}/{addonId}")]

        public async Task<ActionResult> Delete(string productID,string addonId, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync($"{productID}/{addonId}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //public async Task<ActionResult> ByProduct(string productId)
        //{
        //    List<ProductAddon> productAddons = await client.GetFromJsonAsync<List<ProductAddon>>("ByProduct/" + productId);
        //    return View(productAddons);
        //}
    }
}
