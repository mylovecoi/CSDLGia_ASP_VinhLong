using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updatedvkcb4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hienthi",
                table: "GiaDvKcbCt",
                type: "nvarchar(max)",
                nullable: true);

            /*migrationBuilder.AddColumn<string>(
                name: "Manhom",
                table: "GiaDvKcbCt",
                type: "nvarchar(max)",
                nullable: true);*/

            migrationBuilder.AddColumn<double>(
                name: "Sapxep",
                table: "GiaDvKcbCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hienthi",
                table: "GiaDvKcbCt");

            /*migrationBuilder.DropColumn(
                name: "Manhom",
                table: "GiaDvKcbCt");*/

            migrationBuilder.DropColumn(
                name: "Sapxep",
                table: "GiaDvKcbCt");
        }
    }
}
