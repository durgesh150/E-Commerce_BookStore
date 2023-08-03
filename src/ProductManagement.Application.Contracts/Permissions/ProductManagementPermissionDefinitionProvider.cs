using ProductManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace ProductManagement.Permissions;

public class ProductManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ProductManagementPermissions.GroupName);

        myGroup.AddPermission(ProductManagementPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(ProductManagementPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(ProductManagementPermissions.MyPermission1, L("Permission:MyPermission1"));

        var authorPermission = myGroup.AddPermission(ProductManagementPermissions.Authors.Default, L("Permission:Authors"));
        authorPermission.AddChild(ProductManagementPermissions.Authors.Create, L("Permission:Create"));
        authorPermission.AddChild(ProductManagementPermissions.Authors.Edit, L("Permission:Edit"));
        authorPermission.AddChild(ProductManagementPermissions.Authors.Delete, L("Permission:Delete"));

        var bookPermission = myGroup.AddPermission(ProductManagementPermissions.Books.Default, L("Permission:Books"));
        bookPermission.AddChild(ProductManagementPermissions.Books.Create, L("Permission:Create"));
        bookPermission.AddChild(ProductManagementPermissions.Books.Edit, L("Permission:Edit"));
        bookPermission.AddChild(ProductManagementPermissions.Books.Delete, L("Permission:Delete"));

        var addressPermission = myGroup.AddPermission(ProductManagementPermissions.Addresses.Default, L("Permission:Addresses"));
        addressPermission.AddChild(ProductManagementPermissions.Addresses.Create, L("Permission:Create"));
        addressPermission.AddChild(ProductManagementPermissions.Addresses.Edit, L("Permission:Edit"));
        addressPermission.AddChild(ProductManagementPermissions.Addresses.Delete, L("Permission:Delete"));

        var transactionPermission = myGroup.AddPermission(ProductManagementPermissions.Transactions.Default, L("Permission:Transactions"));
        transactionPermission.AddChild(ProductManagementPermissions.Transactions.Create, L("Permission:Create"));
        transactionPermission.AddChild(ProductManagementPermissions.Transactions.Edit, L("Permission:Edit"));
        transactionPermission.AddChild(ProductManagementPermissions.Transactions.Delete, L("Permission:Delete"));

        var transactionDetailPermission = myGroup.AddPermission(ProductManagementPermissions.TransactionDetails.Default, L("Permission:TransactionDetails"));
        transactionDetailPermission.AddChild(ProductManagementPermissions.TransactionDetails.Create, L("Permission:Create"));
        transactionDetailPermission.AddChild(ProductManagementPermissions.TransactionDetails.Edit, L("Permission:Edit"));
        transactionDetailPermission.AddChild(ProductManagementPermissions.TransactionDetails.Delete, L("Permission:Delete"));

        var cartPermission = myGroup.AddPermission(ProductManagementPermissions.Carts.Default, L("Permission:Carts"));
        cartPermission.AddChild(ProductManagementPermissions.Carts.Create, L("Permission:Create"));
        cartPermission.AddChild(ProductManagementPermissions.Carts.Edit, L("Permission:Edit"));
        cartPermission.AddChild(ProductManagementPermissions.Carts.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ProductManagementResource>(name);
    }
}