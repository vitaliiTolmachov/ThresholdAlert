using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Indexes_To_Threshold_HostActivity_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HostName",
                table: "HostActivities",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_HostActivities_HostName",
                table: "HostActivities",
                column: "HostName");

            migrationBuilder.CreateIndex(
                name: "IX_HostActivities_Month",
                table: "HostActivities",
                column: "Month");

            migrationBuilder.CreateIndex(
                name: "IX_HostActivities_Year",
                table: "HostActivities",
                column: "Year");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HostActivities_HostName",
                table: "HostActivities");

            migrationBuilder.DropIndex(
                name: "IX_HostActivities_Month",
                table: "HostActivities");

            migrationBuilder.DropIndex(
                name: "IX_HostActivities_Year",
                table: "HostActivities");

            migrationBuilder.AlterColumn<string>(
                name: "HostName",
                table: "HostActivities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
