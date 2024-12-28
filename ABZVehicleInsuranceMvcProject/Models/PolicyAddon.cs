using System.ComponentModel.DataAnnotations;

namespace ABZVehicleInsuranceMvcProject.Models
{
    public class PolicyAddon
    {
        [RegularExpression(@"\w{4}", ErrorMessage = "AddonID must be 4 characters")]

        public string AddonID { get; set; } = null!;
        [Required]
        public string PolicyNo { get; set; } = null!;

        public decimal? Amount { get; set; }

       // public virtual Policy? PolicyNoNavigation { get; set; } = null!;
    }
}
