using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineLearning.Migrations
{
    public partial class increase_zoom_meeting_id_length : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ZoomMeetingId",
                table: "RoomMeetings",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ZoomMeetingId",
                table: "RoomMeetings",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
