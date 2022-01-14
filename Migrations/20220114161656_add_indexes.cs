using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineLearning.Migrations
{
    public partial class add_indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersRooms_UserId",
                table: "UsersRooms");

            migrationBuilder.DropIndex(
                name: "IX_UserInterests_UserId",
                table: "UserInterests");

            migrationBuilder.DropIndex(
                name: "IX_RoomInterests_RoomId",
                table: "RoomInterests");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_UsersRooms_UserId_RoomId",
                table: "UsersRooms",
                columns: new[] { "UserId", "RoomId" },
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserInterests_UserId_InterestId",
                table: "UserInterests",
                columns: new[] { "UserId", "InterestId" },
                unique: true,
                filter: "[UserId] IS NOT NULL AND [InterestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoomInterests_RoomId_InterestId",
                table: "RoomInterests",
                columns: new[] { "RoomId", "InterestId" },
                unique: true,
                filter: "[InterestId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersRooms_UserId_RoomId",
                table: "UsersRooms");

            migrationBuilder.DropIndex(
                name: "IX_UserInterests_UserId_InterestId",
                table: "UserInterests");

            migrationBuilder.DropIndex(
                name: "IX_RoomInterests_RoomId_InterestId",
                table: "RoomInterests");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Rooms");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRooms_UserId",
                table: "UsersRooms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInterests_UserId",
                table: "UserInterests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomInterests_RoomId",
                table: "RoomInterests",
                column: "RoomId");
        }
    }
}
