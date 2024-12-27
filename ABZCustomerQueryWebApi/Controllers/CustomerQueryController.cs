using ABZCustomerQueryLibrary.Repo;
using ABZCustomerQueryLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABZCustomerQueryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpPost("{token}")]
        public async Task<ActionResult> Insert(string token,CustomerQuery customerQuery)
        {
            try
            {
                await cqRepo.InsertCustomerQueryAsync(customerQuery);
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                return Created($"api/CustomerQuery{customerQuery.QueryID}", customerQuery);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
