using System.ComponentModel.DataAnnotations;

namespace ABZVehicleInsuranceMvcProject.Models
{
    public class Product
    {
        [RegularExpression(@"\w{10}", ErrorMessage = "ProductID must be 10 characters")]

        public string ProductID { get; set; } = null!;
        [Required]
        public string ProductName { get; set; } = null!;
        [Required]
        public string ProductDescription { get; set; } = null!;
        [RegularExpression(@"\w{20}", ErrorMessage = "ProductUIN must be 20 characters")]

        public string ProductUIN { get; set; } = null!;
        [RegularExpression(@"^(public car|private car)$", ErrorMessage = "Please enter either 'public car' or 'private car'.")]
        public string InsuredInterests { get; set; } = null!;
        [Required]
        public string PolicyCoverage { get; set; } = null!;

     //   public virtual ICollection<ProductAddon> ProductAddons { get; set; } = new List<ProductAddon>();
    }
}
