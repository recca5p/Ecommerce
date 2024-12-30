using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.Entities;

public class CartItem : AuditEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long CartItemId { get; set; }

    [ForeignKey(nameof(Cart))]
    public long CartId { get; set; }

    public virtual Cart Cart { get; set; }

    [ForeignKey(nameof(ProductVariant))]
    public long VariantId { get; set; }

    public virtual ProductVariant ProductVariant { get; set; }

    public int Quantity { get; set; }
}
