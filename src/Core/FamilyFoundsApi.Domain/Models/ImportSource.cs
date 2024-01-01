using System.ComponentModel.DataAnnotations;

namespace FamilyFoundsApi.Domain;

public class ImportSource : BaseEntity<short>
{
    public ImportSource()
    {
        Transactions = new HashSet<Transaction>();
    }

    [Required]
    [StringLength(256)]
    public string Name { get; set; }
    [StringLength(512)]
    public string Description { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; }
}
