using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WONDERLUST_PROJECT_APIs.Migrations
{
    public partial class Payment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageBookingId = table.Column<int>(type: "int", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    PayemtMode = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymetId);
                    table.ForeignKey(
                        name: "FK_Payment_Package_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Package",
                        principalColumn: "PackageId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Payment_PackageBooking_PackageBookingId",
                        column: x => x.PackageBookingId,
                        principalTable: "PackageBooking",
                        principalColumn: "PackageBookingId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Payment_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PackageBookingId",
                table: "Payment",
                column: "PackageBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PackageId",
                table: "Payment",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserId",
                table: "Payment",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");
        }
    }
}
