using AutoMapper;
using Domain.Entities;
using wms.Dto.Users;

namespace wms.Dto.Common;

public static class ViewModelExtensions
{
    public static UserViewModel ToViewModel(this User user, IMapper mapper)
    {
        var vm = mapper.Map<UserViewModel>(user);

        Console.WriteLine("User id" + vm.Id);
        return vm;
    }
    
    public static IEnumerable<UserViewModel> ToViewModels(this IEnumerable<User> users, IMapper mapper)
    {
        return users.Select(user => user.ToViewModel(mapper));
    }
}