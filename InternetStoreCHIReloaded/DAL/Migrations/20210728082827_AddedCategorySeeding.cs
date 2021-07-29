using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedCategorySeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[] { -1, new DateTime(2021, 7, 28, 11, 28, 26, 589, DateTimeKind.Local).AddTicks(7451), "Like the phone, but cooler", "Smatrphone", new DateTime(2021, 7, 28, 11, 28, 26, 589, DateTimeKind.Local).AddTicks(7451) });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[] { -2, new DateTime(2021, 7, 28, 11, 28, 26, 589, DateTimeKind.Local).AddTicks(7451), "Like the smartphone, but bigger", "TV", new DateTime(2021, 7, 28, 11, 28, 26, 589, DateTimeKind.Local).AddTicks(7451) });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[] { -3, new DateTime(2021, 7, 28, 11, 28, 26, 589, DateTimeKind.Local).AddTicks(7451), "Totally not like a TV, gives you a nice ride though", "Car", new DateTime(2021, 7, 28, 11, 28, 26, 589, DateTimeKind.Local).AddTicks(7451) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
