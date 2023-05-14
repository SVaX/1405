using System;
using System.Collections.Generic;

namespace DemoAgainAndAgain;

public partial class ServiceAdditionalPhoto
{
    public int ServiceAdditionalPhotoId { get; set; }

    public int? ServiceId { get; set; }

    public string? Photo { get; set; }

    public virtual Service? Service { get; set; }
}
