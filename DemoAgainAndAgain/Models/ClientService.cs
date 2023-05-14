using System;
using System.Collections.Generic;

namespace DemoAgainAndAgain;

public partial class ClientService
{
    public int ClientServiceId { get; set; }

    public int? ServiceId { get; set; }

    public string? ServiceName { get; set; }

    public DateTime? Start { get; set; }

    public string? ClientName { get; set; }

    public int? ClientId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Service? Service { get; set; }
}
