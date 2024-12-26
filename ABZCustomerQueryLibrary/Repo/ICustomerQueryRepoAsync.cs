using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABZCustomerQueryLibrary.Models;

namespace ABZCustomerQueryLibrary.Repo
{
    public interface ICustomerQueryRepoAsync
    {
        Task InsertCustomerQueryAsync(CustomerQuery customerQuery);
        Task DeleteCustomerQueryAsync(string queryID);
        Task UpdateCustomerQueryAsync(string queryID, CustomerQuery customerQuery);
        Task<List<CustomerQuery>> GetAllCustomerQuerysAsync();
        Task<CustomerQuery> GetCustomerQueryAsync(string queryID);
    }
}
