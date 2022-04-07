using Application.Commands.Categories;
using Application.Queries.Categories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Category;
using wms.Dto.Common;
using wms.Dto.Common.Responses;

namespace wms.Controllers.Api;

[Authorize]
public class CategoriesController : ApiControllerBase
{
    public CategoriesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<Category>>> CreateCategory(CreateCategoryRequest request)
    {
        var command = Mapper.Map<CreateCategoryCommand>(request);

        var categoryId = await Mediator.Send(command);

        return await GetCategory(categoryId);
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<Category>>> GetCategory(int id)
    {
        var query = new GetCategoryQuery {Id = id};

        var categoryEntity = await Mediator.Send(query);

        return Ok(categoryEntity.ToViewModel<CategoryViewModel>(Mapper));
    }
}