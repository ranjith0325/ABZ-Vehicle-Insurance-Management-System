using System.ComponentModel.DataAnnotations;

namespace ABZVehicleInsuranceMvcProject.Models
{
    public class CustomerQuery
    {
        [RegularExpression(@"\w{10}", ErrorMessage = "QueryID must be 10 digits")]

        public string QueryID { get; set; } = null!;

        public string CustomerID { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime? QueryDate { get; set; }

        public string? Status { get; set; }
    }
}
