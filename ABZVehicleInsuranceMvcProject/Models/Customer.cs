using System.ComponentModel.DataAnnotations;

namespace ABZVehicleInsuranceMvcProject.Models
{
    public class Customer
    {
        [RegularExpression(@"\w{10}", ErrorMessage = "CustomerID must be 10 characters")]

        public string CustomerID { get; set; } = null!;
        [Required]
        public string CustomerName { get; set; } = null!;
        [RegularExpression(@"\d{10}", ErrorMessage = "Phone number must be 10 digits")]

        public string CustomerPhone { get; set; } = null!;
        [Required]
        public string CustomerEmail { get; set; } = null!;

        public string? CustomerAddress { get; set; }
    }
}
