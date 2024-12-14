namespace Domain;

public interface IAuditEntity   
{   
    Int64 CreatedById { get; set; }
    DateTime CreatedDate { get; set; }   
    Int64? UpdateById { get; set; }
    DateTime? UpdatedDate { get; set; }   
}   
public interface IAuditEntity<TKey> : IAuditEntity, IDeleteEntity
{   
}