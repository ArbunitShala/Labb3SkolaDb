using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb3SkolaDb.Models;

public partial class Klasser
{
    public int Klassid { get; set; }

    public string Klassnamn { get; set; } = null!;

    public int Mentorid { get; set; }

    public virtual ICollection<Elever> Elevers { get; set; } = new List<Elever>();

    public virtual Personal Mentor { get; set; } = null!;

    [Column("avdelningid")]
    public virtual int AvdelningID { get; set; }
    [ForeignKey("AvdelningID")]
    public virtual Avdelning Avdelning { get; set; }
}
