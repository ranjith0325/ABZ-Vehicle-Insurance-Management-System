using System.ComponentModel.DataAnnotations;

namespace ABZVehicleInsuranceMvcProject.Models
{
    public class QueryResponse
    {
        [Required]

        public string QueryID { get; set; } = null!;
        [RegularExpression(@"\w{10}", ErrorMessage = "SrNO must be 10 characters")]

        public string SrNo { get; set; } = null!;
        [Required]
        public string? AgentID { get; set; }

        public string? Description { get; set; }

        public DateTime? ResponseDate { get; set; }
    }
}
