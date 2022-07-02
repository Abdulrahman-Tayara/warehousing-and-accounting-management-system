using Application.Services.Settings;

namespace Infrastructure.Persistence.Database.Seeders;

public interface ISeeder
{
    public Task Seed(ApplicationDbContext dbContext, IApplicationSettingsProvider settingsProvider);
}