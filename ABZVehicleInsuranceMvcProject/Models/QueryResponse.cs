namespace ABZVehicleInsuranceMvcProject.Models
{
    public class QueryResponse
    {
        public string QueryID { get; set; } = null!;

        public string SrNo { get; set; } = null!;

        public string? AgentID { get; set; }

        public string? Description { get; set; }

        public DateTime? ResponseDate { get; set; }
    }
}
