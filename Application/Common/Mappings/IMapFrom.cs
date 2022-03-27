using AutoMapper;

namespace Application.Common.Mappings;

public interface IMapFrom<TFrom>
{
    void Map(Profile profile) => profile.CreateMap(typeof(TFrom), GetType());
}