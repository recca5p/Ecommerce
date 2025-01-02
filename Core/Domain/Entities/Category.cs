using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.Entities;

public class Category : AuditEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long CategoryId { get; set; }

    [Required]
    [MaxLength(256)]
    public string CategoryName { get; set; }

    public virtual IList<Product> Products { get; set; } = new List<Product>();

}
