using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.Entities;

public class Cart : AuditEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long CartId { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual IList<CartItem> CartItems { get; set; } = new List<CartItem>();
}
