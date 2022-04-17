using Application.Commands.Categories;
using Application.Queries.Categories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Categories;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Pagination;

namespace wms.Controllers.Api;

[Authorize]
public class CategoriesController : ApiControllerBase
{
    public CategoriesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<CategoryViewModel>>> CreateCategory(CreateCategoryRequest request)
    {
        var command = Mapper.Map<CreateCategoryCommand>(request);

        var categoryId = await Mediator.Send(command);

        return await GetCategory(categoryId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<CategoryViewModel>>> GetCategory(int id)
    {
        var query = new GetCategoryQuery {Id = id};

        var categoryEntity = await Mediator.Send(query);

        return Ok(categoryEntity.ToViewModel<CategoryViewModel>(Mapper));
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<PageViewModel<CategoryViewModel>>>> GetCategories(
        [FromQuery] PaginationRequestParams request)
    {
        var query = request.AsQuery<GetAllCategoriesQuery>();

        var categoryEntities = await Mediator.Send(query);

        return Ok(categoryEntities.ToViewModel<CategoryViewModel>(Mapper));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BaseResponse<CategoryViewModel>>> CreateCategory(UpdateCategoryRequest request,
        int id)
    {
        var command = Mapper.Map<UpdateCategoryCommand>(request);
        command.Id = id;

        var updatedCategoryId = await Mediator.Send(command);

        return await GetCategory(updatedCategoryId);
    }
}