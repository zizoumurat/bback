using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buyersoft.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractApproval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractApproval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractApproval_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractApproval_Users_UserId",
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
                values: new object[] { "4d13e7fb-6ace-4ac2-98a8-95f2af29ba9f", "AQAAAAIAAYagAAAAEFPZE5+338IVpwYNfHWB1fYfwwe0YBXayHU7N7fiD/1IM9i8bh9qvP/6AimPZrfYEQ==", "c83fa7cf-a9d3-4f7d-aeff-33ed6432f560" });

            migrationBuilder.CreateIndex(
                name: "IX_ContractApproval_ContractId",
                table: "ContractApproval",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractApproval_UserId",
                table: "ContractApproval",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractApproval");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "75749f8d-2b7d-412b-9e21-e917053332ca", "AQAAAAIAAYagAAAAEG4TF+MyLLVc/ec4+HwwWOvqMkiost+f/5cc54PU+wG2/n69wdxTXs5Cflb30RIxxQ==", "0ab17802-b01b-4eb9-934e-e0143b08eccc" });
        }
    }
}
