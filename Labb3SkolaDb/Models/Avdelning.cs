using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb3SkolaDb.Models;

[Table("avdelningar")]
public class Avdelning
{
    [Column("avdelningid")]
    public int AvdelningID { get; set; }
    [Column("avdelningnamn")]
    public string AvdelningNamn { get; set; }
    public ICollection<Klasser> Klassers { get; set; }
}
