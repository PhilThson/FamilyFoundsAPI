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


    public static bool operator==(Transaction transaction, Transaction other)
    {
        return transaction.Title == other.Title
            && transaction.Contractor == other.Contractor
            && transaction.Amount == other.Amount
            && transaction.Description == other.Description
            && transaction.Date.Date == other.Date.Date
            && transaction.PostingDate?.Date == other.PostingDate?.Date
            && transaction.Category?.Name == other.Category?.Name;
    }

    public static bool operator!=(Transaction transaction, Transaction other)
    {
        return !(transaction == other);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (ReferenceEquals(obj, null))
        {
            return false;
        }

        if (obj is not Transaction) {
            return false;
        }
        
        return this == obj as Transaction;
    }
}
