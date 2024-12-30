using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.Entities;

public class ProductVariant : AuditEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long VariantId { get; set; }

    [ForeignKey(nameof(Product))]
    public long ProductId { get; set; }

    public virtual Product Product { get; set; }

    [Required]
    [MaxLength(50)]
    public string Color { get; set; }

    [Required]
    public string Storage { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }
}
