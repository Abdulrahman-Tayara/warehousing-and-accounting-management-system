using System.ComponentModel.DataAnnotations;

namespace wms.Dto.Requests;

public class AuthenticateRequestBody
{
    [Required] public string UserName { get; set; }

    [Required] public string Password { get; set; }
}