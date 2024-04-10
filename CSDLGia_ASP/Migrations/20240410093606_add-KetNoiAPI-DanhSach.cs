using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class addKetNoiAPIDanhSach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KetNoiAPI_DanhSach",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkTruyenGet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkTruyenPost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkTruyenPut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkNhanGet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkNhanPost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkNhanPut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KetNoiAPI_DanhSach", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KetNoiAPI_DanhSach");
        }
    }
}
