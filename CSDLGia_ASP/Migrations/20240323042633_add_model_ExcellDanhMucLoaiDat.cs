using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class add_model_ExcellDanhMucLoaiDat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExcellDanhMucLoaiDat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maloaidat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loaidat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LineStart = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcellDanhMucLoaiDat", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcellDanhMucLoaiDat");
        }
    }
}
