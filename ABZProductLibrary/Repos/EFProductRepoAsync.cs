using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABZProductLibrary.Models;

namespace ABZProductLibrary.Repos
{
    public class EFProductRepoAsync : IProductRepoAsync
    {
        ABZProductDBContext ctx=new ABZProductDBContext();
        public async Task DeleteProductAsync(string productID)
        {
            await ctx.Products.Remove(productID);
            await ctx.SaveChangesAsync();
        }

        public Task<List<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductAsync(string ProductID)
        {
            throw new NotImplementedException();
        }

        public Task InsertProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(string productID, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
