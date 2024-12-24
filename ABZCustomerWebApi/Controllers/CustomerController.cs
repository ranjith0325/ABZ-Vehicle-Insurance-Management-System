﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ABZCustomerLibrary.Models;
using ABZCustomerLibrary.RepoAsync;
using System.Net.Http.Json;

namespace ABZCustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerRepoAsync custRepo;
        public CustomerController(ICustomerRepoAsync repo)
        {
            custRepo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Customer> customer = await custRepo.GetAllCustomersAsync();
            return Ok(customer);
        }
        [HttpGet("{customerId}")]
        public async Task<ActionResult> GetOne(string customerId)
        {
            try
            {
                Customer cust = await custRepo.GetCustomerByIdAsync(customerId);
                return Ok(cust);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Insert(Customer customer)
        {
            try
            {
                await custRepo.InsertCustomerAsync(customer);
                HttpClient client = new HttpClient();
               // await client.PostAsJsonAsync("http://localhost:5273/api/Proposal/Customer", new {CustomerID=customer.CustomerID});
               // await client.PostAsJsonAsync("http://localhost:5083/api/Vehicle/Customer",new { CustomerID = customer.CustomerID });
                await client.PostAsJsonAsync("http://abzvehiclewebapi-mani.azurewebsites.net/api/Vehicle/Customer", new { CustomerId = customer.CustomerID });
                await client.PostAsJsonAsync("http://abzproposalwebapi-mani.azurewebsites.net/api/Proposal/Customer", new { CustomerId = customer.CustomerID });

                return Created($"api/Customer/{customer.CustomerID}", customer);
                
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("{customerId}")]
        public async Task<ActionResult> Update(string customerId, Customer customer)
        {
            try
            {
                await custRepo.UpdateCustomerAsync(customerId, customer);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{customerId}")]
        public async Task<ActionResult> Delete(string customerId)
        {
            try
            {
                await custRepo.DeleteCustomerAsync(customerId);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
