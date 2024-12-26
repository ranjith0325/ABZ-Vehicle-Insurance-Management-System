using System;
using System.Collections.Generic;

namespace ABZCustomerQueryLibrary.Models;

public partial class CustomerQuery
{
    public string QueryID { get; set; } = null!;

    public string CustomerID { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? QueryDate { get; set; }

    public string? status { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<QueryResponse> QueryResponses { get; set; } = new List<QueryResponse>();
}
