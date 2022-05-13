using Application.Repositories;
using FluentValidation;

namespace Application.Commands.StoragePlaces;

public class CreateStoragePlaceValidation : AbstractValidator<CreateStoragePlaceCommand>
{
    private readonly IWarehouseRepository _warehouseRepository;

    public CreateStoragePlaceValidation(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;

        RuleFor(command => command.WarehouseId)
            .MustAsync(_warehouseExists).WithMessage("Warehouse id is not found");
    }

    private Task<bool> _warehouseExists(int warehouseId, CancellationToken cancellationToken)
    {
        return _warehouseRepository.IsExistsById(warehouseId);
    }
}