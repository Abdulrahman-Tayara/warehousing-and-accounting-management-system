using System.ComponentModel.DataAnnotations;

namespace wms.Dto.Category;

public class CreateCategoryRequest
{
    [Required] public string Name { get; set; }
}