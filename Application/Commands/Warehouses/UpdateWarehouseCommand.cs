using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Warehouses;

public class UpdateWarehouseCommand : IUpdateEntityCommand<int>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Location { get; set; }
}

public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, int>
{
    private readonly IWarehouseRepository _repository;

    public UpdateWarehouseCommandHandler(IWarehouseRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var oldWarehouse = await _repository.FindByIdAsync(request.Id);

        await _repository.Update(
            new Warehouse(
                id: request.Id,
                name: request.Name,
                location: request.Location,
                cashDrawerAccountId: oldWarehouse.CashDrawerAccountId
            )
        );

        await _repository.SaveChanges();

        return request.Id;
    }
}