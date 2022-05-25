using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineLearning.Migrations
{
    public partial class addNumberOfUsersInRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfJoinedUsers",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfLeftUsers",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRejectedUsers",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRequestedUsers",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfJoinedUsers",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "NumberOfLeftUsers",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "NumberOfRejectedUsers",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "NumberOfRequestedUsers",
                table: "Rooms");
        }
    }
}
