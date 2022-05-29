using System.ComponentModel.DataAnnotations;
using Application.Common.Mappings;
using wms.Dto.Common;

namespace wms.Dto.CountryOrigins;

public class CountryOriginViewModel : IViewModel, IMapFrom<Domain.Entities.CountryOrigin>
{
    public int Id { get; init; }
    
    [Required] public string Name { get; init; }
}