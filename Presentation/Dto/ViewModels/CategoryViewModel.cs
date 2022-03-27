using System.ComponentModel.DataAnnotations;

namespace wms.Dto.ViewModels;

public class CategoryViewModel
{
    [Required] public string Id { get; set; }

    [Required] public string Name { get; set; }
}