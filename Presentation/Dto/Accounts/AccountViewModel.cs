using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.Common;

namespace wms.Dto.Accounts;

public class AccountViewModel : IViewModel, IMapFrom<Account>
{
    public int Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public string Phone { get; set; }

    public string City { get; set; }
}