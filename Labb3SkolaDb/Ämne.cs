using System;
using System.Collections.Generic;

namespace Labb3SkolaDb;

public partial class Ämne
{
    public int Ämneid { get; set; }

    public string Ämnenamn { get; set; } = null!;

    public virtual ICollection<Betyg> Betygs { get; set; } = new List<Betyg>();
}
