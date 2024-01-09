namespace FamilyFoundsApi.Domain.Dtos.Read;

public class ReadDictionaryEntityDto<T>
{
    public T Id { get; set; } 
    public string Name { get; set; }
};
