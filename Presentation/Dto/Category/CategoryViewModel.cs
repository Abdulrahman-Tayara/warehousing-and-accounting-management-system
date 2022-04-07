using System.ComponentModel.DataAnnotations;
using Application.Common.Mappings;
using wms.Dto.Common;

namespace wms.Dto.Category;

public class CategoryViewModel : IViewModel, IMapFrom<Domain.Entities.Category>
{
    public int Id { get; init; }
    
    [Required] public string Name { get; init; }
}