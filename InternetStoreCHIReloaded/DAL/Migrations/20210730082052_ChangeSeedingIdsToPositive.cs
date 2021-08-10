using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ChangeSeedingIdsToPositive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[] { 1, new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345), "Like the phone, but cooler", "Smatrphone", new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345) });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[] { 2, new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345), "Like the smartphone, but bigger", "TV", new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345) });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[] { 3, new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345), "Totally not like a TV, gives you a nice ride though", "Car", new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Name", "Price", "UpdatedDate" },
                values: new object[] { 1, 1, new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345), "Best smartphone for the best daddies", "Shwamsung Galaxy Milky Way 8 - Infinitely Blue", 1337.69m, new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Name", "Price", "UpdatedDate" },
                values: new object[] { 2, 2, new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345), "4k to masses, money in cases", "Salami Mi LED TV 4S 50 inch", 484.74m, new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Name", "Price", "UpdatedDate" },
                values: new object[] { 3, 3, new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345), "DC Powah!", "Edison Model S", 123000m, new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[] { -1, new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291), "Like the phone, but cooler", "Smatrphone", new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291) });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[] { -2, new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291), "Like the smartphone, but bigger", "TV", new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291) });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[] { -3, new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291), "Totally not like a TV, gives you a nice ride though", "Car", new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Name", "Price", "UpdatedDate" },
                values: new object[] { -1, -1, new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291), "Best smartphone for the best daddies", "Shwamsung Galaxy Milky Way 8 - Infinitely Blue", 1337.69m, new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Name", "Price", "UpdatedDate" },
                values: new object[] { -2, -2, new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291), "4k to masses, money in cases", "Salami Mi LED TV 4S 50 inch", 484.74m, new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Name", "Price", "UpdatedDate" },
                values: new object[] { -3, -3, new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291), "DC Powah!", "Edison Model S", 123000m, new DateTime(2021, 7, 29, 8, 19, 55, 93, DateTimeKind.Utc).AddTicks(1291) });
        }
    }
}
