using System.ComponentModel.DataAnnotations;

namespace FamilyFoundsApi.Domain.Dtos.Create;

public class CreateTransactionDto
{
    [Required]
    public double? Amount { get; set; }
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
    public DateTime? Date { get; set; }
    public DateTime? PostingDate { get; set; }
    [StringLength(128)]
    public string ContractorAccountNumber { get; set; }
    [StringLength(256)]
    public string ContractorBankName { get; set; }
    public short? CategoryId { get; set; }
}
