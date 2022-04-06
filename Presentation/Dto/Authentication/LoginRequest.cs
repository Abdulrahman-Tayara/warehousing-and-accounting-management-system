using System.ComponentModel.DataAnnotations;
using Application.Common.Mappings;
using Authentication.Dto;

namespace wms.Dto.Authentication;

public class LoginRequest : IMapFrom<JwtLoginRequest>
{
    [Required] public string Username { get; set; }

    [Required] public string Password { get; set; }
}