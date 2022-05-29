using System.ComponentModel.DataAnnotations;
using Application.Commands.CountryOrigins;
using Application.Common.Mappings;

namespace wms.Dto.CountryOrigins;

public class CreateCountryOriginRequest : IMapFrom<CreateCountryOriginCommand>
{
    [Required] public string Name { get; set; }
}