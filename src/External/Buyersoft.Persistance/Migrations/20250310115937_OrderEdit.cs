using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buyersoft.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class OrderEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyComments",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NonconformityDetail",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NonconformityReason",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NonconformityStatus",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupplierComments",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af613f2d-7d3d-46b9-ba74-2bc2a78fb535", "AQAAAAIAAYagAAAAECb4D3LCzJisq9PZHGYO8/9TNVJ1C4FTGsj8sgGUMH4ZcCqcBmxCKAZk1kIw8GWJgw==", "e57c36b1-4316-4c45-bc6a-252b06a686cb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyComments",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "NonconformityDetail",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "NonconformityReason",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "NonconformityStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SupplierComments",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a936601-1f8e-4c32-83cf-5e03b1755849", "AQAAAAIAAYagAAAAEBe+qk179d05ePu+z2HDlOi4MuK6d0LUadhSQYDcOOZ+0IVhxCWHaHmSMbGBMVjRvg==", "f89ba5d2-ab7b-4a6b-8198-64b657e673f9" });
        }
    }
}
