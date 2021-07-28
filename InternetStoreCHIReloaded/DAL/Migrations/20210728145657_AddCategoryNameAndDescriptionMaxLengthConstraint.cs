using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddCategoryNameAndDescriptionMaxLengthConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
                columns: new[] { "CreatedDate", "Name", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 28, 14, 56, 56, 783, DateTimeKind.Utc).AddTicks(3288), "Salami Mi LED TV 4S 50 inch", new DateTime(2021, 7, 28, 14, 56, 56, 783, DateTimeKind.Utc).AddTicks(3288) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 28, 14, 56, 56, 783, DateTimeKind.Utc).AddTicks(3288), new DateTime(2021, 7, 28, 14, 56, 56, 783, DateTimeKind.Utc).AddTicks(3288) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

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

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823), new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "Name", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823), "Salami Mi LED TV 4S", new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823), new DateTime(2021, 7, 28, 9, 26, 0, 734, DateTimeKind.Utc).AddTicks(9823) });
        }
    }
}
