namespace Application.Common.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class AuthorizeAttribute : Attribute
{
    public Resource Resource { get; set; }
    public Method Method { get; set; }
}