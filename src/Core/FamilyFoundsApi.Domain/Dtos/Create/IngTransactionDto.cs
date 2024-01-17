namespace FamilyFoundsApi.Domain;


public class IngTransactionDto
{
    public DateTime Date { get; set; }
    public DateTime? PostingDate { get; set; }
    public string Contractor { get; set; }
    public string Title { get; set; }
    public string AccountNumber { get; set; }
    public string BankName { get; set; }
    public double? Details { get; set; }
    public string TransactionNumber { get; set; }
    public double Amount { get; set; }
    public string AmountCurrency { get; set; }
    public double? BlockAmount { get; set; }
    public string BlockAmountCurrency { get; set; }
    public double? CurrencyPaymentAmount { get; set; }
    public string CurrencyPaymentAmountCurrency { get; set; }
    public string Account { get; set; }
    public double AfterBalance { get; set; }
    public string AfterBalanceCurrency { get; set; }
}
