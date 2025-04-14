using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buyersoft.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class PaymentList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentListId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    PaymentListCode = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentLists_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentListApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentListId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentListApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentListApprovals_PaymentLists_PaymentListId",
                        column: x => x.PaymentListId,
                        principalTable: "PaymentLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentListApprovals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9902a465-bd00-45ef-9135-639843af4a73", "AQAAAAIAAYagAAAAEH3XuuOw/sAA/n1LB2m4qTF2cStFzkmoOXQ+B9t4Bhfd40OrhMvy10bZ/N7LqV+SZw==", "d61f689f-e882-4cd0-83db-ecfeb266706a" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentListId",
                table: "Orders",
                column: "PaymentListId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentListApprovals_PaymentListId",
                table: "PaymentListApprovals",
                column: "PaymentListId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentListApprovals_UserId",
                table: "PaymentListApprovals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentLists_CompanyId",
                table: "PaymentLists",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentLists_PaymentListId",
                table: "Orders",
                column: "PaymentListId",
                principalTable: "PaymentLists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentLists_PaymentListId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "PaymentListApprovals");

            migrationBuilder.DropTable(
                name: "PaymentLists");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentListId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentListId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f772f511-c6f6-458c-8101-f94e186d0971", "AQAAAAIAAYagAAAAEF/A5hYqCqE0/kfpFbOiqVTnkxeyP56R5110sak18eNOhDcX1Aqys3wLONBoqg0V1A==", "5d8d29c5-3a04-4016-b65c-d8b45e6048a7" });
        }
    }
}
