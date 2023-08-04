using ProductManagement.Carts;
using ProductManagement.TransactionDetails;
using ProductManagement.Transactions;
using ProductManagement.Addresses;
using ProductManagement.Books;
using ProductManagement.Authors;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Editions;
using Volo.Saas.Tenants;
using Volo.Abp.Gdpr;
using Volo.Abp.OpenIddict.EntityFrameworkCore;

namespace ProductManagement.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityProDbContext))]
[ReplaceDbContext(typeof(ISaasDbContext))]
[ConnectionStringName("Default")]
public class ProductManagementDbContext :
    AbpDbContext<ProductManagementDbContext>,
    IIdentityProDbContext,
    ISaasDbContext
{
    public DbSet<Cart> Carts { get; set; }
    public DbSet<TransactionDetail> TransactionDetails { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // SaaS
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Edition> Editions { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public ProductManagementDbContext(DbContextOptions<ProductManagementDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentityPro();
        builder.ConfigureOpenIddictPro();
        builder.ConfigureFeatureManagement();
        builder.ConfigureLanguageManagement();
        builder.ConfigureSaas();
        builder.ConfigureTextTemplateManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureGdpr();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(ProductManagementConsts.DbTablePrefix + "YourEntities", ProductManagementConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Author>(b =>
{
    b.ToTable(ProductManagementConsts.DbTablePrefix + "Authors", ProductManagementConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.FirstName).HasColumnName(nameof(Author.FirstName)).IsRequired();
    b.Property(x => x.LastName).HasColumnName(nameof(Author.LastName));
    b.Property(x => x.Email).HasColumnName(nameof(Author.Email)).IsRequired();
    b.Property(x => x.Bio).HasColumnName(nameof(Author.Bio));
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Book>(b =>
{
    b.ToTable(ProductManagementConsts.DbTablePrefix + "Books", ProductManagementConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Title).HasColumnName(nameof(Book.Title)).IsRequired();
    b.Property(x => x.Price).HasColumnName(nameof(Book.Price)).IsRequired();
    b.Property(x => x.Description).HasColumnName(nameof(Book.Description));
    b.HasOne<Author>().WithMany().IsRequired().HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Transaction>(b =>
{
    b.ToTable(ProductManagementConsts.DbTablePrefix + "Transactions", ProductManagementConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.UserId).HasColumnName(nameof(Transaction.UserId));
    b.Property(x => x.TransactionDate).HasColumnName(nameof(Transaction.TransactionDate));
    b.Property(x => x.TotalAmount).HasColumnName(nameof(Transaction.TotalAmount));
    b.Property(x => x.PaymentStatus).HasColumnName(nameof(Transaction.PaymentStatus)).IsRequired();
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<TransactionDetail>(b =>
{
    b.ToTable(ProductManagementConsts.DbTablePrefix + "TransactionDetails", ProductManagementConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Quantity).HasColumnName(nameof(TransactionDetail.Quantity));
    b.Property(x => x.SubTotal).HasColumnName(nameof(TransactionDetail.SubTotal));
    b.HasOne<Transaction>().WithMany().HasForeignKey(x => x.TransactionId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Book>().WithMany().HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Cart>(b =>
{
    b.ToTable(ProductManagementConsts.DbTablePrefix + "Carts", ProductManagementConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.UserId).HasColumnName(nameof(Cart.UserId));
    b.Property(x => x.Quantity).HasColumnName(nameof(Cart.Quantity));
    b.Property(x => x.DateAdded).HasColumnName(nameof(Cart.DateAdded));
    b.HasOne<Book>().WithMany().HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Address>(b =>
{
    b.ToTable(ProductManagementConsts.DbTablePrefix + "Addresses", ProductManagementConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.CIty).HasColumnName(nameof(Address.CIty)).IsRequired();
    b.Property(x => x.State).HasColumnName(nameof(Address.State)).IsRequired();
    b.Property(x => x.PostalCode).HasColumnName(nameof(Address.PostalCode)).IsRequired();
    b.Property(x => x.Country).HasColumnName(nameof(Address.Country));
    b.Property(x => x.UserId).HasColumnName(nameof(Address.UserId));
    b.Property(x => x.StreetAddress).HasColumnName(nameof(Address.StreetAddress)).IsRequired();
});

        }
    }
}