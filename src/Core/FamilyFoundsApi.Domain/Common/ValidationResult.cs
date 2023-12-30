namespace FamilyFoundsApi.Domain;

public class ValidationResult
{
    public List<string> Errors { get; set; }
    public bool IsValid => Errors.Count == 0;

    public ValidationResult()
    {
        Errors = new List<string>();
    }

    public override string ToString()
    {
        return string.Join(", ", Errors);
    }
}
