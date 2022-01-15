using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineLearning.Migrations
{
    public partial class add_status_reasons_userrooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LeaveReason",
                table: "UsersRooms",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SuspensionReason",
                table: "UsersRooms",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveReason",
                table: "UsersRooms");

            migrationBuilder.DropColumn(
                name: "SuspensionReason",
                table: "UsersRooms");
        }
    }
}
