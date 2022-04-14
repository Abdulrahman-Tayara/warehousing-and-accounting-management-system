using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

[Table("Accounts")]
public class AccountDb : IMapFrom<Account>, IDbModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } = default;

    public string Name { get; set; } = default!;

    public string Code { get; set; } = default!;

    public string Phone { get; set; } = default!;

    public string City { get; set; } = default!;
}