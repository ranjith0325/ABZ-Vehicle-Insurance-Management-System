using System;
using System.Collections.Generic;

namespace ABZVehicleLibrary.Models;

public partial class Customer
{
    public string CustomerID { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
