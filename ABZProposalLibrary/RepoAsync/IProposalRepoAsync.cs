using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using ABZProposalLibrary.Models;

namespace ABZProposalLibrary.RepoAsync
{
    public interface IProposalRepoAsync
    {
        Task InsertProposalAsync(Proposal proposal);
        Task UpdateProposalAsync(string proposalNo,Proposal proposal);
        Task DeleteProposalAsync(string proposalNo);
        Task<Proposal> GetProposalByIdAsync(string proposalNo);
        Task<List<Proposal>> GetProposalByAgentAsync(string agentId);
        Task<List<Proposal>> GetProposalByCustomerAsync(string customerId);
        Task<List<Proposal>> GetProposalByProductAsync(string productId);
        Task<List<Proposal>> GetProposalByVehicleAsync(string regNo);
        Task<List<Proposal>> GetAllProposalsAsync();
        Task InsertAgentAsync(Agent agent);
        Task InsertCustomerAsync(Customer customer);
        Task InsertProductAsync(Product product);
        Task InsertVehicleAsync(Vehicle vehicle);

    }
}
