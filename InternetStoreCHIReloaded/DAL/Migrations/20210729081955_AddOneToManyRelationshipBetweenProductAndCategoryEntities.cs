using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddOneToManyRelationshipBetweenProductAndCategoryEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291), new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291), new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291), new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CategoryId", "CreatedDate", "UpdatedDate" },
                values: new object[] { -3, new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291), new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CategoryId", "CreatedDate", "UpdatedDate" },
                values: new object[] { -2, new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291), new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CategoryId", "CreatedDate", "UpdatedDate" },
                values: new object[] { -1, new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291), new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291) });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 28, 14, 56, 56, 783, DateTimeKind.Utc).AddTicks(3288), new DateTime(2021, 7, 28, 14, 56, 56, 783, DateTimeKind.Utc).AddTicks(3288) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 28, 14, 56, 56, 783, DateTimeKind.Utc).AddTicks(3288), new DateTime(2021, 7, 28, 14, 56, 56, 783, DateTimeKind.Utc).AddTicks(3288) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 28, 14, 56, 56, 783, DateTimeKind.Utc).AddTicks(3288), new DateTime(2021, 7, 28, 14, 56, 56, 783, DateTimeKind.Utc).AddTicks(3288) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 28, 14, 56, 56, 783, DateTimeKind.Utc).AddTicks(3288), new DateTime(2021, 7, 28, 14, 56, 56, 783, DateTimeKind.Utc).AddTicks(3288) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 28, 14, 56, 56, 783, DateTimeKind.Utc).AddTicks(3288), new DateTime(2021, 7, 28, 14, 56, 56, 783, DateTimeKind.Utc).AddTicks(3288) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 28, 14, 56, 56, 783, DateTimeKind.Utc).AddTicks(3288), new DateTime(2021, 7, 28, 14, 56, 56, 783, DateTimeKind.Utc).AddTicks(3288) });
        }
    }
}
