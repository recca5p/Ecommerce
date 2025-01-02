using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.Entities;

public class Product : AuditEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ProductId { get; set; }

    [Required]
    [MaxLength(256)]
    public string ProductName { get; set; }

    public string Description { get; set; }

    public long CategoryId { get; set; }

    public virtual Category Category { get; set; }

    // Add navigation property for ProductVariants
    public virtual IList<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

    // Add navigation property for Images
    public virtual IList<Image> Images { get; set; } = new List<Image>();

    // Add navigation property for Reviews
    public virtual IList<Review> Reviews { get; set; } = new List<Review>();
}
