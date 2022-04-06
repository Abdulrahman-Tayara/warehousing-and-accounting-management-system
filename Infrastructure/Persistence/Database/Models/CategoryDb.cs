using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Database.Models;


[Table("Categories")]
public class CategoryDb : IDbModel
{
    [Key]
    public int CategoryId { get; set; }
    
    public int Name { get; set; }
}