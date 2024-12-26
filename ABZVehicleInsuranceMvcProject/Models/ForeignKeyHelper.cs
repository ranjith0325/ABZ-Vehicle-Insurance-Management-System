using Microsoft.AspNetCore.Mvc.Rendering;

namespace ABZVehicleInsuranceMvcProject.Models
{
    public class ForeignKeyHelper
    {
        public static async Task<List<SelectListItem>> GetCustomerIds()
        {
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5151/api/Customer/") };
            List<Customer> customers = await client.GetFromJsonAsync<List<Customer>>("");
            List<SelectListItem> customerIds = new List<SelectListItem>();
            foreach (Customer customer in customers)
            {
                customerIds.Add(new SelectListItem { Text = customer.CustomerID.ToString(), Value = customer.CustomerID.ToString() });
            }
            return customerIds;
        }
        public static async Task<List<SelectListItem>> GetPolicyNos()
        {
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5007/api/Policy/") };
            List<Policy> policies = await client.GetFromJsonAsync<List<Policy>>("");
            List<SelectListItem> policyNos = new List<SelectListItem>();
            foreach (Policy policy in policies)
            {
                policyNos.Add(new SelectListItem { Text = policy.PolicyNo.ToString(), Value = policy.PolicyNo.ToString() });
            }
            return policyNos;
        }
        public static async Task<List<SelectListItem>> GetProductIds()
        {
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5145/api/Product/") };
            List<Product> products = await client.GetFromJsonAsync<List<Product>>("");
            List<SelectListItem> productIds = new List<SelectListItem>();
            foreach (Product product in products)
            {
                productIds.Add(new SelectListItem { Text = product.ProductID.ToString(), Value = product.ProductID.ToString() });
            }
            return productIds;
        }
        public static async Task<List<SelectListItem>> GetAgentIds()
        {
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5147/api/Agent/") };
            List<Agent> agents = await client.GetFromJsonAsync<List<Agent>>("");
            List<SelectListItem> agentIds = new List<SelectListItem>();
            foreach (Agent agent in agents)
            {
                agentIds.Add(new SelectListItem { Text = agent.AgentID.ToString(), Value = agent.AgentID.ToString() });
            }
            return agentIds;
        }
        public static async Task<List<SelectListItem>> GetRegNos()
        {
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5083/api/Vehicle/") };
            List<Vehicle> vehicles = await client.GetFromJsonAsync<List<Vehicle>>("");
            List<SelectListItem> regNos = new List<SelectListItem>();
            foreach (Vehicle vehicle in vehicles)
            {
                regNos.Add(new SelectListItem { Text = vehicle.RegNo.ToString(), Value = vehicle.RegNo.ToString() });
            }
            return regNos;
        }
        public static async Task<List<SelectListItem>> GetProposalNos()
        {
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5273/api/proposal/") };
            List<Proposal> proposals = await client.GetFromJsonAsync<List<Proposal>>("");
            List<SelectListItem> proposalNos = new List<SelectListItem>();
            foreach (Proposal proposal in proposals)
            {
                proposalNos.Add(new SelectListItem { Text = proposal.ProposalNo.ToString(), Value = proposal.ProposalNo.ToString() });
            }
            return proposalNos;
        }
    }
}
