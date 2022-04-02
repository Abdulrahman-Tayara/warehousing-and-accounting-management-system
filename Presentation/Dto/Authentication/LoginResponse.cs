
using Domain.Entities;

namespace wms.Dto.Authentication;

public class LoginResponse 
{
    public string Token { get; init; }
    
    public User User { get; init; }
}