using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ChangedCartAndOrdersPart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ProductsSet_UserCartId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ProductsSetEntityUserEntity");

            migrationBuilder.DropTable(
                name: "ProductWithQuantityEntityProductsSetEntity");

            migrationBuilder.DropTable(
                name: "ProductsSet");

            migrationBuilder.AddColumn<int>(
                name: "CartEntityId",
                table: "ProductsWithQuantity",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsWithQuantity_CartEntityId",
                table: "ProductsWithQuantity",
                column: "CartEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Carts_UserCartId",
                table: "AspNetUsers",
                column: "UserCartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsWithQuantity_Carts_CartEntityId",
                table: "ProductsWithQuantity",
                column: "CartEntityId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Carts_UserCartId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsWithQuantity_Carts_CartEntityId",
                table: "ProductsWithQuantity");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_ProductsWithQuantity_CartEntityId",
                table: "ProductsWithQuantity");

            migrationBuilder.DropColumn(
                name: "CartEntityId",
                table: "ProductsWithQuantity");

            migrationBuilder.CreateTable(
                name: "ProductsSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductsSetEntityUserEntity",
                columns: table => new
                {
                    UserOrdersId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsSetEntityUserEntity", x => new { x.UserOrdersId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ProductsSetEntityUserEntity_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsSetEntityUserEntity_ProductsSet_UserOrdersId",
                        column: x => x.UserOrdersId,
                        principalTable: "ProductsSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductWithQuantityEntityProductsSetEntity",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    ProductsSetsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWithQuantityEntityProductsSetEntity", x => new { x.ProductsId, x.ProductsSetsId });
                    table.ForeignKey(
                        name: "FK_ProductWithQuantityEntityProductsSetEntity_ProductsSet_ProductsSetsId",
                        column: x => x.ProductsSetsId,
                        principalTable: "ProductsSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductWithQuantityEntityProductsSetEntity_ProductsWithQuantity_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "ProductsWithQuantity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSetEntityUserEntity_UsersId",
                table: "ProductsSetEntityUserEntity",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWithQuantityEntityProductsSetEntity_ProductsSetsId",
                table: "ProductWithQuantityEntityProductsSetEntity",
                column: "ProductsSetsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ProductsSet_UserCartId",
                table: "AspNetUsers",
                column: "UserCartId",
                principalTable: "ProductsSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
