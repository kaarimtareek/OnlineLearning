using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineLearning.Migrations
{
    public partial class enhance_room_meeting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Link",
                table: "RoomMeetings",
                newName: "MeetingUrl");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "RoomMeetings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeetingDescription",
                table: "RoomMeetings",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeetingName",
                table: "RoomMeetings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeetingPassword",
                table: "RoomMeetings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZoomMeetingId",
                table: "RoomMeetings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeetingDescription",
                table: "RoomMeetings");

            migrationBuilder.DropColumn(
                name: "MeetingName",
                table: "RoomMeetings");

            migrationBuilder.DropColumn(
                name: "MeetingPassword",
                table: "RoomMeetings");

            migrationBuilder.DropColumn(
                name: "ZoomMeetingId",
                table: "RoomMeetings");

            migrationBuilder.RenameColumn(
                name: "MeetingUrl",
                table: "RoomMeetings",
                newName: "Link");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "RoomMeetings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
