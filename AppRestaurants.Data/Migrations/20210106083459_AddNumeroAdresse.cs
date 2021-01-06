using Microsoft.EntityFrameworkCore.Migrations;

namespace AppRestaurants.Data.Migrations
{
    public partial class AddNumeroAdresse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "Adresses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Adresses");
        }
    }
}
