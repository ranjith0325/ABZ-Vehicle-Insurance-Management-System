using System.ComponentModel.DataAnnotations;

namespace ABZVehicleInsuranceMvcProject.Models
{
    public class Claim
    {
        [RegularExpression(@"\w{10}", ErrorMessage = "Claim Number must be 10 characters")]
        public string ClaimNo { get; set; } = null!;

        public DateTime? ClaimDate { get; set; }
        [Required]
        public string PolicyNo { get; set; } = null!;
        [Required]
        public DateTime IncidentDate { get; set; }
        [Required]
        public string IncidentLocation { get; set; } = null!;

        public string? IncidentDescription { get; set; }
        [Required]
        public decimal ClaimAmount { get; set; }
        [Required]

        public string SurveyorName { get; set; } = null!;

        [RegularExpression(@"\d{10}", ErrorMessage = "Phone number must be 10 digits")]
        public string SurveyorPhone { get; set; } = null!;

        public DateTime? SurveyDate { get; set; }

        public string? SurveyDescription { get; set; }
        [RegularExpression(@"^[sartSART]{1}$", ErrorMessage = "Please enter only 'S' or 'A' or 'R' or 'T'")]
        public string ClaimStatus { get; set; } = null!;

     //   public virtual Policy? PolicyNoNavigation { get; set; } = null!;
    }
}
