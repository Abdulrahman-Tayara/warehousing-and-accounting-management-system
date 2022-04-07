using System.ComponentModel.DataAnnotations;
using Application.Commands.Categories;
using Application.Common.Mappings;

namespace wms.Dto.Category;

public class UpdateCategoryRequest : IMapFrom<UpdateCategoryCommand>
{
    [Required] public string Name { get; init; }
}