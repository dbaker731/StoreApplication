using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class NewInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    StoreLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreLocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoreLocationAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.StoreLocationId);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ProductDesc = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAgeRestricted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerUserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustomerPassword = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustomerFName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustomerLName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustomerAge = table.Column<int>(type: "int", nullable: false),
                    CustomerBirthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isAdmin = table.Column<bool>(type: "bit", nullable: true),
                    PerferedStoreStoreLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_customers_locations_PerferedStoreStoreLocationId",
                        column: x => x.PerferedStoreStoreLocationId,
                        principalTable: "locations",
                        principalColumn: "StoreLocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "inventories",
                columns: table => new
                {
                    InventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StoreLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventories", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_inventories_locations_StoreLocationId",
                        column: x => x.StoreLocationId,
                        principalTable: "locations",
                        principalColumn: "StoreLocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventories_products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StoreLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    isOrdered = table.Column<bool>(type: "bit", nullable: false),
                    isCart = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_orders_customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_locations_StoreLocationId",
                        column: x => x.StoreLocationId,
                        principalTable: "locations",
                        principalColumn: "StoreLocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orderLineDetails",
                columns: table => new
                {
                    OrderDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemInventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderDetailsQuantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderDetailsPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderLineDetails", x => x.OrderDetailsId);
                    table.ForeignKey(
                        name: "FK_orderLineDetails_inventories_ItemInventoryId",
                        column: x => x.ItemInventoryId,
                        principalTable: "inventories",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orderLineDetails_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customers_PerferedStoreStoreLocationId",
                table: "customers",
                column: "PerferedStoreStoreLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_ProductID",
                table: "inventories",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_StoreLocationId",
                table: "inventories",
                column: "StoreLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_orderLineDetails_ItemInventoryId",
                table: "orderLineDetails",
                column: "ItemInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_orderLineDetails_OrderId",
                table: "orderLineDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_CustomerID",
                table: "orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_orders_StoreLocationId",
                table: "orders",
                column: "StoreLocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderLineDetails");

            migrationBuilder.DropTable(
                name: "inventories");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "locations");
        }
    }
}
