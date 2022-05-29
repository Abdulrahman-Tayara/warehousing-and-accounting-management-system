using System.ComponentModel.DataAnnotations;
using Application.Commands.Categories;
using Application.Commands.CountryOrigins;
using Application.Common.Mappings;

namespace wms.Dto.CountryOrigins;

public class UpdateCountryOriginRequest : IMapFrom<UpdateCountryOriginCommand>
{
    [Required] public string Name { get; init; }
}