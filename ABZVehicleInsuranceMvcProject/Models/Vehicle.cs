using System.ComponentModel.DataAnnotations;

namespace ABZVehicleInsuranceMvcProject.Models
{
    public class Vehicle
    {
        [RegularExpression(@"\w{10}", ErrorMessage = "RegNumber must be 10 characters")]

        public string RegNo { get; set; } = null!;
        [Required]
        public string RegAuthority { get; set; } = null!;
        [Required]
        public string Make { get; set; } = null!;
        [Required]
        public string Model { get; set; } = null!;
        [RegularExpression(@"^[pdeclPDECL]{1}$", ErrorMessage = "Please enter only 'P' or 'D' or 'E' or 'C' or 'L'")]

        public string FuelType { get; set; } = null!;
        [Required]
        public string Variant { get; set; } = null!;
        [Required]
        public string EngineNo { get; set; } = null!;
        [Required]
        public string ChassisNo { get; set; } = null!;
        [Required]
        public int EngineCapacity { get; set; }
        [Required]
        public int SeatingCapacity { get; set; }
        [Required]
        public string MfgYear { get; set; } = null!;
        [Required]
        public DateTime RegDate { get; set; }
        [Required]
        public string BodyType { get; set; } = null!;
        [Required]
        public string? LeasedBy { get; set; }
        [Required]
        public string OwnerId { get; set; } = null!;

      //  public virtual Customer? Owner { get; set; } = null!;
    }
}
