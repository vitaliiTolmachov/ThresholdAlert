using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Users_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAlertSent",
                table: "Thresholds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "UserIdId",
                table: "Thresholds",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "HostActivities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "HostActivities",
                keyColumn: "HostActivityId",
                keyValue: 1L,
                column: "UserId",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "HostActivities",
                keyColumn: "HostActivityId",
                keyValue: 2L,
                column: "UserId",
                value: 1L);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { 1L, "Admin", "Admin", "test", "test" });

            migrationBuilder.UpdateData(
                table: "Thresholds",
                keyColumn: "ThresholdId",
                keyValue: 1L,
                column: "UserIdId",
                value: 1L);

            migrationBuilder.CreateIndex(
                name: "IX_Thresholds_UserIdId",
                table: "Thresholds",
                column: "UserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_HostActivities_UserId",
                table: "HostActivities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Password",
                table: "Users",
                column: "Password");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Thresholds_Users_UserIdId",
                table: "Thresholds",
                column: "UserIdId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thresholds_Users_UserIdId",
                table: "Thresholds");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Thresholds_UserIdId",
                table: "Thresholds");

            migrationBuilder.DropIndex(
                name: "IX_HostActivities_UserId",
                table: "HostActivities");

            migrationBuilder.DropColumn(
                name: "IsAlertSent",
                table: "Thresholds");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "Thresholds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HostActivities");
        }
    }
}
