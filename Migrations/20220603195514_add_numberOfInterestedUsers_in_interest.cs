using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineLearning.Migrations
{
    public partial class add_numberOfInterestedUsers_in_interest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfInterestedUsers",
                table: "Interests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfInterestedUsers",
                table: "Interests");
        }
    }
}
