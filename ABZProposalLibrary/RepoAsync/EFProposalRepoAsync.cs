﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ABZProposalLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace ABZProposalLibrary.RepoAsync
{
    public class EFProposalRepoAsync : IProposalRepoAsync
    {
        ABZProposalDBContext ctx = new ABZProposalDBContext();
        public async Task DeleteProposalAsync(string proposalNo)
        {
            Proposal proposal = await GetProposalByIdAsync(proposalNo);
            ctx.Proposals.Remove(proposal);
            await ctx.SaveChangesAsync();
        }

        public async Task<List<Proposal>> GetAllProposalsAsync()
        {
            List<Proposal> proposals = await ctx.Proposals.ToListAsync();
            return proposals;
        }

        public async Task<List<Proposal>> GetProposalByAgentAsync(string agentId)
        {
            List<Proposal> proposals = await (from pro in ctx.Proposals where agentId== pro.AgentID select pro).ToListAsync();
            return proposals;
        }

        public async Task<List<Proposal>> GetProposalByCustomerAsync(string customerId)
        {
            List<Proposal> proposals = await(from pro in ctx.Proposals where customerId == pro.CustomerID select pro).ToListAsync();
            return proposals;
        }

        public async Task<Proposal> GetProposalByIdAsync(string proposalNo)
        {
            Proposal proposals = await (from pro in ctx.Proposals where pro.ProposalNo==proposalNo select pro).FirstAsync();
            return proposals;
        }

        public async Task<List<Proposal>> GetProposalByProductAsync(string productId)
        {
           List<Proposal> proposals = await(from pro in ctx.Proposals where pro.ProductID == productId select pro).ToListAsync();
            return proposals;
        }

        public async Task<List<Proposal>> GetProposalByVehicleAsync(string regNo)
        {
            List<Proposal> proposals = await(from pro in ctx.Proposals where pro.RegNo == regNo select pro).ToListAsync();
            return proposals;
        }

        public async Task InsertProposalAsync(Proposal proposal)
        {
            await ctx.Proposals.AddAsync(proposal);
            await ctx.SaveChangesAsync();
        }
        public async Task InsertAgentAsync(Agent agent)
        {
            await ctx.Agents.AddAsync(agent);
            await ctx.SaveChangesAsync();
        }
        public async Task InsertCustomerAsync(Customer customer)
        {
            await ctx.Customers.AddAsync(customer);
            await ctx.SaveChangesAsync();
        }
        public async Task InsertProductAsync(Product product)
        {
            await ctx.Products.AddAsync(product);
            await ctx.SaveChangesAsync();
        }
        public async Task InsertVehicleAsync(Vehicle vehicle)
        {
            await ctx.Vehicles.AddAsync(vehicle);
            await ctx.SaveChangesAsync();
        }

        public async Task UpdateProposalAsync(string proposalNo, Proposal updatedProposal)
        {
            Proposal existingProposal = await GetProposalByIdAsync(proposalNo);
            existingProposal.FromDate = updatedProposal.FromDate;
            existingProposal.ToDate = updatedProposal.ToDate;
            existingProposal.IDV = updatedProposal.IDV;
            existingProposal.BasicAmount = updatedProposal.BasicAmount;
            existingProposal.TotalAmount = updatedProposal.TotalAmount;
            await ctx.SaveChangesAsync();
        }
    }
}
