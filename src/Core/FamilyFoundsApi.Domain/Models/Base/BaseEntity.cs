using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyFoundsApi.Domain;

public class BaseEntity<T>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public T Id { get; set; }
}
