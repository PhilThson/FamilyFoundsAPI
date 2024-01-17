namespace FamilyFoundsApi.Domain.Dtos.Create;

public class ImportFromCsvDto
{
    public Stream FileStream { get; set; }
    public int StartingLine { get; set; }
    public string[] RequiredColumns { get; set; }
}
