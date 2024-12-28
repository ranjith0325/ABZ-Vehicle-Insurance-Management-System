using System.ComponentModel.DataAnnotations;

namespace ABZVehicleInsuranceMvcProject.Models
{
    public class Proposal
    {
        [RegularExpression(@"\d{10}", ErrorMessage = "Proposal Number must be 10 characters")]

        public string ProposalNo { get; set; } = null!;
        [Required]
        public string RegNo { get; set; } = null!;
        [Required]
        public string ProductID { get; set; } = null!;
        [Required]
        public string CustomerID { get; set; } = null!;
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        [Required]
        public decimal IDV { get; set; }
        [Required]
        public string AgentID { get; set; } = null!;

        public decimal? BasicAmount { get; set; }

        public decimal? TotalAmount { get; set; }

        //public virtual Agent? Agent { get; set; } = null!;

        //public virtual Customer? Customer { get; set; } = null!;

        //public virtual Product? Product { get; set; } = null!;

        //public virtual Vehicle? RegNoNavigation { get; set; } = null!;
    }
}
