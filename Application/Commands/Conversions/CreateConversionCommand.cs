using Application.Commands.Invoicing;
using Application.Commands.Invoicing.Dto;
using Application.Repositories.UnitOfWork;
using Application.Settings;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Conversions;

public class CreateConversionCommand : IRequest<int>
{
    public int FromWarehouseId { get; set; }

    public int ToWarehouseId { get; set; }

    public int FromProductId { get; set; }

    public int ToProductId { get; set; }

    public int FromPlaceId { get; set; }

    public int ToPlaceId { get; set; }

    public int FromQuantity { get; set; }

    public int ToQuantity { get; set; }

    public string? Note { get; set; }

    public bool IgnoreMinLevelWarnings { get; set; } = false;
}

public class CreateConversionCommandHandler : IRequestHandler<CreateConversionCommand, int>
{
    private readonly Lazy<IUnitOfWork> _unitOfWork;
    private readonly IMediator _mediator;
    private readonly ApplicationSettings _applicationSettings;

    public CreateConversionCommandHandler(Lazy<IUnitOfWork> unitOfWork, IMediator mediator,
        ApplicationSettings applicationSettings)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _applicationSettings = applicationSettings;
    }

    public async Task<int> Handle(CreateConversionCommand request, CancellationToken cancellationToken)
    {
        var defaultCurrencyId = _applicationSettings.DefaultCurrencyId;
        var defaultConversionsAccount = _applicationSettings.DefaultConversionsAccountId;

        using (var unitOfWork = _unitOfWork.Value)
        {
            var exportInvoiceId = await _mediator.Send(new CreateInvoiceCommand
            {
                AccountId = defaultConversionsAccount,
                WarehouseId = request.FromWarehouseId,
                CurrencyId = defaultCurrencyId,
                Note = null,
                Type = InvoiceType.Out,
                AccountType = InvoiceAccountType.ImportExport,
                Items = new[]
                {
                    new InvoiceItemDto
                    {
                        ProductId = request.FromProductId,
                        UnitPrice = 0,
                        Quantity = request.FromQuantity,
                        CurrencyId = defaultCurrencyId,
                        PlaceId = request.FromPlaceId,
                        Note = request.Note,
                        CurrencyAmounts = null
                    }
                },
                IgnoreMinLevelWarnings = request.IgnoreMinLevelWarnings
            }, cancellationToken);

            var importInvoiceId = await _mediator.Send(new CreateInvoiceCommand
            {
                AccountId = defaultConversionsAccount,
                WarehouseId = request.FromWarehouseId,
                CurrencyId = defaultCurrencyId,
                Note = null,
                Type = InvoiceType.In,
                AccountType = InvoiceAccountType.ImportExport,
                Items = new[]
                {
                    new InvoiceItemDto
                    {
                        ProductId = request.ToProductId,
                        UnitPrice = 0,
                        Quantity = request.ToQuantity,
                        CurrencyId = defaultCurrencyId,
                        PlaceId = request.ToPlaceId,
                        Note = request.Note,
                        CurrencyAmounts = null
                    }
                },
                IgnoreMinLevelWarnings = request.IgnoreMinLevelWarnings
            }, cancellationToken);

            var saveConversionAction = await unitOfWork.ConversionRepository.CreateAsync(
                new Conversion
                {
                    FromWarehouseId = request.ToWarehouseId,
                    ToWarehouseId = request.ToWarehouseId,
                    FromProductId = request.FromProductId,
                    ToProductId = request.ToProductId,
                    FromPlaceId = request.FromPlaceId,
                    ToPlaceId = request.ToPlaceId,
                    FromQuantity = request.FromQuantity,
                    ToQuantity = request.ToQuantity,
                    Note = request.Note,
                    ImportInvoiceId = importInvoiceId,
                    ExportInvoiceId = exportInvoiceId
                });

            var conversion = await saveConversionAction();

            await unitOfWork.CommitAsync();

            return conversion.Id;
        }
    }
}