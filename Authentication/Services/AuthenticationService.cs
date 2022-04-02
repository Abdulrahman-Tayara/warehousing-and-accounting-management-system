using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Exceptions;
using Application.Services.Identity;
using Authentication.Dto;
using Authentication.Options;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using InvalidCredentialException = Application.Exceptions.InvalidCredentialException;

namespace Authentication.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IIdentityService _identityService;
    private readonly JwtOptions _jwtOptions;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(IIdentityService identityService, IOptions<JwtOptions> jwtOptions,
        ILogger<AuthenticationService> logger)
    {
        _identityService = identityService;
        _jwtOptions = jwtOptions.Value;
        _logger = logger;
    }

    public async Task<JwtLoginResult> JwtLogin(JwtLoginRequest request)
    {
        var user = await _findUserByName(request.Username);

        var validPassword = await _identityService.CheckPasswordAsync(user, request.Password);

        if (!validPassword)
            throw new InvalidCredentialException();

        var token = _generateToken(user);

        return new JwtLoginResult()
        {
            Token = token,
            User = user
        };
    }

    private async Task<User> _findUserByName(string username)
    {
        try
        {
            var user = await _identityService.FindUserByNameAsync(username);
            return user;
        }
        catch (NotFoundException _)
        {
            throw new InvalidCredentialException();
        }
    }

    private string _generateToken(User user)
    {
        var secretKey = System.Text.Encoding.ASCII.GetBytes(_jwtOptions.IssuerSigningKey);

        var expireAt = _jwtOptions.ExpirationDate;

        var jwtToken = new JwtSecurityToken(issuer: _jwtOptions.ValidIssuer,
            audience: _jwtOptions.ValidAudience,
            claims: _claims(user),
            notBefore: new DateTimeOffset(DateTime.Now).DateTime,
            expires: new DateTimeOffset(expireAt).DateTime,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    private IEnumerable<Claim> _claims(User user) =>
        new[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
}