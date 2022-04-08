using Application.Settings;

namespace Application.Services.Settings;

public interface IApplicationSettingsProvider
{
    public ApplicationSettings Get();

    public void Configure(ApplicationSettings settings);
}