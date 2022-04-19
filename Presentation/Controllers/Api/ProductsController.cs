using Application.Commands.Products;
using Application.Queries.Products;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Pagination;
using wms.Dto.Products;

namespace wms.Controllers.Api;

[Authorize]
public class ProductsController : ApiControllerBase
{
    public ProductsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<PageViewModel<ProductJoinedViewModel>>>> GetAllProducts(
        [FromQuery] PaginationRequestParams request)
    {
        var productEntities = await Mediator.Send(request.AsQuery<GetAllProductsQuery>());

        return Ok(productEntities.ToViewModel<ProductJoinedViewModel>(Mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<ProductJoinedViewModel>>> GetProduct(int id)
    {
        var query = new GetProductQuery {Id = id};

        var productEntity = await Mediator.Send(query);

        return Ok(productEntity.ToViewModel<ProductJoinedViewModel>(Mapper));
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<ProductJoinedViewModel>>> CreateProduct(CreateProductRequest request)
    {
        var command = Mapper.Map<CreateProductCommand>(request);

        var createdProductId = await Mediator.Send(command);

        return await GetProduct(createdProductId);
    }
}