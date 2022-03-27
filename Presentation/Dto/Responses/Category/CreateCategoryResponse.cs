using wms.Dto.Responses.Common;
using wms.Dto.ViewModels;

namespace wms.Dto.Responses.Category;

public class CreateCategoryResponse : BaseResponse<CategoryViewModel>
{
    public CreateCategoryResponse(CategoryViewModel data)
        : base(new ResponseMetaData() {message = "Category created successfully"}, data)
    {
    }
}