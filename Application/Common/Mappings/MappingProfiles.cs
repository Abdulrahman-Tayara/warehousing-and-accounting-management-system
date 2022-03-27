using System.Reflection;
using AutoMapper;

namespace Application.Common.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        AddProfiles(Assembly.GetExecutingAssembly());
    }

    private void AddProfiles(Assembly assembly)
    {
        var mapFromTypes = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var mapFromType in mapFromTypes)
        {
            var instance = Activator.CreateInstance(mapFromType);

            var mappingMethod = mapFromType.GetMethod("Map")
                                ?? mapFromType.GetInterface("IMapFrom`1")!.GetMethod("Map");

            mappingMethod?.Invoke(instance, new object[] {this});
        }
    }
}