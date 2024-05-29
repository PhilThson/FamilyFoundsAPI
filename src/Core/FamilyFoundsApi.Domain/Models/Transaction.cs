using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using FamilyFoundsApi.Domain.Models.Base;

namespace FamilyFoundsApi.Domain.Models;

public class Transaction : BaseEntity<long>, IRemovable
{
    private string _number;

    [Required]
    [Column(TypeName = "money")]
    public decimal Amount { get; set; }
    [StringLength(3)]
    public string Currency { get; set; }
    [StringLength(128)]
    public string Account { get; set; }
    [StringLength(256)]
    public string Contractor { get; set; }
    [StringLength(256)]
    public string Title { get; set; }
    [StringLength(1024)]
    public string Description { get; set; }
    [Required]
    public DateTime Date { get; set; }
    public DateTime? PostingDate { get; set; }
    [StringLength(128)]
    public string Number 
    { 
        get => _number ??= ComputeNumber();
        set => _number = value;
    }
    [StringLength(128)]
    public string ContractorAccountNumber { get; set; }
    [StringLength(256)]
    public string ContractorBankName { get; set; }
    public short? CategoryId { get; set; }
    public short? ImportSourceId { get; set; }
    public bool IsActive { get; set; } = true;

    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }
    [ForeignKey(nameof(ImportSourceId))]
    public virtual ImportSource ImportSource { get; set; }

    private string ComputeNumber()
    {
        var concat = $"{Date}-{Description}-{Amount}-{Contractor}-{Account}-{IsActive}-{Title}";
        byte[] inputBytes = Encoding.UTF8.GetBytes(concat.Replace(" ", ""));
        byte[] hashBytes = MD5.HashData(inputBytes);
        return Convert.ToHexString(hashBytes);
    }


    public static bool operator==(Transaction transaction, Transaction other)
    {
        return transaction.Title == other.Title
            && transaction.Contractor == other.Contractor
            && transaction.Amount == other.Amount
            && transaction.Description == other.Description
            && transaction.Date.Date == other.Date.Date
            && transaction.PostingDate?.Date == other.PostingDate?.Date
            && transaction.CategoryId == other.CategoryId
            && transaction.Currency == other.Currency
            && transaction.Account == other.Account
            && transaction.ContractorAccountNumber == other.ContractorAccountNumber
            && transaction.ContractorBankName == other.ContractorBankName;
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

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
