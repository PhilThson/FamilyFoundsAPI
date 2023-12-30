namespace FamilyFoundsApi.Domain;

public class ReadTransactionDto
{
    public long Id { get; set; }
    public double Amount { get; set; }
    public string Contractor { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public DateTime? PostingDate { get; set; }
}
