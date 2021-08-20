using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ChangedSeveralManytomanyToManytooneRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartEntityCartItemEntity");

            migrationBuilder.DropTable(
                name: "OrderEntityOrderedProductEntity");

            migrationBuilder.DropTable(
                name: "OrderEntityUserEntity");

            migrationBuilder.AddColumn<int>(
                name: "UserEntityId",
                table: "Orders",
                type: "int",
                nullable: true);

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
                name: "IX_Orders_UserEntityId",
                table: "Orders",
                column: "UserEntityId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserEntityId",
                table: "Orders",
                column: "UserEntityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartitems_Carts_CartEntityId",
                table: "Cartitems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderedProducts_Orders_OrderEntityId",
                table: "OrderedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserEntityId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserEntityId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderedProducts_OrderEntityId",
                table: "OrderedProducts");

            migrationBuilder.DropIndex(
                name: "IX_Cartitems_CartEntityId",
                table: "Cartitems");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderEntityId",
                table: "OrderedProducts");

            migrationBuilder.DropColumn(
                name: "CartEntityId",
                table: "Cartitems");

            migrationBuilder.CreateTable(
                name: "CartEntityCartItemEntity",
                columns: table => new
                {
                    CartItemsId = table.Column<int>(type: "int", nullable: false),
                    CartsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartEntityCartItemEntity", x => new { x.CartItemsId, x.CartsId });
                    table.ForeignKey(
                        name: "FK_CartEntityCartItemEntity_Cartitems_CartItemsId",
                        column: x => x.CartItemsId,
                        principalTable: "Cartitems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartEntityCartItemEntity_Carts_CartsId",
                        column: x => x.CartsId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderEntityOrderedProductEntity",
                columns: table => new
                {
                    OrderItemsId = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEntityOrderedProductEntity", x => new { x.OrderItemsId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_OrderEntityOrderedProductEntity_OrderedProducts_OrderItemsId",
                        column: x => x.OrderItemsId,
                        principalTable: "OrderedProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderEntityOrderedProductEntity_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderEntityUserEntity",
                columns: table => new
                {
                    UserOrdersId = table.Column<int>(type: "int", nullable: false),
                    UsersNavId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEntityUserEntity", x => new { x.UserOrdersId, x.UsersNavId });
                    table.ForeignKey(
                        name: "FK_OrderEntityUserEntity_AspNetUsers_UsersNavId",
                        column: x => x.UsersNavId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderEntityUserEntity_Orders_UserOrdersId",
                        column: x => x.UserOrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartEntityCartItemEntity_CartsId",
                table: "CartEntityCartItemEntity",
                column: "CartsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntityOrderedProductEntity_OrdersId",
                table: "OrderEntityOrderedProductEntity",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntityUserEntity_UsersNavId",
                table: "OrderEntityUserEntity",
                column: "UsersNavId");
        }
    }
}
