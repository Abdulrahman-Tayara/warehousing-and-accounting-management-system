using System.Linq.Expressions;

namespace Infrastructure;

public class FilterConditionAdapter<TFilter, TModel>
{
    public TFilter Filter;
    public TModel Model;

    public FilterConditionAdapter(TFilter filter, TModel model)
    {
        Filter = filter;
        Model = model;
    }

    public Expression<Func<TModel, bool>> GenerateConditions()
    {
        Expression<Func<TModel, bool>> expression = model => model != null;

        foreach (var propertyInfo in Filter!.GetType().GetProperties())
        {
            var modelProperties = Model!.GetType().GetProperties();

            var filterValue = propertyInfo.GetValue(Filter);
        }

        return expression;
    }
}