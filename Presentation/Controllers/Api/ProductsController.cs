using System.ComponentModel.DataAnnotations;
using Application.Commands.Products;
using Application.Queries.Invoicing;
using Application.Queries.Invoicing.Dto;
using Application.Queries.Products;
using AutoMapper;
using Domain.Exceptions;
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
        [FromQuery] ProductsQueryParams request)
    {
        var productEntities = await Mediator.Send(request.AsQuery<GetAllProductsQuery>(Mapper));

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

    [HttpGet("{id}/checkQuantity")]
    public async Task CheckQuantity(int id, [FromQuery] [Required] int quantity)
    {
        await Mediator.Send(new CheckProductQuantityQuery
        {
            ProductQuantities = new[] {new CheckProductQuantityDto {ProductId = id, Quantity = quantity}}
        });
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<BaseResponse<ProductJoinedViewModel>>> UpdateProduct(int id, UpdateProductRequest request)
    {
        var command = Mapper.Map<UpdateProductCommand>(request);
        command.Id = id;

        var _ = await Mediator.Send(command);

        return await GetProduct(id);
    }
    
    [HttpDelete("{id}")]
    public async Task DeleteProduct(int id)
    {
        await Mediator.Send(new DeleteProductCommand() {key = id});
    }
}