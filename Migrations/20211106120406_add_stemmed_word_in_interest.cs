using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineLearning.Migrations
{
    public partial class add_stemmed_word_in_interest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameArabic",
                table: "Interests");

            migrationBuilder.RenameColumn(
                name: "NameEnglish",
                table: "Interests",
                newName: "StemmedName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StemmedName",
                table: "Interests",
                newName: "NameEnglish");

            migrationBuilder.AddColumn<string>(
                name: "NameArabic",
                table: "Interests",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
