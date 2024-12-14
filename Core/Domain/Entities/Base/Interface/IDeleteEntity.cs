namespace Domain;

public interface IDeleteEntity
{
    Boolean IsDeleted { get; set; }
}   
   
public interface IDeleteEntity<TKey> : IDeleteEntity   
{   
}   