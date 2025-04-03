using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace CatalogAPI.Entities;

public class Offer
{
    public Guid Id { get; set; }

    public Guid ApplicationId { get; set; }
    public Application Application { get; set; }
    [MaxLength(50)]
    public string? OfferType { get; set; } // Conditional / Unconditional
    public DateTime OfferDate { get; set; }
    [MaxLength(1000)]

    public string? ConditionsText { get; set; }
    [MaxLength(50)]
    public string? StudentResponse { get; set; } // Accepted / Rejected / Pending
    public DateTime? ResponseDate { get; set; }

    [Precision(10, 2)]
    public decimal? RequiredDepositAmount { get; set; }
}
