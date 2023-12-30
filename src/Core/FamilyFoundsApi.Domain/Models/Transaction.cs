using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyFoundsApi.Domain;

public class Transaction : BaseEntity<long>
{
    public double Amount { get; set; }
    [StringLength(256)]
    public string Contractor { get; set; }
    [StringLength(256)]
    public string Title { get; set; }
    [StringLength(1024)]
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public DateTime? PostingDate { get; set; }
    public short? CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }
}
