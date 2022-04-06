using AutoMapper;
using Domain.Entities;

namespace wms.Dto.Common;

public static class ViewModelExtensions
{
    public static TViewModel ToViewModel<TViewModel>(this IEntity entity, IMapper mapper)
        where TViewModel : IViewModel
    {
        var vm = mapper.Map<TViewModel>(entity);

        return vm;
    }

    public static IEnumerable<TViewModel> ToViewModels<TViewModel>(this IEnumerable<IEntity> entities,
        IMapper mapper)
        where TViewModel : IViewModel
    {
        return entities.Select(entity => entity.ToViewModel<TViewModel>(mapper));
    }
}