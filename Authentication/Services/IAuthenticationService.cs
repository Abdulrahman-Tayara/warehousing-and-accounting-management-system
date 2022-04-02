using Authentication.Dto;

namespace Authentication.Services;

public interface IAuthenticationService
{
    Task<JwtLoginResult> JwtLogin(JwtLoginRequest request);
}