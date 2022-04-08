using Application.Commands.Currencies.CreateCurrency;
using Application.Common.Mappings;

namespace wms.Dto.Currencies;

public class CreateCurrencyRequest : IMapFrom<CreateCurrencyCommand>
{
    public string Name { get; set; }
    
    public string Symbol { get; set; }
    
    public float Factor { get; set; }
}