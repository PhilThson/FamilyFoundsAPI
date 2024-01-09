using System.ComponentModel.DataAnnotations;
using FamilyFoundsApi.Domain.Models.Base;

namespace FamilyFoundsApi.Domain.Models;

public class Category : BaseEntity<short>
{
    public Category()
    {
        Transactions = new HashSet<Transaction>();
    }

    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; }
}
