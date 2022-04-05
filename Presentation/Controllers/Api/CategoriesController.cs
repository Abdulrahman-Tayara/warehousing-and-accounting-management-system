using Application.Commands.Categories;
using Application.Queries.Categories;
using AutoMapper;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Category;
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

        var query = new GetCategoryQuery {Id = categoryId};

        var queryResult = await Mediator.Send(query);

        return Ok(queryResult);
    }
}