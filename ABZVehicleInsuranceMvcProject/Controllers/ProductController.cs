﻿using ABZVehicleInsuranceMvcProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ABZVehicleInsuranceMvcProject.Controllers;
using System.Security.Cryptography;
using System.Net.Http.Json;

namespace ABZVehicleInsuranceMvcProject.Controllers
{

    public class ProductController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5145/api/Product/") };
        // GET: ProductController
        public async Task<ActionResult> Index()
        {
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
        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                await client.PostAsJsonAsync<Product>("",product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("Product/Edit/{productID}")]
        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(string productID)
        {
            Product product = await client.GetFromJsonAsync<Product>("" + productID);
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Product/Edit/{productID}")]
        public async Task<ActionResult> Edit(string productID, Product product)
        {
            try
            {
                await client.PutAsJsonAsync<Product>(""+productID,product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        [Route("Product/Delete/{productID}")]
        public async Task<ActionResult> Delete(string productID)
        {
           Product product = await client.GetFromJsonAsync<Product>("" + productID);
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Product/Delete/{productID}")]
        public async Task<ActionResult> Delete(string productID, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync("" + productID);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
