using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.Entities;

public class Review : AuditEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ReviewId { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; }

    public long ProductId { get; set; }

    public virtual Product Product { get; set; }

    public int Rating { get; set; }

    [MaxLength(1000)]
    public string Comment { get; set; }
}
