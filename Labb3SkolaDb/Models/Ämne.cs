using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb3SkolaDb.Models;

public partial class Ämne
{
    public int Ämneid { get; set; }

    public string Ämnenamn { get; set; } = null!;

    [Column("aktiv")]
    public bool Aktiv { get; set; }

    public virtual ICollection<Betyg> Betygs { get; set; } = new List<Betyg>();
}
