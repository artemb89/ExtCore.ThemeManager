using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace ExtCore.ThemeManager
{
    using Infrastructure;

    public class Startup:IExtension
    {
        private IConfigurationRoot _configuration;
        public Startup()
        {
        }

        public IEnumerable<KeyValuePair<int, Action<IApplicationBuilder>>> ConfigureActionsByPriorities
        {
            get
            {
                return null;
            }
        }

        public IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IServiceCollection>>()
                {
                    [2000] = services =>
                    {
                        services.Configure<RazorViewEngineOptions>(options =>
                        {
                            options.ViewLocationExpanders.Add(new ThemeLocationExpander(_configuration));
                        }
                        );
                    }
                };
            }
        }

        public string Name
        {
            get
            {
                return "ExtCore Theme Manager";
            }
        }

        public void SetConfigurationRoot(IConfigurationRoot configurationRoot)
        {
            this._configuration = configurationRoot;
        }

        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
        }
    }
}
