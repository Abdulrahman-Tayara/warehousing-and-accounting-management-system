
using Domain.Entities;
using wms.Dto.Users;

namespace wms.Dto.Authentication;

public class LoginResponse 
{
    public string Token { get; init; }
    
    public UserViewModel User { get; init; }
}