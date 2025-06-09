using System.ComponentModel.DataAnnotations.Schema;

namespace SQLSanitizorNator.Data.Interfaces;

public interface IEntityBase<TKey> where TKey : IEquatable<TKey>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    TKey Id { get; set; }
}
