using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

[Table("Categories")]
public class CategoryDb : IMapFrom<Category>, IDbModel
{
    [Key] public int Id { get; set; }

    public string Name { get; set; }
}