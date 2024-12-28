using ABZCustomerQueryLibrary.Models;
using ABZCustomerQueryLibrary.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABZCustomerQueryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QueryResponseController : ControllerBase
    {
        IQueryResponseRepoAsync qRepo;
        public QueryResponseController(IQueryResponseRepoAsync repo)
        {
            qRepo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<QueryResponse> qr=await qRepo.GetAllQueryResponseAsync();
            return Ok(qr);
        }
        [HttpGet("{queryID}/{srNo}")]
        public async Task<ActionResult> GetOne(string queryID,string srNo)
        {
            try
            {
                QueryResponse qr = await qRepo.GetQueryResponseAsync(queryID, srNo);
                return Ok(qr);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("Agent")]
        public async Task<ActionResult> InsertAgent(Agent agent)
        {
            await qRepo.InsertAgentAsync(agent);
            return Created();
        }
        [HttpPost]
        public async Task<ActionResult> Insert(QueryResponse queryResponse)
        {
            try
            {
                await qRepo.InsertQueryResponseAsync(queryResponse);
                //HttpClient client = new HttpClient();
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                return Created($"api/QueryResponse{queryResponse.QueryID}", queryResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{queryID}/{srNo}")]
        public async Task<ActionResult> Update(string queryID, string srNo,QueryResponse queryResponse)
        {
            try
            {
                await qRepo.UpdateQueryResponseAsync(queryID,srNo,queryResponse);
                return Ok(qRepo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{queryID}/{srNo}")]
        public async Task<ActionResult> Delete(string queryID, string srNo)
        {
            try
            {
                await qRepo.DeleteQueryResponseAsync(queryID, srNo);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ByCustomerQuery/{queryID}")]
        public async Task<ActionResult> GetByCustomerQuery(string queryID)
        {
            try
            {
                List<QueryResponse> queryResponses = await qRepo.GetQueryResponseByCustomerQuery(queryID);
                return Ok(queryResponses);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet("ByAgent/{agentID}")]
        public async Task<ActionResult> GetByAgent(string agentID)
        {
            try
            {
                List<QueryResponse> queryResponses = await qRepo.GetQueryResponseByAgentAsync(agentID);
                return Ok(queryResponses);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
