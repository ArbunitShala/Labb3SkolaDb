using System;
using System.Collections.Generic;

namespace Labb3SkolaDb.Models;

public partial class Elever
{
    public int Elevid { get; set; }

    public string Förnamn { get; set; } = null!;

    public string Efternamn { get; set; } = null!;

    public string Personnummer { get; set; } = null!;

    public int? Klassid { get; set; }

    public virtual ICollection<Betyg> Betygs { get; set; } = new List<Betyg>();

    public virtual Klasser? Klass { get; set; }
}
