using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Database.Models;

public static class ModelsExtensions
{
    public static object Id(this IDbModel model)
    {
        var field = model.GetType().GetField("Id");
        
        if (field == null)
        {
            throw new ValidationException(model.GetType() + " has no Id field");
        }

        return field.GetValue(model)!;
    }
}