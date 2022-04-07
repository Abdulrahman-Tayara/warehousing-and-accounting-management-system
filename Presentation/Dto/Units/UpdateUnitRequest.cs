using System.ComponentModel.DataAnnotations;
using Application.Commands.Categories;
using Application.Commands.Units;
using Application.Common.Mappings;

namespace wms.Dto.Units;

public class UpdateUnitRequest : IMapFrom<UpdateUnitCommand>
{
    [Required] public string Name { get; init; }

    [Required] public int Value { get; init; }
}