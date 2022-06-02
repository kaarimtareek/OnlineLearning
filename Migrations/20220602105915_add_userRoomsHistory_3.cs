using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineLearning.Migrations
{
    public partial class add_userRoomsHistory_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoomsHistories_UsersRooms_UsersRoomId",
                table: "UserRoomsHistories");

            migrationBuilder.RenameColumn(
                name: "UsersRoomId",
                table: "UserRoomsHistories",
                newName: "UsersRoomsId");

            migrationBuilder.RenameColumn(
                name: "UserRoomId",
                table: "UserRoomsHistories",
                newName: "UserRoomsId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoomsHistories_UsersRoomId",
                table: "UserRoomsHistories",
                newName: "IX_UserRoomsHistories_UsersRoomsId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoomsHistories_UsersRooms_UsersRoomsId",
                table: "UserRoomsHistories",
                column: "UsersRoomsId",
                principalTable: "UsersRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoomsHistories_UsersRooms_UsersRoomsId",
                table: "UserRoomsHistories");

            migrationBuilder.RenameColumn(
                name: "UsersRoomsId",
                table: "UserRoomsHistories",
                newName: "UsersRoomId");

            migrationBuilder.RenameColumn(
                name: "UserRoomsId",
                table: "UserRoomsHistories",
                newName: "UserRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoomsHistories_UsersRoomsId",
                table: "UserRoomsHistories",
                newName: "IX_UserRoomsHistories_UsersRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoomsHistories_UsersRooms_UsersRoomId",
                table: "UserRoomsHistories",
                column: "UsersRoomId",
                principalTable: "UsersRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
