using System.ComponentModel.DataAnnotations.Schema;

namespace SQLSanitizorNator.Data.Models;

public class NaughtyType: EntityBase<Guid>
{
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; } = null!;
    [Column(TypeName = "varchar(511)")]
    public string? Description { get; set; }

    public ICollection<NaughtyWord> NaughtyWords { get; set; } = new List<NaughtyWord>();
}
