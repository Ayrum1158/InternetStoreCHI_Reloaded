using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class RenamedProductDateToDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderedPrice",
                table: "OrderedProducts",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "OrderedDate",
                table: "OrderedProducts",
                newName: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderedProducts",
                newName: "OrderedPrice");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "OrderedProducts",
                newName: "OrderedDate");
        }
    }
}
