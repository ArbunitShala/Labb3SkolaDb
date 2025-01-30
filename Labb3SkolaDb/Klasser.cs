using System;
using System.Collections.Generic;

namespace Labb3SkolaDb;

public partial class Klasser
{
    public int Klassid { get; set; }

    public string Klassnamn { get; set; } = null!;

    public int Mentorid { get; set; }

    public virtual ICollection<Elever> Elevers { get; set; } = new List<Elever>();

    public virtual Personal Mentor { get; set; } = null!;
}
