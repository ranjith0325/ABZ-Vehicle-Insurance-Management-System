using ABZProductLibrary.Models;
using ABZProductLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABZProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProductAddonController : ControllerBase
    {
        IProductAddonRepoAsync proRepo;
        public ProductAddonController(IProductAddonRepoAsync repo)
        {
            proRepo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<ProductAddon> productAddons = await proRepo.GetAllProductAddonAsync();
            return Ok(productAddons);

        }
        [HttpGet("{productID}/{addonId}")]
        public async Task<ActionResult> GetOne(string productID, string addonId)
        {
            try
            {
                ProductAddon productAddon=await proRepo.GetProductAddonAsync(productID, addonId);
                return Ok(productAddon);
            }
            catch (Exception ex)
            {

               return NotFound(ex.Message);
            }
        }
        [HttpPost("{token}")]
        public async Task<ActionResult> Insert(string token,ProductAddon productAddon)
        {
            try
            {
                await proRepo.InsertProductAddonAsync(productAddon);
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                return Created($"api/ProductAddon{productAddon.ProductID}",productAddon);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{productID}/{addonId}")]
        public async Task<ActionResult> Update(string productID, string addonId, ProductAddon productAddon)
        {
            try
            {
                await proRepo.UpdateProductAddonAsync(productID,addonId,productAddon);
                return Ok(productAddon);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{productID}/{addonId}")]
        public async Task<ActionResult> Delete(string productID, string addonId)
        {
            try
            {
                await proRepo.DeleteProductAddonAsync(productID, addonId);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ByProduct/{productID}")]
        public async Task<ActionResult> GetProductAddonByProduct(string productID)
        {
            try
            {
                List<ProductAddon> productAddons = await proRepo.GetProductAddonByProductAsync(productID);
                return Ok(productAddons);
            }
            catch (Exception ex)
            {

                return NotFound();
            }
        }
       
    }
}
