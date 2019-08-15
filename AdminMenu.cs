using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;

namespace OrchardCore.WebApi
{
    public class AdminMenu : INavigationProvider
    {
        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            S = localizer;
        }
        
        public IStringLocalizer S { get; set; }
        
        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder
                .Add(S["Configuration"], "11", design => design
                    .AddClass("menu-configuration").Id("configuration")
                    .Add(S["WebApi"], "6", deployment => deployment
                        .Action("Index", "Admin", new {area = "OrchardCore.WebApi"})
                        .Permission(Permissions.ExecuteWebApi)
                        .LocalNav()
                    )
                );
            
            return Task.CompletedTask;
        }
    }
}