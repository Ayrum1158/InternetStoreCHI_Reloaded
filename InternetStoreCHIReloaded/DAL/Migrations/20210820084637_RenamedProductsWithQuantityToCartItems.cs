using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class RenamedProductsWithQuantityToCartItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartEntityProductWithQuantityEntity");

            migrationBuilder.DropTable(
                name: "ProductsWithQuantity");

            migrationBuilder.CreateTable(
                name: "Cartitems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartitems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cartitems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CartEntityCartItemEntity_CartsId",
                table: "CartEntityCartItemEntity",
                column: "CartsId");

            migrationBuilder.CreateIndex(
                name: "IX_Cartitems_ProductId",
                table: "Cartitems",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartEntityCartItemEntity");

            migrationBuilder.DropTable(
                name: "Cartitems");

            migrationBuilder.CreateTable(
                name: "ProductsWithQuantity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsWithQuantity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsWithQuantity_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartEntityProductWithQuantityEntity",
                columns: table => new
                {
                    CartItemsId = table.Column<int>(type: "int", nullable: false),
                    CartsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartEntityProductWithQuantityEntity", x => new { x.CartItemsId, x.CartsId });
                    table.ForeignKey(
                        name: "FK_CartEntityProductWithQuantityEntity_Carts_CartsId",
                        column: x => x.CartsId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartEntityProductWithQuantityEntity_ProductsWithQuantity_CartItemsId",
                        column: x => x.CartItemsId,
                        principalTable: "ProductsWithQuantity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartEntityProductWithQuantityEntity_CartsId",
                table: "CartEntityProductWithQuantityEntity",
                column: "CartsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsWithQuantity_ProductId",
                table: "ProductsWithQuantity",
                column: "ProductId");
        }
    }
}
