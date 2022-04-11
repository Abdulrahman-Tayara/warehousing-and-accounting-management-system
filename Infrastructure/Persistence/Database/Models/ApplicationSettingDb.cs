using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Database.Models;

[Table("Settings")]
public class ApplicationSettingDb
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Key { get; set; } = default!;
    
    public string Value { get; set; } = default!;
}