using core.Models.AppSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Principal;

namespace multi_tenant.Models.Models;

/// <summary>
/// MultiTenantContext
/// </summary>
public partial class MultiTenantContext : DbContext
{
    private readonly string _connectionString;
    /// <summary>
    /// ctor
    /// </summary>
    public MultiTenantContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="options"></param>
    /// <param name="connectionStringConfiguration"></param>
    public MultiTenantContext(DbContextOptions<MultiTenantContext> options,
        IOptions<ConnectionStringSettings> connectionStringConfiguration)
        : base(options)
    {
        _connectionString = connectionStringConfiguration.Value.PostgresConnectionString;
    }

    /// <summary>
    /// DbSet Accounts
    /// </summary>
    public virtual DbSet<Account> Accounts { get; set; } = null!;

    /// <summary>
    /// DbSet Tenants
    /// </summary>
    public virtual DbSet<Tenant> Tenants { get; set; } = null!;

    /// <summary>
    /// On COnfiguring
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(_connectionString,
                npgsqlOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null
                        );
                });
        }
    }

    /// <summary>
    /// On Model Creating
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_account_id");

            entity.ToTable("account");

            entity.HasIndex(e => e.Email, "account_email_idx");

            entity.HasIndex(e => e.TenantId, "account_tenant_id_idx");

            entity.HasIndex(e => e.Username, "username");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(200)
                .HasColumnName("full_name");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .HasColumnName("password");
            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.Username)
                .HasMaxLength(200)
                .HasColumnName("username");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tenant_tenant_id");
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_tenant_id");

            entity.ToTable("tenant");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
