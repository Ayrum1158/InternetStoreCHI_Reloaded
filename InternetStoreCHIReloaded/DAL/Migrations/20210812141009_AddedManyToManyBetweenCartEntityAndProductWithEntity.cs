using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedManyToManyBetweenCartEntityAndProductWithEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsWithQuantity_Carts_CartEntityId",
                table: "ProductsWithQuantity");

            migrationBuilder.DropIndex(
                name: "IX_ProductsWithQuantity_CartEntityId",
                table: "ProductsWithQuantity");

            migrationBuilder.DropColumn(
                name: "CartEntityId",
                table: "ProductsWithQuantity");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartEntityProductWithQuantityEntity");

            migrationBuilder.AddColumn<int>(
                name: "CartEntityId",
                table: "ProductsWithQuantity",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductsWithQuantity_CartEntityId",
                table: "ProductsWithQuantity",
                column: "CartEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsWithQuantity_Carts_CartEntityId",
                table: "ProductsWithQuantity",
                column: "CartEntityId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
