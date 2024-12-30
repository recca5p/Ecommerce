using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.Entities;

public class OrderDetail : AuditEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long OrderDetailId { get; set; }

    [ForeignKey(nameof(Order))]
    public long OrderId { get; set; }

    public virtual Order Order { get; set; }

    [ForeignKey(nameof(ProductVariant))]
    public long VariantId { get; set; }

    public virtual ProductVariant ProductVariant { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}