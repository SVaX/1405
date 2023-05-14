using System;
using System.Collections.Generic;

namespace DemoAgainAndAgain;

public partial class Client
{
    public int ClientId { get; set; }

    public string? LastName { get; set; }

    public string? Name { get; set; }

    public string? Patronymic { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Email { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public string? Photo { get; set; }

    public int? TagId { get; set; }

    public virtual ICollection<ClientService> ClientServices { get; set; } = new List<ClientService>();

    public virtual Tag? Tag { get; set; }
}
