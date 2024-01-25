namespace FamilyFoundsApi.Domain.Dtos.Read;

public class ReadTransactionDto
{
    public long Id { get; set; }
    public double Amount { get; set; }
    public string Currency { get; set; }
    public string Account { get; set; }
    public string Contractor { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public DateTime? PostingDate { get; set; }
    public string Number { get; set; }
    public string ContractorAccountNumber { get; set; }
    public string ContractorBankName { get; set; }
    public ReadCategoryDto Category { get; set; }
    public ReadImportSourceDto ImportSource { get; set; }
}
