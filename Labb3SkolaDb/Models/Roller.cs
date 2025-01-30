using System;
using System.Collections.Generic;

namespace Labb3SkolaDb.Models;

public partial class Roller
{
    public int Rollid { get; set; }

    public string Rollnamn { get; set; } = null!;

    public virtual ICollection<Personal> Personals { get; set; } = new List<Personal>();
}
