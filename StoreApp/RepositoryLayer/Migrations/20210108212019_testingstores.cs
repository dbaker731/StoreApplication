using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class testingstores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_locations_PerferedStoreStoreLocationId",
                table: "customers");

            migrationBuilder.DropForeignKey(
                name: "FK_inventories_locations_StoreLocationId",
                table: "inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_locations_StoreLocationId",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_locations",
                table: "locations");

            migrationBuilder.RenameTable(
                name: "locations",
                newName: "stores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_stores",
                table: "stores",
                column: "StoreLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_stores_PerferedStoreStoreLocationId",
                table: "customers",
                column: "PerferedStoreStoreLocationId",
                principalTable: "stores",
                principalColumn: "StoreLocationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_stores_StoreLocationId",
                table: "inventories",
                column: "StoreLocationId",
                principalTable: "stores",
                principalColumn: "StoreLocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_stores_StoreLocationId",
                table: "orders",
                column: "StoreLocationId",
                principalTable: "stores",
                principalColumn: "StoreLocationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_stores_PerferedStoreStoreLocationId",
                table: "customers");

            migrationBuilder.DropForeignKey(
                name: "FK_inventories_stores_StoreLocationId",
                table: "inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_stores_StoreLocationId",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_stores",
                table: "stores");

            migrationBuilder.RenameTable(
                name: "stores",
                newName: "locations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_locations",
                table: "locations",
                column: "StoreLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_locations_PerferedStoreStoreLocationId",
                table: "customers",
                column: "PerferedStoreStoreLocationId",
                principalTable: "locations",
                principalColumn: "StoreLocationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_locations_StoreLocationId",
                table: "inventories",
                column: "StoreLocationId",
                principalTable: "locations",
                principalColumn: "StoreLocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_locations_StoreLocationId",
                table: "orders",
                column: "StoreLocationId",
                principalTable: "locations",
                principalColumn: "StoreLocationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
