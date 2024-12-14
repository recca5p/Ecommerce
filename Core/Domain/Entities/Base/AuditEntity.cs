namespace Domain.Base;

public abstract class AuditEntity<TKey> : DeleteEntity, IAuditEntity 
{   
    public Int64 CreatedById { get; set; }
    public DateTime CreatedDate { get; set; }   
    public Int64? UpdateById { get; set; }
    public DateTime? UpdatedDate { get; set; } 
}