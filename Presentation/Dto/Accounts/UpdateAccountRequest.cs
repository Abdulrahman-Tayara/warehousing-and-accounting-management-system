using System.ComponentModel.DataAnnotations;
using Application.Commands.Accounts;
using Application.Common.Mappings;

namespace wms.Dto.Accounts;

public class UpdateAccountRequest : IMapFrom<UpdateAccountCommand>
{
    [Required] public string Code { get; set; }

    [Required] public string Name { get; set; }

    [Required] public string Phone { get; set; }

    [Required] public string City { get; set; }
}