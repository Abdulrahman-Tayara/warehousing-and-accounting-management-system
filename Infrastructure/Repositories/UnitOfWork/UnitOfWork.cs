using Application.Repositories;
using Application.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly Lazy<AccountRepository> _accountRepository;
    private readonly Lazy<CategoryRepository> _categoryRepository;
    private readonly Lazy<CurrencyAmountRepository> _currencyAmountRepository;
    private readonly Lazy<CurrencyRepository> _currencyRepository;
    private readonly Lazy<InvoiceRepository> _invoiceRepository;
    private readonly Lazy<ManufacturerRepository> _manufacturerRepository;
    private readonly Lazy<ProductMovementRepository> _productMovementRepository;
    private readonly Lazy<ProductRepository> _productRepository;
    private readonly Lazy<StoragePlaceRepository> _storagePlaceRepository;
    private readonly Lazy<UnitRepository> _unitRepository;
    private readonly Lazy<UserRepository> _userRepository;
    private readonly Lazy<WarehouseRepository> _warehouseRepository;

    private readonly IDbContextTransaction _transaction;

    public UnitOfWork(Lazy<AccountRepository> accountRepository, Lazy<CategoryRepository> categoryRepository,
        Lazy<CurrencyAmountRepository> currencyAmountRepository, Lazy<CurrencyRepository> currencyRepository,
        Lazy<InvoiceRepository> invoiceRepository, Lazy<ManufacturerRepository> manufacturerRepository,
        Lazy<ProductMovementRepository> productMovementRepository, Lazy<ProductRepository> productRepository,
        Lazy<StoragePlaceRepository> storagePlaceRepository, Lazy<UnitRepository> unitRepository,
        Lazy<UserRepository> userRepository, Lazy<WarehouseRepository> warehouseRepository, DbContext dbContext)
    {
        _accountRepository = accountRepository;
        _categoryRepository = categoryRepository;
        _currencyAmountRepository = currencyAmountRepository;
        _currencyRepository = currencyRepository;
        _invoiceRepository = invoiceRepository;
        _manufacturerRepository = manufacturerRepository;
        _productMovementRepository = productMovementRepository;
        _productRepository = productRepository;
        _storagePlaceRepository = storagePlaceRepository;
        _unitRepository = unitRepository;
        _userRepository = userRepository;
        _warehouseRepository = warehouseRepository;
        _transaction = dbContext.Database.BeginTransaction();
    }

    public IAccountRepository AccountRepository => _accountRepository.Value;

    public ICategoryRepository CategoryRepository => _categoryRepository.Value;

    public ICurrencyAmountRepository CurrencyAmountRepository => _currencyAmountRepository.Value;

    public ICurrencyRepository CurrencyRepository => _currencyRepository.Value;

    public IInvoiceRepository InvoiceRepository => _invoiceRepository.Value;

    public IManufacturerRepository ManufacturerRepository => _manufacturerRepository.Value;

    public IProductMovementRepository ProductMovementRepository => _productMovementRepository.Value;

    public IProductRepository ProductRepository => _productRepository.Value;

    public IStoragePlaceRepository StoragePlaceRepository => _storagePlaceRepository.Value;

    public IUnitRepository UnitRepository => _unitRepository.Value;

    public IUserRepository UserRepository => _userRepository.Value;

    public IWarehouseRepository WarehouseRepository => _warehouseRepository.Value;

    public void Commit()
    {
        _transaction.Commit();
    }

    public void Dispose()
    {
        _transaction.Dispose();
        GC.SuppressFinalize(this);
    }
}