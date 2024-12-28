using ABZCustomerQueryLibrary.Repo;
using ABZCustomerQueryLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ABZCustomerQueryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerQueryController : ControllerBase
    {
        ICustomerQueryRepoAsync cqRepo;
        public CustomerQueryController(ICustomerQueryRepoAsync repo)
        {
            cqRepo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<CustomerQuery> cqr=await cqRepo.GetAllCustomerQuerysAsync();
            return Ok(cqr);
        }
        [HttpGet("{queryID}")]
        public async Task<ActionResult> GetOne(string queryID)
        {
            try
            {
                CustomerQuery cqr = await cqRepo.GetCustomerQueryAsync(queryID);
                return Ok(cqr);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByCustomer/{customerID}")]
        public async Task<ActionResult> GetByCustomer(string customerID)
        {
            try
            {
                List<CustomerQuery> cqs = await cqRepo.GetCustomerQueryByCustomerAsync(customerID);
                return Ok(cqs);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpPost("Customer")]
        public async Task<ActionResult> InsertCustomer(Customer customer)
        {
            await cqRepo.InsertCustomerAsync(customer);
            return Created();
        }
        [HttpPost]
        public async Task<ActionResult> Insert(CustomerQuery customerQuery)
        {
            try
            {
                await cqRepo.InsertCustomerQueryAsync(customerQuery);
              // HttpClient client = new HttpClient();
             //  client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
              //  await client.PostAsJsonAsync("http://localhost:5151/api/Customer/CustomewQuery", new { QueryID = customerQuery.QueryID });
                return Created($"api/CustomerQuery{customerQuery.QueryID}", customerQuery);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{queryID}")]
        public async Task<ActionResult> Update(string queryID,CustomerQuery customerQuery)
        {
            try
            {
                await cqRepo.UpdateCustomerQueryAsync(queryID,customerQuery);
                return Ok(customerQuery);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{queryID}")]
        public async Task<ActionResult> Delete(string queryID)
        {
            try
            {
                await cqRepo.DeleteCustomerQueryAsync(queryID);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
