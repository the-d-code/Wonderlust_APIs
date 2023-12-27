using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WONDERLUST_PROJECT_APIs.Migrations
{
    public partial class upload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryImage",
                table: "Category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryImage",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
