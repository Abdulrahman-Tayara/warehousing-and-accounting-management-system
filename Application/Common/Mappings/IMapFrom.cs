using AutoMapper;

namespace Application.Common.Mappings;

public interface IMapFrom<TFrom>
{
    void Map(Profile profile)
    {
        profile.CreateMap(typeof(TFrom), GetType());
        profile.CreateMap(GetType(), typeof(TFrom));
    }
}