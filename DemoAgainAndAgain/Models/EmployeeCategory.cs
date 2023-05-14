using System;
using System.Collections.Generic;

namespace DemoAgainAndAgain;

public partial class EmployeeCategory
{
    public int EmployeeCategoryId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
