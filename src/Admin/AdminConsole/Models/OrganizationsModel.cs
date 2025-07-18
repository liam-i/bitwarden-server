﻿// FIXME: Update this file to be null safe and then delete the line below
#nullable disable

using Bit.Admin.Models;
using Bit.Core.AdminConsole.Entities;

namespace Bit.Admin.AdminConsole.Models;

public class OrganizationsModel : PagedModel<Organization>
{
    public string Name { get; set; }
    public string UserEmail { get; set; }
    public bool? Paid { get; set; }
    public string Action { get; set; }
    public bool SelfHosted { get; set; }

    public double StorageGB(Organization org) => org.Storage.HasValue ? Math.Round(org.Storage.Value / 1073741824D, 2) : 0;
}
