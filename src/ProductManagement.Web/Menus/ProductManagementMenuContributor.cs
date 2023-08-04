using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using ProductManagement.Localization;
using ProductManagement.Permissions;
using Volo.Abp.AuditLogging.Web.Navigation;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.LanguageManagement.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TextTemplateManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.OpenIddict.Pro.Web.Menus;
using Volo.Abp.UI.Navigation;
using Volo.Saas.Host.Navigation;
using Volo.Abp.Users;

namespace ProductManagement.Web.Menus;

public class ProductManagementMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<ProductManagementResource>();

        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                ProductManagementMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );

        //HostDashboard
        context.Menu.AddItem(
            new ApplicationMenuItem(
                ProductManagementMenus.HostDashboard,
                l["Menu:Dashboard"],
                "~/HostDashboard",
                icon: "fa fa-line-chart",
                order: 2
            ).RequirePermissions(ProductManagementPermissions.Dashboard.Host)
        );

        //TenantDashboard
        context.Menu.AddItem(
            new ApplicationMenuItem(
                ProductManagementMenus.TenantDashboard,
                l["Menu:Dashboard"],
                "~/Dashboard",
                icon: "fa fa-line-chart",
                order: 2
            ).RequirePermissions(ProductManagementPermissions.Dashboard.Tenant)
        );

        context.Menu.SetSubItemOrder(SaasHostMenuNames.GroupName, 3);

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 5;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);

        //Administration->OpenIddict
        administration.SetSubItemOrder(OpenIddictProMenus.GroupName, 2);

        //Administration->Language Management
        administration.SetSubItemOrder(LanguageManagementMenuNames.GroupName, 3);

        //Administration->Text Template Management
        administration.SetSubItemOrder(TextTemplateManagementMainMenuNames.GroupName, 4);

        //Administration->Audit Logs
        administration.SetSubItemOrder(AbpAuditLoggingMainMenuNames.GroupName, 5);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 6);
        context.Menu.AddItem(
            new ApplicationMenuItem(
                ProductManagementMenus.Authors,
                l["Menu:Authors"],
                url: "/Authors",
                icon: "fa fa-file-alt",
                requiredPermissionName: ProductManagementPermissions.Authors.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ProductManagementMenus.Books,
                l["Menu:Books"],
                url: "/Books",
                icon: "fa fa-file-alt",
                requiredPermissionName: ProductManagementPermissions.Books.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ProductManagementMenus.Addresses,
                l["Menu:Addresses"],
                url: "/Addresses",
                icon: "fa fa-file-alt",
                requiredPermissionName: ProductManagementPermissions.Addresses.Default)
        );
        return Task.CompletedTask;
    }
}