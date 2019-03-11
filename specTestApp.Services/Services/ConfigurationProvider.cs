﻿using System;
using System.Configuration;
using System.Linq;
using Newtonsoft.Json;
using specTestApp.Services.Interfaces;

namespace specTestApp.Services.Services
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public T GetConfig<T>(string name, T defaultValue)
        {
            try
            {
                if (!ConfigurationManager.AppSettings.AllKeys.Contains(name))
                {
                    return defaultValue;
                }
                var configValueString = ConfigurationManager.AppSettings[name];
                var configValue = JsonConvert.DeserializeObject<T>(configValueString);
                return configValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public string GetConfig(string name)
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Contains(name))
            {
                return string.Empty;
            }
            var configvalue = ConfigurationManager.AppSettings[name];
            return configvalue;
        }
    }
}
