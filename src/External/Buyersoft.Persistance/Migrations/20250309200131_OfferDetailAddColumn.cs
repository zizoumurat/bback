using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buyersoft.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class OfferDetailAddColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductDefinition",
                table: "OfferDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a936601-1f8e-4c32-83cf-5e03b1755849", "AQAAAAIAAYagAAAAEBe+qk179d05ePu+z2HDlOi4MuK6d0LUadhSQYDcOOZ+0IVhxCWHaHmSMbGBMVjRvg==", "f89ba5d2-ab7b-4a6b-8198-64b657e673f9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductDefinition",
                table: "OfferDetails");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5140a57-e0c5-42d4-a935-000e0a7bbc1c", "AQAAAAIAAYagAAAAEEQmSYnSrPV1H7yNc/tz4zVtfb54fk8xUdBrdvnvf6kl7EtNAZm5HZ/sOtZHBZfxSA==", "c619abd5-bb87-43f1-b6be-969cb1d839f4" });
        }
    }
}
