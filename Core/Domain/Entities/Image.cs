using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.Entities;

public class Image : AuditEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ImageId { get; set; }

    public long? ProductId { get; set; }

    public virtual Product Product { get; set; }

    public long? VariantId { get; set; }

    public virtual ProductVariant ProductVariant { get; set; }

    [Required]
    public string ImageUrl { get; set; }

    public bool IsPrimary { get; set; }
}
