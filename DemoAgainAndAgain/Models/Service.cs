using System;
using System.Collections.Generic;

namespace DemoAgainAndAgain;

public partial class Service
{
    public int ServiceId { get; set; }

    public string? Name { get; set; }

    public string? Photo { get; set; }

    public int? Duration { get; set; }

    public double? Cost { get; set; }

    public int? Discount { get; set; }

    public double? CostWithDiscount { get; set; }

    public string? Discription { get; set; }

    public virtual ICollection<ClientService> ClientServices { get; set; } = new List<ClientService>();

    public virtual ICollection<ServiceAdditionalPhoto> ServiceAdditionalPhotos { get; set; } = new List<ServiceAdditionalPhoto>();
}
