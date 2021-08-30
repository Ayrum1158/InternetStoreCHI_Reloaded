using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedForeighKeyConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartitems_Carts_CartEntityId",
                table: "Cartitems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderedProducts_Orders_OrderEntityId",
                table: "OrderedProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderedProducts_OrderEntityId",
                table: "OrderedProducts");

            migrationBuilder.DropIndex(
                name: "IX_Cartitems_CartEntityId",
                table: "Cartitems");

            migrationBuilder.DropColumn(
                name: "OrderEntityId",
                table: "OrderedProducts");

            migrationBuilder.DropColumn(
                name: "CartEntityId",
                table: "Cartitems");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderedProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Cartitems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderedProducts_OrderId",
                table: "OrderedProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Cartitems_CartId",
                table: "Cartitems",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cartitems_Carts_CartId",
                table: "Cartitems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedProducts_Orders_OrderId",
                table: "OrderedProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartitems_Carts_CartId",
                table: "Cartitems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderedProducts_Orders_OrderId",
                table: "OrderedProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderedProducts_OrderId",
                table: "OrderedProducts");

            migrationBuilder.DropIndex(
                name: "IX_Cartitems_CartId",
                table: "Cartitems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderedProducts");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Cartitems");

            migrationBuilder.AddColumn<int>(
                name: "OrderEntityId",
                table: "OrderedProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CartEntityId",
                table: "Cartitems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderedProducts_OrderEntityId",
                table: "OrderedProducts",
                column: "OrderEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cartitems_CartEntityId",
                table: "Cartitems",
                column: "CartEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cartitems_Carts_CartEntityId",
                table: "Cartitems",
                column: "CartEntityId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedProducts_Orders_OrderEntityId",
                table: "OrderedProducts",
                column: "OrderEntityId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
