using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

[Table("Units")]
public class UnitDb : IDbModel, IMapFrom<Unit>
{
    [Key] public int Id { get; set; }

    public string Name { get; set; }

    public int Value { get; set; }
}