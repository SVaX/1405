using System;
using System.Collections.Generic;

namespace DemoAgainAndAgain;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public int? ProductCategoryId { get; set; }

    public double? Cost { get; set; }

    public string? Characteristics { get; set; }

    public string? Description { get; set; }

    public double? Weight { get; set; }

    public string? Size { get; set; }

    public int? ManufacturerId { get; set; }

    public string? Photo { get; set; }

    public bool? IsActual { get; set; }

    public virtual Manufacturer? Manufacturer { get; set; }

    public virtual ICollection<ProductAdditionalPhoto> ProductAdditionalPhotos { get; set; } = new List<ProductAdditionalPhoto>();

    public virtual ProductCategory? ProductCategory { get; set; }
}
