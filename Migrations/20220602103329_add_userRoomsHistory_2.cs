using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineLearning.Migrations
{
    public partial class add_userRoomsHistory_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoomsHistory_AspNetUsers_UserId",
                table: "UserRoomsHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoomsHistory_UsersRooms_UsersRoomId",
                table: "UserRoomsHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoomsHistory",
                table: "UserRoomsHistory");

            migrationBuilder.RenameTable(
                name: "UserRoomsHistory",
                newName: "UserRoomsHistories");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoomsHistory_UsersRoomId",
                table: "UserRoomsHistories",
                newName: "IX_UserRoomsHistories_UsersRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoomsHistory_UserId",
                table: "UserRoomsHistories",
                newName: "IX_UserRoomsHistories_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoomsHistories",
                table: "UserRoomsHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoomsHistories_AspNetUsers_UserId",
                table: "UserRoomsHistories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoomsHistories_UsersRooms_UsersRoomId",
                table: "UserRoomsHistories",
                column: "UsersRoomId",
                principalTable: "UsersRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoomsHistories_AspNetUsers_UserId",
                table: "UserRoomsHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoomsHistories_UsersRooms_UsersRoomId",
                table: "UserRoomsHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoomsHistories",
                table: "UserRoomsHistories");

            migrationBuilder.RenameTable(
                name: "UserRoomsHistories",
                newName: "UserRoomsHistory");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoomsHistories_UsersRoomId",
                table: "UserRoomsHistory",
                newName: "IX_UserRoomsHistory_UsersRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoomsHistories_UserId",
                table: "UserRoomsHistory",
                newName: "IX_UserRoomsHistory_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoomsHistory",
                table: "UserRoomsHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoomsHistory_AspNetUsers_UserId",
                table: "UserRoomsHistory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoomsHistory_UsersRooms_UsersRoomId",
                table: "UserRoomsHistory",
                column: "UsersRoomId",
                principalTable: "UsersRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
