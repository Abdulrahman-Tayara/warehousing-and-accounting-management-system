using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Extensions;

public static class IdentityResultExtensions
{
    public static string? GetErrorsAsString(this IdentityResult result)
    {
        return result.Errors.FirstOrDefault(e => true)?.Description;
    }
}