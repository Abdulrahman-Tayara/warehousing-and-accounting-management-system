using Domain.Entities;

namespace Authentication.Dto;

public class JwtLoginResult
{
    public string Token { get; init; }
    
    public User User { get; init; }
}