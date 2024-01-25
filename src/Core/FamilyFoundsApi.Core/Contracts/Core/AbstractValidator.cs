using FamilyFoundsApi.Core.Exceptions;
using FamilyFoundsApi.Domain.Common;

namespace FamilyFoundsApi.Core.Contracts.Core;

public abstract class AbstractValidator<T>
{
    private Dictionary<Func<T, Task<bool>>, string> _rules = [];

    protected void AddRule(Func<T, Task<bool>> rule, string errorMessage) => 
        _rules.Add(rule, errorMessage);

    public async Task ValidateAsync(T instance)
    {
        var result = new ValidationResult();

        foreach (var (rule, error) in _rules)
        {
            if (await rule.Invoke(instance) == false)
            {
                result.Errors.Add(error);
            }
        }

        if (!result.IsValid)
            throw new ValidationException(result.Errors);
    }
}
