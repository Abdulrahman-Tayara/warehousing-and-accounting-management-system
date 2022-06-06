namespace Infrastructure.Persistence.Database.Models.Common;

public interface ISoftDeletable
{
    public bool IsDeleted { get; set; }
    
    public DateTime? DeletedAt { get; set; }
}