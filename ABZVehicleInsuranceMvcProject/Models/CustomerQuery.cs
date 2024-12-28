namespace ABZVehicleInsuranceMvcProject.Models
{
    public class CustomerQuery
    {
        public string QueryID { get; set; } = null!;

        public string CustomerID { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime? QueryDate { get; set; }

        public string? Status { get; set; }
    }
}
