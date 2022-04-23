using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using wms.Dto.Pagination;

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
        return mapper.Map<IEnumerable<IEntity>, IEnumerable<TViewModel>>(entities);
    }

    public static PageViewModel<TViewModel> ToViewModel<TViewModel>(
        this IPaginatedEnumerable<IEntity> page,
        IMapper mapper)
        where TViewModel : IViewModel
    {
        return new PageViewModel<TViewModel>
        {
            Data = page.ToViewModels<TViewModel>(mapper),
            CurrentPage = page.CurrentPage,
            PagesCount = page.PagesCount,
            PageSize = page.PageSize,
            RowsCount = page.RowsCount
        };
    }
}