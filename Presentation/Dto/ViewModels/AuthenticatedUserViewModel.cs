using System.ComponentModel.DataAnnotations;

namespace wms.Dto.ViewModels;

public class AuthenticatedUserViewModel
{
    [Required] public string Token { get; set; }

    [Required] public UserViewModel UserViewModel { get; set; }
}