using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineLearning.Migrations
{
    public partial class add_room_status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_LookupRoomStatus_StatusId",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LookupRoomStatus",
                table: "LookupRoomStatus");

            migrationBuilder.RenameTable(
                name: "LookupRoomStatus",
                newName: "LookupRoomStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LookupRoomStatuses",
                table: "LookupRoomStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_LookupRoomStatuses_StatusId",
                table: "Rooms",
                column: "StatusId",
                principalTable: "LookupRoomStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_LookupRoomStatuses_StatusId",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LookupRoomStatuses",
                table: "LookupRoomStatuses");

            migrationBuilder.RenameTable(
                name: "LookupRoomStatuses",
                newName: "LookupRoomStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LookupRoomStatus",
                table: "LookupRoomStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_LookupRoomStatus_StatusId",
                table: "Rooms",
                column: "StatusId",
                principalTable: "LookupRoomStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
