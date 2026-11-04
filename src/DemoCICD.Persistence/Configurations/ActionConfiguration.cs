using DemoCICD.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Action = DemoCICD.Domain.Entities.Identity.Action;

namespace DemoCICD.Persistence.Configurations
{
    public class ActionConfiguration : IEntityTypeConfiguration<Action>
    {
        public void Configure(EntityTypeBuilder<Action> builder)
        {
            builder.ToTable(TableNames.Actions);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.SortOrder).HasDefaultValue(null);

            //each user can have many Permissions
            builder.HasMany(x => x.Permissions)
                .WithOne()
                .HasForeignKey(per => per.ActionId)
                .IsRequired();

            //each user can have many ActionInFunctions
            builder.HasMany(x => x.ActionInFunctions)
                .WithOne()
                .HasForeignKey(x => x.ActionId)
                .IsRequired();
        }
    }
}
