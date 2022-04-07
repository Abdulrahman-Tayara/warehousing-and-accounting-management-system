using System.ComponentModel.DataAnnotations;
using Application.Commands.Categories;
using Application.Common.Mappings;

namespace wms.Dto.Category;

public class CreateCategoryRequest : IMapFrom<CreateCategoryCommand>
{
    [Required] public string Name { get; set; }
}