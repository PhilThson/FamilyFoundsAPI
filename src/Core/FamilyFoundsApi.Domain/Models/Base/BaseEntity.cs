using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyFoundsApi.Domain.Models.Base;

public class BaseEntity<T>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public T Id { get; set; }
}
