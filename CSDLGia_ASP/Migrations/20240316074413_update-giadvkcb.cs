using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updategiadvkcb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ghichu",
                table: "GiaDvKcbDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sapxep",
                table: "GiaDvKcbDm",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ghichu",
                table: "GiaDvKcbDm");

            migrationBuilder.DropColumn(
                name: "Sapxep",
                table: "GiaDvKcbDm");
        }
    }
}
