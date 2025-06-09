using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SQLSanitizorNator.Data.Interfaces;

namespace SQLSanitizorNator.Data.Models;

public abstract partial class EntityBase<TKey> : IEntityBase<TKey> where TKey : IEquatable<TKey>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual TKey Id { get; set; }
}
