using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.Entities;

public class Order : AuditEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long OrderId { get; set; }

    [ForeignKey(nameof(User))]
    public long UserId { get; set; }

    public virtual User User { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalPrice { get; set; }

    public string PaymentStatus { get; set; }

    public string ShippingStatus { get; set; }

    public virtual IList<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public virtual Payment Payment { get; set; }
}
