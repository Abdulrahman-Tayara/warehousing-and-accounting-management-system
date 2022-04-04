using Application.Common.Mappings;

namespace Infrastructure.Models;

public class Category : IMapFrom<Domain.Entities.Category>
{
    public int id { get; set; }

    public string name { get; set; }
}