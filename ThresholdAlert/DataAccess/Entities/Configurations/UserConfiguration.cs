using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Entities.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired();

            builder.Property(x => x.LastName)
            .IsRequired();

            builder.Property(x => x.Password)
            .IsRequired();

            builder.HasIndex(x => x.Username);
            builder.HasIndex(x => x.Password);

            builder.HasMany(u => u.Thresholds)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserIdId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
