using Application.Common.Mappings;

namespace Infrastructure.Models;

public class Category : IMapFrom<Domain.Entities.Category>
{
    public int Id { get; set; }

    public string Name { get; set; }
}