using System.ComponentModel.DataAnnotations;

namespace ABZVehicleInsuranceMvcProject.Models
{
    public class Agent
    {
        [RegularExpression(@"\w{10}", ErrorMessage = "Agent ID must be 10 characters")]
        public string AgentID { get; set; } = null!;
        [Required]
        public string AgentName { get; set; } = null!;
        
        [RegularExpression(@"\d{10}", ErrorMessage = "Phone number must be 10 digits")]
        public string AgentPhone { get; set; } = null!;
        [Required]
        public string AgentEmail { get; set; } = null!;
        [RegularExpression(@"\w{10}", ErrorMessage = "License Code must be 10 characters")]
        public string LicenseCode { get; set; } = null!;
    }
}
