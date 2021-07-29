using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedProductsSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823), new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823), new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823), new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "Price", "UpdatedDate" },
                values: new object[,]
                {
                    { -1, new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823), "Best smartphone for the best daddies", "Shwamsung Galaxy Milky Way 8 - Infinitely Blue", 1337.69m, new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823) },
                    { -2, new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823), "4k to masses, money in cases", "Salami Mi LED TV 4S", 484.74m, new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823) },
                    { -3, new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823), "DC Powah!", "Edison Model S", 123000m, new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 28, 11, 28, 26, 589, DateTimeKind.Local).AddTicks(7451), new DateTime(2021, 7, 28, 11, 28, 26, 589, DateTimeKind.Local).AddTicks(7451) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 28, 11, 28, 26, 589, DateTimeKind.Local).AddTicks(7451), new DateTime(2021, 7, 28, 11, 28, 26, 589, DateTimeKind.Local).AddTicks(7451) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 28, 11, 28, 26, 589, DateTimeKind.Local).AddTicks(7451), new DateTime(2021, 7, 28, 11, 28, 26, 589, DateTimeKind.Local).AddTicks(7451) });
        }
    }
}
