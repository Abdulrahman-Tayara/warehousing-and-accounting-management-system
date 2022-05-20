using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.Accounts;
using wms.Dto.Currencies;
using wms.Dto.Warehouses;

namespace wms.Dto.Invoices;

public class GetInvoiceByIdViewModel : InvoiceViewModel
{
    public AccountViewModel Account { get; set; }
    
    public WarehouseViewModel Warehouse { get; set; }
    
    public CurrencyViewModel? Currency { get; set; }
}