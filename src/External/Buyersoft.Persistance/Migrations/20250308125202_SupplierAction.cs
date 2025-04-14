using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buyersoft.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class SupplierAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupplierActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SupplierNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupplierActionStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierActions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierActions_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierActions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Ýstanbul Location");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "098d6358-d63b-4bc3-8c21-c41ed9815b4f", "AQAAAAIAAYagAAAAEB0VQB3ED3uvsJxBbSHjyieOMQYcoQfHwuyM7z1+sgz9AvsvfJwayQGf/7JCOuBwlg==", "791876e5-4a9f-466f-bfdb-f210ca9f4365" });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierActions_CompanyId",
                table: "SupplierActions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierActions_SupplierId",
                table: "SupplierActions",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierActions_UserId",
                table: "SupplierActions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierActions");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "İstanbul Location");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d13e7fb-6ace-4ac2-98a8-95f2af29ba9f", "AQAAAAIAAYagAAAAEFPZE5+338IVpwYNfHWB1fYfwwe0YBXayHU7N7fiD/1IM9i8bh9qvP/6AimPZrfYEQ==", "c83fa7cf-a9d3-4f7d-aeff-33ed6432f560" });
        }
    }
}
