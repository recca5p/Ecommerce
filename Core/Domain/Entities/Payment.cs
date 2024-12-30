using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.Entities;

public class Payment : AuditEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long PaymentId { get; set; }

    [ForeignKey(nameof(Order))]
    public long OrderId { get; set; }

    public virtual Order Order { get; set; }

    public string PaymentMethod { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }
}
