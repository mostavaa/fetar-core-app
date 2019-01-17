using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class DecimalItemPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Items",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
