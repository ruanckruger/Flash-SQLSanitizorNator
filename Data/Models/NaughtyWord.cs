using SQLSanitizorNator.Data.Consts;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLSanitizorNator.Data.Models;

public class NaughtyWord : EntityBase<Guid>
{
    [Column(TypeName = "varchar(100)")]
    public string Value { get; set; } = null!;

    public int UsageCount { get; set; } = 0;

    private uint _severity = 10;
    [Column(TypeName = "tinyint")]
    public uint Severity
    {
        get => _severity; 
        set {
            if(value < SeverityLimits.Min) throw new ArgumentOutOfRangeException(nameof(value), $"Severity must be larger than {SeverityLimits.Min}.");
            if(value > SeverityLimits.Max) throw new ArgumentOutOfRangeException(nameof(value), $"Severity must be less than {SeverityLimits.Max}.");
            if(_severity == value) return;
            _severity = value;
        }
    }

    #region relationships
    public Guid NaughtyTypeId { get; set; }
    [ForeignKey(nameof(NaughtyTypeId))]
    public NaughtyType Type { get; set; } = null!;
    #endregion

    #region functions
    #endregion
}
