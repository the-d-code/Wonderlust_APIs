using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WONDERLUST_PROJECT_APIs.Migrations
{
    public partial class uploada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryImage",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryImage",
                table: "Category");
        }
    }
}
