namespace Application.Settings;

public class ApplicationSettings
{
    public int DefaultCurrencyId { get; set; } = default;

    public int DefaultSalesAccountId { get; set; } = default;

    public int DefaultPurchasesAccountId { get; set; } = default;

    public int DefaultMainCashDrawerAccountId { get; set; } = default;
    
    public int DefaultMainExportsAccountId { get; set; } = default;
    
    public int DefaultMainImportsAccountId { get; set; } = default;
    
    public int DefaultConversionsAccountId { get; set; } = default;
    
    public int DefaultSalesReturnsAccountId { get; set; } = default;

    public int DefaultPurchasesReturnsAccountId { get; set; } = default;
}