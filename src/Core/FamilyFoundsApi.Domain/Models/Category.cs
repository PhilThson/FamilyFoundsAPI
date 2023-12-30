using System.ComponentModel.DataAnnotations;

namespace FamilyFoundsApi.Domain;

public class Category : BaseEntity<short>
{
    public Category()
    {
        Transactions = new HashSet<Transaction>();
    }

    [StringLength(128)]
    public string Name { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; }
}
