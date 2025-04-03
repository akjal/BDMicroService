using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace CatalogAPI.Entities;
public class Payment
{
    public Guid Id { get; set; }

    public Guid StudentId { get; set; }
    public Student Student { get; set; }

    public Guid ApplicationId { get; set; }
    public Application Application { get; set; }

    [Precision(10, 2)]
    public decimal AmountPaid { get; set; }

    public DateTime PaymentDate { get; set; }
    [MaxLength(50)]
    public string? PaymentMethod { get; set; }
    [MaxLength(100)]
    public string? TransactionRef { get; set; }
}
