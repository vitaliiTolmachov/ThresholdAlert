using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Column_HostName_Removed_tbl_HostActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HostActivities_HostName",
                table: "HostActivities");

            migrationBuilder.DropColumn(
                name: "HostName",
                table: "HostActivities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HostName",
                table: "HostActivities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "HostActivities",
                keyColumn: "HostActivityId",
                keyValue: 1L,
                column: "HostName",
                value: "localhost:6054");

            migrationBuilder.UpdateData(
                table: "HostActivities",
                keyColumn: "HostActivityId",
                keyValue: 2L,
                column: "HostName",
                value: "localhost:6054");

            migrationBuilder.CreateIndex(
                name: "IX_HostActivities_HostName",
                table: "HostActivities",
                column: "HostName");
        }
    }
}
