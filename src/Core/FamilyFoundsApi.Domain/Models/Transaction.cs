using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FamilyFoundsApi.Domain.Models.Base;

namespace FamilyFoundsApi.Domain.Models;

public class Transaction : BaseEntity<long>, IRemoveable
{
    [Required]
    public double Amount { get; set; }
    [Required]
    [StringLength(256)]
    public string Contractor { get; set; }
    [StringLength(256)]
    public string Title { get; set; }
    [StringLength(1024)]
    public string Description { get; set; }
    [Required]
    public DateTime Date { get; set; }
    public DateTime? PostingDate { get; set; }
    public short? CategoryId { get; set; }
    public short? ImportSourceId { get; set; }
    public bool IsActive { get; set; } = true;

    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }
    [ForeignKey(nameof(ImportSourceId))]
    public virtual ImportSource ImportSource { get; set; }

}
