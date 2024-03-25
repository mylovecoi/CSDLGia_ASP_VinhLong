using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class addGiaGDDTNhom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoiTuong",
                table: "GiaDvGdDtDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaNhom",
                table: "GiaDvGdDtDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SapXep",
                table: "GiaDvGdDtDm",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaDvGdDtDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GiaDvGdDtNhom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SapXep = table.Column<int>(type: "int", nullable: false),
                    MaNhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Syle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDvGdDtNhom", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaDvGdDtNhom");

            migrationBuilder.DropColumn(
                name: "DoiTuong",
                table: "GiaDvGdDtDm");

            migrationBuilder.DropColumn(
                name: "MaNhom",
                table: "GiaDvGdDtDm");

            migrationBuilder.DropColumn(
                name: "SapXep",
                table: "GiaDvGdDtDm");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaDvGdDtDm");
        }
    }
}
