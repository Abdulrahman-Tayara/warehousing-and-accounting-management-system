using Application.Common.Mappings;
using Domain.Aggregations;
using wms.Dto.Common;
using wms.Dto.StoragePlaces;

namespace wms.Dto.StoragePlaceQuantity;

public class StoragePlaceQuantityViewModel: IViewModel, IMapFrom<AggregateStoragePlaceQuantity>
{
    public int Quantity { get; set; }

    public StoragePlaceViewModel StoragePlace { get; set; }
}