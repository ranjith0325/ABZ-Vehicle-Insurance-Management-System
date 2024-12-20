using ABZProductLibrary.Models;
using ABZProductLibrary.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABZProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepoAsync proRepo;
        public ProductController(IProductRepoAsync repo)
        {
            proRepo= repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Product> products = await proRepo.GetAllProductAsync();
            return Ok(products);
        }
    }
}
