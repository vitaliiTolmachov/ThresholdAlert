using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Entities.Configurations
{
    public class ThresholdConfiguration : IEntityTypeConfiguration<Threshold>
    {
        public void Configure(EntityTypeBuilder<Threshold> builder)
        {
            builder.HasKey(x => x.ThresholdId);

            builder.Property(x => x.HostName)
                .IsRequired();

            builder.Property(x => x.MaxCalls)
            .IsRequired();

            builder.HasMany(t => t.HostActivities)
                .WithOne(h => h.Threshold)
                .HasForeignKey(h => h.ThresholdId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
