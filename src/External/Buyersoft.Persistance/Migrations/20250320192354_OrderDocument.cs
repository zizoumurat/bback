using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buyersoft.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class OrderDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "Orders",
                type: "int",
                nullable: true);

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
                values: new object[] { "7cbab210-10bf-4c42-ac26-45523cb2193e", "AQAAAAIAAYagAAAAEOEQvuJEhvGDzzA8tpyR0tybY4y5LqJ3HUXC/+ePg/Vs/3uLED+/R/fEAhg+UOm2tQ==", "5a35e60d-2f70-4e50-b2ef-6e30f04d9859" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DocumentId",
                table: "Orders",
                column: "DocumentId",
                unique: true,
                filter: "[DocumentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Documents_DocumentId",
                table: "Orders",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Documents_DocumentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DocumentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Orders");

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
    }
}
