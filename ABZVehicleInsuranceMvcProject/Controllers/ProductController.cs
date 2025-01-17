﻿using ABZVehicleInsuranceMvcProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ABZVehicleInsuranceMvcProject.Controllers;
using System.Security.Cryptography;
using System.Net.Http.Json;
using NuGet.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ABZVehicleInsuranceMvcProject.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        //static HttpClient client = new HttpClient() { BaseAddress = new Uri("https://abzproductwebapi-akshitha.azurewebsites.net/api/product/") };
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5002/ProductSvc/") };
        static string token;

        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            string userName = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            HttpClient client2 = new HttpClient();
           // token = await client2.GetStringAsync("https://authenticationwebapi-akshitha.azurewebsites.net/api/Auth/" + userName + "/" + role + "/" + secretKey);
            token = await client2.GetStringAsync("http://localhost:5002/AuthSvc/" + userName + "/" + role + "/" + secretKey);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            

            List<Product> products = await client.GetFromJsonAsync<List<Product>>("");
            return View(products);
        }


        
        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(string productID)
        {
            Product product = await client.GetFromJsonAsync<Product>("" + productID);
            return View(product);
        }

        // GET: ProductController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewData["token"] = token;
            List<SelectListItem> fuelTypes = new List<SelectListItem>
 {
     new SelectListItem { Text = "private car", Value = "private car" },
     new SelectListItem { Text = "public car", Value = "public car" }
  };

            // Passing the fuelTypes list to the View using ViewBag
            ViewBag.FuelTypes = fuelTypes;
            Product product = new Product();
            return View(product);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                await client.PostAsJsonAsync<Product>(""+token,product);
                TempData["AlertMessage"] = "Created Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("Product/Edit/{productID}")]
        // GET: ProductController/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string productID)
        {
            ViewData["token"] = token;
            Product product = await client.GetFromJsonAsync<Product>("" + productID);
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Product/Edit/{productID}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> Edit(string productID, Product product)
        {
            try
            {
                await client.PutAsJsonAsync<Product>(""+productID,product);
                TempData["AlertMessage"] = "Edited Successfully.....!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        [Route("Product/Delete/{productID}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> Delete(string productID)
        {
           Product product = await client.GetFromJsonAsync<Product>("" + productID);
            return View(product);
        }
        [Authorize(Roles ="Admin")]
        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Product/Delete/{productID}")]

        public async Task<ActionResult> Delete(string productID, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync("" + productID);
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
