namespace FamilyFoundsApi.Core.Exceptions;

public class ImportException : Exception
{
    public ImportException(string message) : base(message)
    {
        
    }

    public ImportException() : base("Wystąpił błąd podczas importu danych")
    {
        
    }
}
