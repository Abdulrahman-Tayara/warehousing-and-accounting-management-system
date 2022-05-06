namespace Infrastructure.Persistence.Database.Models.Common;

public static class DbModelsExtensions
{

    public static IQueryable<T> FilterSoftDeletedModels<T>(this IQueryable<T> set)
    {
        if (typeof(T).GetInterface(nameof(ISoftDeletable)) == null)
            return set;

        return set.Where(model => !((ISoftDeletable) model).IsDeleted);
    }
}