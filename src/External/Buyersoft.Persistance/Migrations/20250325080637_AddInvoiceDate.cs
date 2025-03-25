using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buyersoft.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f772f511-c6f6-458c-8101-f94e186d0971", "AQAAAAIAAYagAAAAEF/A5hYqCqE0/kfpFbOiqVTnkxeyP56R5110sak18eNOhDcX1Aqys3wLONBoqg0V1A==", "5d8d29c5-3a04-4016-b65c-d8b45e6048a7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f6b8f18-b5dd-4d90-9cce-a02f709d1610", "AQAAAAIAAYagAAAAENd7iKE/7x+6YwR95/LJlTVvwVKVyHmWRHHsHPD8jxuxbPFr4M5jSkxoENPvqtTMbA==", "01fd1d40-3343-4edb-918c-af5edf0fed3d" });
        }
    }
}
