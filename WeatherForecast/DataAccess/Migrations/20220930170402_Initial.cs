using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Thresholds",
                columns: table => new
                {
                    ThresholdId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxCalls = table.Column<long>(type: "bigint", nullable: false),
                    NotificationLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thresholds", x => x.ThresholdId);
                });

            migrationBuilder.CreateTable(
                name: "HostActivities",
                columns: table => new
                {
                    HostActivityId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<long>(type: "bigint", nullable: false),
                    Year = table.Column<long>(type: "bigint", nullable: false),
                    HostName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CallsMade = table.Column<long>(type: "bigint", nullable: false),
                    ThresholdId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostActivities", x => x.HostActivityId);
                    table.ForeignKey(
                        name: "FK_HostActivities_Thresholds_ThresholdId",
                        column: x => x.ThresholdId,
                        principalTable: "Thresholds",
                        principalColumn: "ThresholdId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Thresholds",
                columns: new[] { "ThresholdId", "HostName", "MaxCalls", "NotificationLevel" },
                values: new object[] { 1L, "http://localhost:6054", 10L, 50 });

            migrationBuilder.InsertData(
                table: "HostActivities",
                columns: new[] { "HostActivityId", "CallsMade", "HostName", "Month", "ThresholdId", "Year" },
                values: new object[] { 1L, 0L, "http://localhost:6054", 9L, 1L, 2022L });

            migrationBuilder.InsertData(
                table: "HostActivities",
                columns: new[] { "HostActivityId", "CallsMade", "HostName", "Month", "ThresholdId", "Year" },
                values: new object[] { 2L, 0L, "http://localhost:6054", 10L, 1L, 2022L });

            migrationBuilder.CreateIndex(
                name: "IX_HostActivities_ThresholdId",
                table: "HostActivities",
                column: "ThresholdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HostActivities");

            migrationBuilder.DropTable(
                name: "Thresholds");
        }
    }
}
