using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Controllers.Api;
using wms.Dto.Common.Responses;
using wms.Dto.StoragePlaces;

namespace wms.Controllers;

[Authorize]
[Route("api/warehouses/{warehouseId}/places")]
public class StoragePlacesController : ApiControllerBase
{
    public StoragePlacesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStoragePlaceRequest request)
    {
        Console.WriteLine("W Id " + request.WarehouseId);

        return Ok();
    }
}