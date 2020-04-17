using Microsoft.EntityFrameworkCore.Migrations;

namespace Consultorio.Migrations
{
    public partial class updatecitas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Citas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Precio",
                table: "Citas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
