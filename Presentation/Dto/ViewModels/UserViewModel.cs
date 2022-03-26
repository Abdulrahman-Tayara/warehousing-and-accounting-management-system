using System.ComponentModel.DataAnnotations;

namespace wms.Dto.ViewModels;

public class UserViewModel
{
    [Required] public string Username { get; set; }
}