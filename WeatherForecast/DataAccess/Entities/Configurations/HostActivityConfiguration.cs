using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Entities.Configurations
{
    public class HostActivityConfiguration : IEntityTypeConfiguration<HostActivity>
    {
        public void Configure(EntityTypeBuilder<HostActivity> builder)
        {
            builder.HasKey(x => x.HostActivityId);

            builder.Property(x => x.HostName)
                .IsRequired();

            builder.Property(x => x.Month)
                .IsRequired();

            builder.Property(x => x.Year)
                .IsRequired();

            builder.Property(x => x.CallsMade)
                .IsRequired();


            builder.HasIndex(x => x.HostName);
            builder.HasIndex(x => x.Month);
            builder.HasIndex(x => x.Year);
            builder.HasIndex(x => x.UserId);

            builder.HasOne(x => x.Threshold)
                .WithMany(x => x.HostActivities);
        }
    }


}
