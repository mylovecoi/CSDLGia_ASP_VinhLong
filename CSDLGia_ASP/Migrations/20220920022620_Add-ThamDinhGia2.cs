using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CSDLGia_ASP.Migrations
{
    public partial class AddThamDinhGia2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThamDinhGiaDmHh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenhanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongsokt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Xuatxu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThamDinhGiaDmHh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThamDinhGiaDv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diachi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoidaidien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chucvu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sothe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThamDinhGiaDv", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThamDinhGiaDmHh");

            migrationBuilder.DropTable(
                name: "ThamDinhGiaDv");
        }
    }
}
