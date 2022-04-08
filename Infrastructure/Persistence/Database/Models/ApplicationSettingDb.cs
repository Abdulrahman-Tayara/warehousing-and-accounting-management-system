using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Database.Models;

[Table("Settings")]
public class ApplicationSettingDb
{
    [Key]
    public string Key { get; set; }
    
    public string Value { get; set; }
}