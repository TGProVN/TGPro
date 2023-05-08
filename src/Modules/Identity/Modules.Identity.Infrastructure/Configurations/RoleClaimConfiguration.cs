using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Identity.Core.Entities;

namespace Modules.Identity.Infrastructure.Configurations;

public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable("RoleClaims");
        
        builder.HasOne(x => x.Role)
               .WithMany(y => y.RoleClaims)
               .HasForeignKey(x => x.RoleId)
               .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(x => x.Group).IsRequired().HasColumnType("nvarchar(20)");
        builder.Property(x => x.Description).IsRequired(false).HasColumnType("nvarchar(255)");
        builder.Property(x => x.ClaimType).IsRequired().HasColumnType("nvarchar(50)");
        builder.Property(x => x.ClaimValue).IsRequired(false).HasColumnType("nvarchar(100)");
        builder.Property(x => x.CreatedBy).IsRequired().HasColumnType("nvarchar(128)");
        builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValue(DateTimeOffset.Now);
        builder.Property(x => x.LastModifiedBy).IsRequired(false).HasColumnType("nvarchar(128)");
        builder.Property(x => x.LastModifiedAt).IsRequired(false);
    }
}
