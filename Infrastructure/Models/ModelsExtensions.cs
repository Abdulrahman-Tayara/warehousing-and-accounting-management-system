using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public static class ModelsExtensions
{
    public static object Id<T>(this T model)
    {
        var field = typeof(T).GetField("Id");
        
        if (field == null)
        {
            throw new ValidationException(typeof(T) + " has no Id field");
        }

        return field.GetValue(model)!;
    }
}