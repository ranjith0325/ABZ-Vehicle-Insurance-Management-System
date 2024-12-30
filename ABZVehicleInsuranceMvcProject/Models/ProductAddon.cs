using System.ComponentModel.DataAnnotations;

namespace ABZVehicleInsuranceMvcProject.Models
{
    public class ProductAddon
    {
        [Required] 
        public string ProductID { get; set; } = null!;
        [RegularExpression(@"\w{10}", ErrorMessage = "AddonID must be 10 characters")]

        public string AddonID { get; set; } = null!;
        [Required]
        public string AddonTitle { get; set; } = null!;
        [Required]
        public string AddonDescription { get; set; } = null!;

      //  public virtual Product? Product { get; set; } = null!;
    }
}
