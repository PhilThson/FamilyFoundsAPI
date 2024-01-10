namespace FamilyFoundsApi.Core.Persistance.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(IEnumerable<string> validationResult)
        : base(string.Join(", ", validationResult))
    {

    }

    public ValidationException(string message) : base(message)
    {
        
    }
}
