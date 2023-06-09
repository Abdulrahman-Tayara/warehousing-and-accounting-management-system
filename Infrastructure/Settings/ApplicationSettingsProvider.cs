﻿using Application.Services.Settings;
using Application.Settings;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Database.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Infrastructure.Settings;

public class ApplicationSettingsProvider : IApplicationSettingsProvider
{
    private readonly ApplicationDbContext _context;

    public ApplicationSettingsProvider(ApplicationDbContext context)
    {
        _context = context;
    }

    public ApplicationSettings Get()
    {
        var data = _context.Settings.ToDictionary(setting => setting.Key, setting => setting.Value);

        return JsonConvert.DeserializeObject<ApplicationSettings>(JsonConvert.SerializeObject(data))!;
    }

    public void Configure(ApplicationSettings settings)
    {
        foreach (var property in typeof(ApplicationSettings).GetProperties())
        {
            if (property.GetValue(settings)!.Equals(default))
            {
                return;
            }

            var settingAlreadyCreated = _context.Settings.FirstOrDefault(setting => setting.Key.Equals(property.Name));

            if (settingAlreadyCreated != null)
            {
                if (settingAlreadyCreated.Value.Equals(property.GetValue(settings)!.ToString()!))
                {
                    continue;
                }

                settingAlreadyCreated.Value = property.GetValue(settings)!.ToString()!;
            }
            else
            {
                ApplicationSettingDb settingDb = new ApplicationSettingDb
                {
                    Key = property.Name,
                    Value = property.GetValue(settings)!.ToString()!,
                };

                _context.Add(settingDb);
            }

            _context.SaveChanges();
        }
    }
}