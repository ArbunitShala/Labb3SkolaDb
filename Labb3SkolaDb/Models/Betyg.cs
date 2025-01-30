using System;
using System.Collections.Generic;

namespace Labb3SkolaDb.Models;

public partial class Betyg
{
    public int Betygid { get; set; }

    public int Elevid { get; set; }

    public int Ämneid { get; set; }

    public int Lärareid { get; set; }

    public char Betyg1 { get; set; }

    public DateOnly Datum { get; set; }

    public virtual Elever Elev { get; set; } = null!;

    public virtual Personal Lärare { get; set; } = null!;

    public virtual Ämne Ämne { get; set; } = null!;
}
