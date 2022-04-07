using System.ComponentModel.DataAnnotations;
using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

public class UnitDb : IDbModel, IMapFrom<Unit>
{
    [Key] public int Id { get; set; }

    public string Name { get; set; }

    public int Value { get; set; }
}