using Application.Commands.Currencies;
using Application.Common.Mappings;

namespace wms.Dto.Currencies;

public class UpdateCurrencyRequest : IMapFrom<UpdateCurrencyCommand>
{
    public string Name { get; set; }

    public string Symbol { get; set; }

    public float Factor { get; set; }
}