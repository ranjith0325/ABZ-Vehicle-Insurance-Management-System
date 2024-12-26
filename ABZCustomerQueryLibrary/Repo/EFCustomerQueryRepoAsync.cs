using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABZCustomerQueryLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace ABZCustomerQueryLibrary.Repo
{
    public class EFCustomerQueryRepoAsync : ICustomerQueryRepoAsync
    {
        ABZCustomerQueryDBContext ctx = new ABZCustomerQueryDBContext();
        public async Task DeleteCustomerQueryAsync(string queryID)
        {
            CustomerQuery customerquery = await GetCustomerQueryAsync(queryID);
            ctx.CustomerQueries.Remove(customerquery);
            await ctx.SaveChangesAsync();
        }

        public async Task<List<CustomerQuery>> GetAllCustomerQuerysAsync()
        {
            List<CustomerQuery> customerQueries = await ctx.CustomerQueries.ToListAsync();  
            return customerQueries;
        }

        public async Task<CustomerQuery> GetCustomerQueryAsync(string queryID)
        {
            try
            {
                CustomerQuery customerquery = await (from c in ctx.CustomerQueries where queryID == c.QueryID select c).FirstAsync();
                return customerquery;
            }
            catch (Exception)
            {

                throw new Exception("Invalid QueryID");
            }
        }

        public async Task InsertCustomerQueryAsync(CustomerQuery customerQuery)
        {
            await ctx.CustomerQueries.AddAsync(customerQuery);
            await ctx.SaveChangesAsync();
        }

        public async Task UpdateCustomerQueryAsync(string queryID, CustomerQuery customerQuery)
        {
            CustomerQuery c1 = await GetCustomerQueryAsync(queryID);
            c1.QueryDate = customerQuery.QueryDate;
            c1.Description = customerQuery.Description;
            c1.Status = customerQuery.Status;
        }
    }
}
