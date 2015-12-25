using Orchard.Localization;
using Orchard.Security;
using Orchard.UI.Navigation;

namespace Orchard.Core.Settings
{
    public class AdminMenu : INavigationProvider
    {
        public Localizer T { get; set; }

        public string MenuName => "admin";

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.Add(T("Settings"), menu => menu
                .Add(T("HTML Minifier"), "9.9", subMenu => subMenu.Action("Index", "Admin", new { area = "JadeX.HtmlMinifier" }).Permission(StandardPermissions.SiteOwner)
                    .Add(T("Features"), "10.0", item => item.Action("Index", "Admin", new { area = "JadeX.HtmlMinifier" }).Permission(StandardPermissions.SiteOwner).LocalNav())
                    .Add(T("Exclusions"), "11.0", item => item.Action("Exclusions", "Admin", new { area = "JadeX.HtmlMinifier" }).Permission(StandardPermissions.SiteOwner).LocalNav())
            ));
        }
    }
}
