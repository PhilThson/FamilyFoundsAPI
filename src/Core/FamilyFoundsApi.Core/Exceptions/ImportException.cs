namespace FamilyFoundsApi.Core.Exceptions;

public class ImportException : Exception
{
    private const string DEFAULT_MESSAGE = "Wystąpił błąd podczas importu danych.";
    private const string EXTENDED_MESSAGE = DEFAULT_MESSAGE + " Upewnij się że plik posiada kodowanie UTF-8.";

    public ImportException(string message) : base(message)
    {
        
    }

    public ImportException(Exception innerException)
        : base(EXTENDED_MESSAGE + "Błąd wewnętrzny: " + innerException.Message)
    {

    }

    public ImportException() : base(DEFAULT_MESSAGE)
    {
        
    }
}
