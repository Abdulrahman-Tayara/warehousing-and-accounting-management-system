using System.ComponentModel.DataAnnotations;
using Application.Commands.Categories;
using Application.Commands.Units;
using Application.Common.Mappings;

namespace wms.Dto.Units;

public class CreateUnitRequest : IMapFrom<CreateUnitCommand>
{
    [Required] public string Name { get; set; }

    [Required] public int Value { get; set; }
}