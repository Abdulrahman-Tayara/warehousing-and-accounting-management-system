using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.Common;

namespace wms.Dto.Currencies;

public class CurrencyViewModel : IMapFrom<Currency>, IViewModel
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Symbol { get; set; }
    
    public float Factor { get; set; }
}