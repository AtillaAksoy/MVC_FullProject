using System;
using System.Collections.Generic;

namespace MVC_FullProject.Models;

public partial class Staff
{
    public int PersonelId { get; set; }

    public string PersonelName { get; set; } = null!;

    public string Phone { get; set; } = null!;
}
