using FamilyFoundsApi.Domain.Dtos.Create;

namespace FamilyFoundsApi.Domain.Dtos.Update;

public class UpdateTransactionDto : CreateTransactionDto
{
    public long Id { get; set; }
}
