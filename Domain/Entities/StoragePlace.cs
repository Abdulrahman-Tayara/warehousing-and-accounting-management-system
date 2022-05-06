namespace Domain.Entities;

public class StoragePlace : BaseEntity<int>
{
    public string Name { get; set; }
    
    public int WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }

    public int? ContainerId { get; set; }
    public StoragePlace? Container { get; set; }
    
    public string? Description { get; set; }
    
    public bool HasContainer => ContainerId != null;
}