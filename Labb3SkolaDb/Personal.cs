using System;
using System.Collections.Generic;

namespace Labb3SkolaDb;

public partial class Personal
{
    public int Personalid { get; set; }

    public string Förnamn { get; set; } = null!;

    public string Efternamn { get; set; } = null!;

    public string Personnummer { get; set; } = null!;

    public int Rollid { get; set; }

    public virtual ICollection<Betyg> Betygs { get; set; } = new List<Betyg>();

    public virtual ICollection<Klasser> Klassers { get; set; } = new List<Klasser>();

    public virtual Roller Roll { get; set; } = null!;
}
