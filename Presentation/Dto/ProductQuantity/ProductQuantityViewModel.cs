using Application.Common.Mappings;
using Domain.Aggregations;
using wms.Dto.Common;
using wms.Dto.Products;

namespace wms.Dto.ProductQuantity;

public class ProductQuantityViewModel : IViewModel, IMapFrom<AggregateProductQuantity>
{
    public ProductViewModel? Product { get; set; }

    public int InputQuantities { get; set; }

    public int OutputQuantities { get; set; }

    public int QuantitySum { get; set; }
}