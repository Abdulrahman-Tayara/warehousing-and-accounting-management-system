using System.Linq.Expressions;
using System.Reflection;

namespace Application.Common.QueryFilters;

public class FilterConditionAdapter<TModel>
{
    private readonly object _filter;

    private readonly ParameterExpression _parameter;

    public FilterConditionAdapter(object filter)
    {
        _filter = filter;
        _parameter = Expression.Parameter(typeof(TModel), "model"); // model => ...
    }

    public Expression<Func<TModel, bool>>? Build()
    {
        var filterProperties = _filter.GetType().GetProperties()
            .Where(p => p.GetCustomAttribute(typeof(QueryFilterAttribute)) is not null);

        Expression? expression = null;

        foreach (var filterProperty in filterProperties)
        {
            var propertyExpression = _buildPropertyExpression(filterProperty);

            if (propertyExpression is null)
                continue;

            expression = expression is null
                ? propertyExpression
                : _concatExpressions(expression, propertyExpression);
        }

        return expression is null ? null : Expression.Lambda<Func<TModel, bool>>(expression, _parameter);
    }

    private Expression _concatExpressions(Expression left, Expression right)
    {
        var queryFilterConcatAttribute =
            (QueryFiltersConcatAttribute?) _filter.GetType().GetCustomAttribute(typeof(QueryFiltersConcatAttribute));
        
        if (queryFilterConcatAttribute is null || queryFilterConcatAttribute.ConcatType == QueryFilterConcatType.And)
            return Expression.AndAlso(left, right);
        
        return Expression.OrElse(left, right);
    }

    private Expression? _buildPropertyExpression(PropertyInfo property)
    {
        var queryFilterAttribute = (QueryFilterAttribute) property.GetCustomAttribute(typeof(QueryFilterAttribute))!;

        var fieldName = property.Name;

        if (queryFilterAttribute.FieldName is not null)
        {
            fieldName = queryFilterAttribute.FieldName;
        }

        var modelProperty = typeof(TModel).GetProperty(fieldName);
        if (modelProperty is null)
            return null;

        var value = property.GetValue(_filter);

        if (queryFilterAttribute.IgnoreNullValue && value is null)
            return null;

        var modelExpressionProperty = Expression.Property(_parameter, fieldName); // model.PropertyName

        var method = _findMatchedCompareTypeMethod(queryFilterAttribute.CompareType);

        return (Expression) method.Invoke(
            this,
            new object[]
            {
                modelExpressionProperty,
                Expression.Constant(value)
            }
        )!;
    }

    private MethodInfo _findMatchedCompareTypeMethod(QueryFilterCompareType compareType)
    {
        var method = GetType()
            .GetMethods()
            .FirstOrDefault(m =>
                m.Name.EndsWith("Expression") && m.Name.ToLower().StartsWith(compareType.ToString().ToLower()));

        if (method is null)
            throw new InvalidOperationException($"There is no expression for {compareType.ToString()} compare type");

        return method;
    }

    public Expression EqualExpression(Expression left, Expression right) 
        => Expression.Equal(left, Expression.Convert(right, left.Type));
    public Expression GreaterThanExpression(Expression left, Expression right) =>
        Expression.GreaterThan(left, Expression.Convert(right, left.Type));

    public Expression GreaterThanOrEqualExpression(Expression left, Expression right) =>
        Expression.GreaterThanOrEqual(left, Expression.Convert(right, left.Type));

    public Expression LessThanExpression(Expression left, Expression right) => 
        Expression.LessThan(left, Expression.Convert(right, left.Type));

    public Expression LessThanOrEqualExpression(Expression left, Expression right) =>
        Expression.LessThanOrEqual(left, Expression.Convert(right, left.Type));

    public Expression StringContainsExpression(Expression left, Expression right)
    {
        var method = typeof(string).GetMethod("Contains", new[] {typeof(string)});
        return Expression.Call(left, method!, Expression.Convert(right, left.Type));
    }

    public Expression InArrayExpression(Expression left, Expression right)
    {
        var method = typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public)
            .Single(x => x.Name == "Contains" && x.GetParameters().Length == 2)
            .MakeGenericMethod(left.Type);

        return Expression.Call(method!, right, left);
    }
}