using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class removeTotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "OrderDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0);
        }
    }
}
