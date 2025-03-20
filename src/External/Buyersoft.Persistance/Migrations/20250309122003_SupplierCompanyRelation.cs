using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buyersoft.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class SupplierCompanyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Suppliers_CompanyId",
                table: "Suppliers");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ba8a58d-fcea-4104-bade-832aa3b6bdf7", "AQAAAAIAAYagAAAAELkqFIPHVsqgeNuRVyJipMAAMghPMNWU/X3k6niI5D3DBVNnakl1ox0riLiNuxLcRg==", "de3bde57-2f41-43ef-86fc-f46a9a71b096" });

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CompanyId",
                table: "Suppliers",
                column: "CompanyId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Suppliers_CompanyId",
                table: "Suppliers");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "098d6358-d63b-4bc3-8c21-c41ed9815b4f", "AQAAAAIAAYagAAAAEB0VQB3ED3uvsJxBbSHjyieOMQYcoQfHwuyM7z1+sgz9AvsvfJwayQGf/7JCOuBwlg==", "791876e5-4a9f-466f-bfdb-f210ca9f4365" });

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CompanyId",
                table: "Suppliers",
                column: "CompanyId");
        }
    }
}
