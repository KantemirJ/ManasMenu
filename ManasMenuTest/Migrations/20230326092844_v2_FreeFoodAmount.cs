using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManasMenuTest.Migrations
{
    public partial class v2_FreeFoodAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountForFree",
                table: "Canteen",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountForFree",
                table: "Canteen");
        }
    }
}
