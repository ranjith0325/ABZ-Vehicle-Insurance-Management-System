﻿using ABZAgentLibrary.Models;
using ABZAgentLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABZAgentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class AgentController : ControllerBase
    {
        IAgentRepoAsync agentRepo;
        public AgentController(IAgentRepoAsync ageRepo)
        {
            agentRepo = ageRepo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Agent> agents = await agentRepo.GetAllAgentsAsync();
            return Ok(agents);
        }
    
        [HttpGet("{agentId}")]
        public async Task<ActionResult> GetOne(string agentId)
        {
            try
            {
                Agent agent = await agentRepo.GetAgentByIDAsync(agentId);
                return Ok(agent);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("{token}")]
        public async Task<ActionResult> Insert(string token, Agent agent)
        {
            try
            {
                await agentRepo.InsertAgentAsync(agent);
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
               // await client.PostAsJsonAsync("http://localhost:5273/api/Proposal/Agent", new { AgentId = agent.AgentID });
                await client.PostAsJsonAsync("http://abzproposalwebapi-akshitha.azurewebsites.net/api/Proposal/Agent", new { AgentId = agent.AgentID });
                return Created($"api/Agent{agent.AgentID}",agent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{agentId}")]
        public async Task<ActionResult> Update(string agentId, Agent agent)
        {
            try
            {
                await agentRepo.UpdateAgentAsync(agentId, agent);
                return Ok(agent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{agentId}")]
        public async Task<ActionResult> Delete(string agentId)
        {
            try
            {
                await agentRepo.DeleteAgentAsync(agentId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}