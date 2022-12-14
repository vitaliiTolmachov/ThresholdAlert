// <auto-generated />
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(DbContext))]
    [Migration("20221001125439_Indexes_To_Threshold_HostActivity_Added")]
    partial class Indexes_To_Threshold_HostActivity_Added
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataAccess.Entities.HostActivity", b =>
                {
                    b.Property<long>("HostActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("HostActivityId"), 1L, 1);

                    b.Property<long>("CallsMade")
                        .HasColumnType("bigint");

                    b.Property<string>("HostName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("Month")
                        .HasColumnType("bigint");

                    b.Property<long>("ThresholdId")
                        .HasColumnType("bigint");

                    b.Property<long>("Year")
                        .HasColumnType("bigint");

                    b.HasKey("HostActivityId");

                    b.HasIndex("HostName");

                    b.HasIndex("Month");

                    b.HasIndex("ThresholdId");

                    b.HasIndex("Year");

                    b.ToTable("HostActivities");

                    b.HasData(
                        new
                        {
                            HostActivityId = 1L,
                            CallsMade = 0L,
                            HostName = "localhost:6054",
                            Month = 9L,
                            ThresholdId = 1L,
                            Year = 2022L
                        },
                        new
                        {
                            HostActivityId = 2L,
                            CallsMade = 0L,
                            HostName = "localhost:6054",
                            Month = 10L,
                            ThresholdId = 1L,
                            Year = 2022L
                        });
                });

            modelBuilder.Entity("DataAccess.Entities.Threshold", b =>
                {
                    b.Property<long>("ThresholdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ThresholdId"), 1L, 1);

                    b.Property<string>("HostName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MaxCalls")
                        .HasColumnType("bigint");

                    b.Property<int>("NotificationLevel")
                        .HasColumnType("int");

                    b.HasKey("ThresholdId");

                    b.ToTable("Thresholds");

                    b.HasData(
                        new
                        {
                            ThresholdId = 1L,
                            HostName = "localhost:6054",
                            MaxCalls = 10L,
                            NotificationLevel = 50
                        });
                });

            modelBuilder.Entity("DataAccess.Entities.HostActivity", b =>
                {
                    b.HasOne("DataAccess.Entities.Threshold", "Threshold")
                        .WithMany("HostActivities")
                        .HasForeignKey("ThresholdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Threshold");
                });

            modelBuilder.Entity("DataAccess.Entities.Threshold", b =>
                {
                    b.Navigation("HostActivities");
                });
#pragma warning restore 612, 618
        }
    }
}
