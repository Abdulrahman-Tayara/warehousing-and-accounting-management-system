namespace Application.Common.QueryFilters;

[AttributeUsage(AttributeTargets.Property)]
public class QueryFilterAttribute : Attribute
{
    public QueryFilterCompareType CompareType;
    public bool IgnoreNullValue;
    public string? FieldName { get; set; }
    
    public QueryFilterAttribute(QueryFilterCompareType compareType)
    {
        CompareType = compareType;
        IgnoreNullValue = true;
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class QueryFiltersConcatAttribute : Attribute
{
    public QueryFilterConcatType ConcatType = QueryFilterConcatType.And;
}