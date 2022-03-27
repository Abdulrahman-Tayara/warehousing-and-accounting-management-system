using Microsoft.AspNetCore.Mvc;
using wms.Dto.Requests.Category;
using wms.Dto.Responses.Common;
using wms.Dto.ViewModels;

namespace wms.Controllers;

public class CategoryController : ApiControllerBase
{
    [HttpPost]
    public ActionResult<BaseResponse<CategoryViewModel>> Create(CreateCategoryRequestBody body)
    {
        var createdCategory = CreateCategory(body);

        return Ok(createdCategory, "Category created successfully");
    }

    private CategoryViewModel CreateCategory(CreateCategoryRequestBody body)
    {
        return new CategoryViewModel {Id = "iducengjihkodfkeihj", Name = body.Name};
    }
}