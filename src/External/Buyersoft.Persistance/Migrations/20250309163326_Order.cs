using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buyersoft.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderPreparations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    MainCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequestCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCategory = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    RequestGroup = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    AvailableLimit = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPreparations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPreparations_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderPreparations_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderPreparations_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderCode = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OrderPreparationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_OrderPreparations_OrderPreparationId",
                        column: x => x.OrderPreparationId,
                        principalTable: "OrderPreparations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    OfferDetailId = table.Column<int>(type: "int", nullable: false),
                    ProductDefinition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_OfferDetails_OfferDetailId",
                        column: x => x.OfferDetailId,
                        principalTable: "OfferDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5140a57-e0c5-42d4-a935-000e0a7bbc1c", "AQAAAAIAAYagAAAAEEQmSYnSrPV1H7yNc/tz4zVtfb54fk8xUdBrdvnvf6kl7EtNAZm5HZ/sOtZHBZfxSA==", "c619abd5-bb87-43f1-b6be-969cb1d839f4" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OfferDetailId",
                table: "OrderItems",
                column: "OfferDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPreparations_CompanyId",
                table: "OrderPreparations",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPreparations_OfferId",
                table: "OrderPreparations",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPreparations_RequestId",
                table: "OrderPreparations",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderPreparationId",
                table: "Orders",
                column: "OrderPreparationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OrderPreparations");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ba8a58d-fcea-4104-bade-832aa3b6bdf7", "AQAAAAIAAYagAAAAELkqFIPHVsqgeNuRVyJipMAAMghPMNWU/X3k6niI5D3DBVNnakl1ox0riLiNuxLcRg==", "de3bde57-2f41-43ef-86fc-f46a9a71b096" });
        }
    }
}
