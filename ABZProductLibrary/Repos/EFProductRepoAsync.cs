using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABZProductLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace ABZProductLibrary.Repos
{
    public class EFProductRepoAsync : IProductRepoAsync
    {
        ABZProductDBContext ctx=new ABZProductDBContext();
        public async Task DeleteProductAsync(string productID)
        {
            Product product = await GetProductAsync(productID);
            ctx.Products.Remove(product);
            await ctx.SaveChangesAsync();
        }

        public Task<List<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductAsync(string ProductID)
        {
            try
            {
                Product product = await (from pro in ctx.Products where ProductID == pro.ProductID select pro).FirstAsync();
                return product;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
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
