using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buyersoft.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WaybillNumber",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

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
                values: new object[] { "b8cedaac-ff7c-4a45-9ca4-553ddaf5932f", "AQAAAAIAAYagAAAAELsQiDBZVUvaamoCWL4n8s0J9hGXWm+LdcSalbG/QL86sR3zs5BnoRnPKC/4jstvag==", "b5978b82-158f-49b3-87a1-f680b7b041b5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WaybillNumber",
                table: "Orders");

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
                values: new object[] { "af613f2d-7d3d-46b9-ba74-2bc2a78fb535", "AQAAAAIAAYagAAAAECb4D3LCzJisq9PZHGYO8/9TNVJ1C4FTGsj8sgGUMH4ZcCqcBmxCKAZk1kIw8GWJgw==", "e57c36b1-4316-4c45-bc6a-252b06a686cb" });
        }
    }
}
