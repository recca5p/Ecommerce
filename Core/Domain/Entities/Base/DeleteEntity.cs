using Domain.Base;
using System.ComponentModel;

namespace Domain;

public abstract class DeleteEntity : IDeleteEntity
{
    public Boolean IsDeleted { get; set; } = false;
}   