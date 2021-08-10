using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ChangeDataSeedingDefaultDateToHardcodedValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 8, 4, 12, 20, 19, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 4, 12, 20, 19, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 8, 4, 12, 20, 19, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 4, 12, 20, 19, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 8, 4, 12, 20, 19, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 4, 12, 20, 19, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 8, 4, 12, 20, 19, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 4, 12, 20, 19, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 8, 4, 12, 20, 19, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 4, 12, 20, 19, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 8, 4, 12, 20, 19, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 4, 12, 20, 19, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345), new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345), new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345), new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345), new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345), new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345), new DateTime(2021, 7, 30, 8, 20, 52, 422, DateTimeKind.Utc).AddTicks(345) });
        }
    }
}
