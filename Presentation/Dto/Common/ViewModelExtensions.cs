using Application.Common.Models;
using AutoMapper;
using wms.Dto.Pagination;

namespace wms.Dto.Common;

public static class ViewModelExtensions
{
    public static TViewModel ToViewModel<TViewModel>(this object entity, IMapper mapper)
        where TViewModel : IViewModel
    {
        var vm = mapper.Map<TViewModel>(entity);

        return vm;
    }

    public static IEnumerable<TViewModel> ToViewModels<TViewModel>(this IEnumerable<object> entities,
        IMapper mapper)
        where TViewModel : IViewModel
    {
        return mapper.Map<IEnumerable<object>, IEnumerable<TViewModel>>(entities);
    }

    public static PageViewModel<TViewModel> ToViewModel<TViewModel>(
        this IPaginatedEnumerable<object> page,
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