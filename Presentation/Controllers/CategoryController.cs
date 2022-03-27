using Microsoft.AspNetCore.Mvc;
using wms.Dto.Requests.Category;
using wms.Dto.Responses.Category;
using wms.Dto.ViewModels;

namespace wms.Controllers;

public class CategoryController : ApiControllerBase
{
    [HttpPost]
    public ActionResult<CreateCategoryResponse> Create(CreateCategoryRequestBody body)
    {
        var createdCategory = CreateCategory(body);

        return Ok(new CreateCategoryResponse(createdCategory));
    }

    private CategoryViewModel CreateCategory(CreateCategoryRequestBody body)
    {
        return new CategoryViewModel {Id = "iducengjihkodfkeihj", Name = body.Name};
    }
}