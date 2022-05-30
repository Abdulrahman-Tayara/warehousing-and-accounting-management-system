using Application.Common.Mappings;
using Domain.Aggregations;
using wms.Dto.Common;
using wms.Dto.Products;
using wms.Dto.StoragePlaces;

namespace wms.Dto.StoragePlaceQuantity;

public class StoragePlaceQuantityViewModel : IViewModel, IMapFrom<AggregateStoragePlaceQuantity>
{
    public ProductJoinedViewModel Product { get; set; }

    public StoragePlaceViewModel StoragePlace { get; set; }

    public int Quantity { get; set; }
}