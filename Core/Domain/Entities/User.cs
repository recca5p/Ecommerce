using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.Entities;

public class User : AuditEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long UserId { get; set; }

    [Required]
    [MaxLength(256)]
    public string Name { get; set; }

    [Required]
    [MaxLength(256)]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
    
    [Required]
    public bool IsAdmin { get; set; } = false;

    public virtual IList<Cart> Carts { get; set; } = new List<Cart>();

    public virtual IList<Order> Orders { get; set; } = new List<Order>();
}
