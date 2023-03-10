using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class Add_GiaKhungGiaDatCt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiaKhungGiaDatCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vungkt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loaixa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giatt = table.Column<double>(type: "float", nullable: false),
                    Giatd = table.Column<double>(type: "float", nullable: false),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaKhungGiaDatCt", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaKhungGiaDatCt");
        }
    }
}
