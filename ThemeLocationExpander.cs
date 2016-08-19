using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
namespace ExtCore.ThemeManager
{
    public class ThemeLocationExpander: IViewLocationExpander
    {
        private IConfigurationRoot _configuration;
        private const string THEME_KEY = "THEME_KEY";
        public ThemeLocationExpander(IConfigurationRoot configurationRoot)
        {
            this._configuration = configurationRoot;
        }
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            string theme = null;
            if (context.Values.TryGetValue(THEME_KEY, out theme))
            {
                IEnumerable<string> themeLocations = new[]
                {
                $"/Themes/{theme}/{{1}}/{{0}}.cshtml",
                $"/Themes/{theme}/Shared/{{0}}.cshtml"
            };
                viewLocations = themeLocations.Concat(viewLocations);
            }
            foreach (var el in viewLocations) System.Console.WriteLine(el);
            return viewLocations;
        }
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values[THEME_KEY] = _configuration["ThemeManager:Theme"];
        }
    }
}
