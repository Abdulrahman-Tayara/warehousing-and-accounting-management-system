using System.Text.Json.Serialization;
using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.Common;

namespace wms.Dto.Users;

public class UserViewModel : IMapFrom<User>, IViewModel
{
    public int Id { get; set; }
    
    public string Username { get; set; }
    
    [JsonIgnore]
    public string? PasswordHash { get; set; }
}