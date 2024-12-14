using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.Entities;

public class Product : AuditEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public virtual long CategoryId { get; set; }
    public virtual Category Category { get; set; }
}