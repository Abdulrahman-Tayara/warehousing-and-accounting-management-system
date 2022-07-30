using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.Common;
using wms.Dto.Invoices;
using wms.Dto.Products;
using wms.Dto.StoragePlaces;
using wms.Dto.Warehouses;

namespace wms.Dto.Conversions;

public class ConversionViewModel : IViewModel, IMapFrom<Conversion>
{
    public int Id { get; set; }
    
    public WarehouseViewModel FromWarehouse { get; set; }
    
    public WarehouseViewModel ToWarehouse { get; set; }
    
    public ProductJoinedViewModel FromProduct { get; set; }
    
    public ProductJoinedViewModel ToProduct { get; set; }
    
    public StoragePlaceViewModel FromPlace { get; set; }
    
    public StoragePlaceViewModel ToPlace { get; set; }
    
    public int FromQuantity { get; set; }
    
    public int ToQuantity { get; set; }
    
    public string Note { get; set; }
    
    public int ExportInvoiceId { get; set; }
    
    public int ImportInvoiceId { get; set; }
}