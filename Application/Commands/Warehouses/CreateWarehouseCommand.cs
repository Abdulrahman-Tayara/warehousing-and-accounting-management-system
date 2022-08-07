using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories.UnitOfWork;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Warehouses;

[Authorize(Method = Method.Write, Resource = Resource.Warehouses)]
public class CreateWarehouseCommand : ICreateEntityCommand<int>
{
    public string Name { get; set; }
    public string Location { get; set; }
}

public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, int>
{
    private readonly Lazy<IUnitOfWork> _unitOfWork;

    public CreateWarehouseCommandHandler(Lazy<IUnitOfWork> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        using var unitOfWork = _unitOfWork.Value;
        var cashDrawerAccount = new Account(
            id: default,
            code: "CD_" + request.Name[..2],
            name: "Cash Drawer " + request.Name,
            phone: "",
            city: request.Location
        );

        var accountSaveAction = await unitOfWork.AccountRepository.CreateAsync(cashDrawerAccount);
        cashDrawerAccount = await accountSaveAction();

        var warehouse = new Warehouse(
            id: default,
            name: request.Name,
            location: request.Location,
            cashDrawerAccountId: cashDrawerAccount.Id
        );

        var warehouseSaveAction = await unitOfWork.WarehouseRepository.CreateAsync(warehouse);
        warehouse = await warehouseSaveAction();

        await unitOfWork.CommitAsync();

        return warehouse.Id;
    }
}