using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WONDERLUST_PROJECT_APIs.Migrations
{
    public partial class Travellers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Travellers",
                columns: table => new
                {
                    TravellersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageBookingId = table.Column<int>(type: "int", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AadharCardNo = table.Column<long>(type: "bigint", nullable: false),
                    ContactNo = table.Column<long>(type: "bigint", nullable: false),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travellers", x => x.TravellersId);
                    table.ForeignKey(
                        name: "FK_Travellers_Package_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Package",
                        principalColumn: "PackageId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Travellers_PackageBooking_PackageBookingId",
                        column: x => x.PackageBookingId,
                        principalTable: "PackageBooking",
                        principalColumn: "PackageBookingId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Travellers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Travellers_PackageBookingId",
                table: "Travellers",
                column: "PackageBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Travellers_PackageId",
                table: "Travellers",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Travellers_UserId",
                table: "Travellers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Travellers");
        }
    }
}
