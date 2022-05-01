using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.Common;

namespace wms.Dto.CurrencyAmounts;

public class CurrencyAmountViewModel : IViewModel, IMapFrom<CurrencyAmount>
{
    public int ObjectId { get; set; }
    
    public CurrencyAmountKey Key { get; set; }
    
    public double Amount { get; set; }
    
    public Currency Currency { get; set; }
}