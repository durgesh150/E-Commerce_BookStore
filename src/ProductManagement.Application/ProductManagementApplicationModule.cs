using Volo.Abp.Account;
using Volo.Abp.AuditLogging;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Gdpr;
using Volo.Abp.Identity;
using Volo.Abp.LanguageManagement;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplateManagement;
using Volo.Saas.Host;

namespace ProductManagement;

[DependsOn(
    typeof(ProductManagementDomainModule),
    typeof(ProductManagementApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(SaasHostApplicationModule),
    typeof(AbpAuditLoggingApplicationModule),
    typeof(AbpOpenIddictProApplicationModule),
    typeof(AbpAccountPublicApplicationModule),
    typeof(AbpAccountAdminApplicationModule),
    typeof(LanguageManagementApplicationModule),
    typeof(AbpGdprApplicationModule),
    typeof(TextTemplateManagementApplicationModule)
    )]
[DependsOn(typeof(AbpAccountSharedApplicationModule))]
    public class ProductManagementApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ProductManagementApplicationModule>();
        });
    }
}
