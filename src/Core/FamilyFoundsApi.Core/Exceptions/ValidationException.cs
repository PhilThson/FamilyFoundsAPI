namespace FamilyFoundsApi.Core;

public class ValidationException : Exception
{
    public ValidationException(List<string> validationResult)
        : base(string.Join(", ", validationResult))
    {

    }
}
