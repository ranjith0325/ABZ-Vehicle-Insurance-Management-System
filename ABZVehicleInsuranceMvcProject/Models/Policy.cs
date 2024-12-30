using System.ComponentModel.DataAnnotations;

namespace ABZVehicleInsuranceMvcProject.Models
{
    public class Policy
    {
        [RegularExpression(@"\w{10}", ErrorMessage = "PolicyID must be 10 digits")]

        public string PolicyNo { get; set; } = null!;
        [Required]
        public string? ProposalNo { get; set; }

        public decimal? NoClaimBonus { get; set; }
        [RegularExpression(@"\w{5}", ErrorMessage = "Receipt Number must be 5 characters")]

        public string ReceiptNo { get; set; } = null!;
        [Required]
        public DateTime ReceiptDate { get; set; }
        [RegularExpression(@"^[ucdqUCDQ]{1}$", ErrorMessage = "Please enter only 'U' or 'C' or 'D' or 'Q'")]

        public string PaymentMode { get; set; } = null!;

        public decimal? Amount { get; set; }

      //  public virtual ICollection<PolicyAddon> PolicyAddons { get; set; } = new List<PolicyAddon>();

      //  public virtual Proposal? ProposalNoNavigation { get; set; }
    }
}
