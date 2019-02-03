using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class orderOrderedAndNotesForItemDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ordered",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "ItemDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ordered",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "ItemDetails");
        }
    }
}
