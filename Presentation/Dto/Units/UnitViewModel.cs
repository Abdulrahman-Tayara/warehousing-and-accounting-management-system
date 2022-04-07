using System.ComponentModel.DataAnnotations;
using Application.Common.Mappings;
using wms.Dto.Common;

namespace wms.Dto.Units;

public class UnitViewModel : IViewModel, IMapFrom<Domain.Entities.Unit>
{
    public int Id { get; init; }

    public string Name { get; init; }

    public int Value { get; init; }
}