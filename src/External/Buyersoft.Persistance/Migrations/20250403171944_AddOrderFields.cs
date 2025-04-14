using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buyersoft.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DesiredShippingDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Incoterms",
                table: "Orders",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "Orders",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

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
                values: new object[] { "9ccbcf99-382b-4416-8cab-c79200d34a83", "AQAAAAIAAYagAAAAEOXLHwCFZovp3ap+VHT21Wf0jVSceeNnnN4gleGgPhoKwcL9wpBNgBjHaIPcQ2YoKQ==", "d1f3a470-3d34-48fc-b874-0a3f208071af" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DesiredShippingDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Incoterms",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
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
                values: new object[] { "9902a465-bd00-45ef-9135-639843af4a73", "AQAAAAIAAYagAAAAEH3XuuOw/sAA/n1LB2m4qTF2cStFzkmoOXQ+B9t4Bhfd40OrhMvy10bZ/N7LqV+SZw==", "d61f689f-e882-4cd0-83db-ecfeb266706a" });
        }
    }
}
