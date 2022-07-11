using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class AddTtTdDn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TtDnTd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diachi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diadanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chucdanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoiky = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Noidknopthue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tailieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giayphepkd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Settingdvvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vtxk = table.Column<double>(type: "float", nullable: false),
                    Vtxb = table.Column<double>(type: "float", nullable: false),
                    Vtxtx = table.Column<double>(type: "float", nullable: false),
                    Vtch = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TtDnTd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TtDnTdCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manganh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manghe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TtDnTdCt", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TtDnTd");

            migrationBuilder.DropTable(
                name: "TtDnTdCt");
        }
    }
}
