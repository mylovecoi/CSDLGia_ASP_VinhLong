using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updategiahhst5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaHangHoaTaiSieuThiDmHHTaiSieuThi");

            migrationBuilder.DropTable(
                name: "GiaHangHoaTaiSieuThiDmSieuThi");

            migrationBuilder.CreateTable(
                name: "GiaHangHoaTaiSieuThiDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tentt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHangHoaTaiSieuThiDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaHangHoaTaiSieuThiDmCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Matt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenhanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dacdiemkt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Xuatxu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHangHoaTaiSieuThiDmCt", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaHangHoaTaiSieuThiDm");

            migrationBuilder.DropTable(
                name: "GiaHangHoaTaiSieuThiDmCt");

            migrationBuilder.CreateTable(
                name: "GiaHangHoaTaiSieuThiDmHHTaiSieuThi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mahanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenhanghoa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHangHoaTaiSieuThiDmHHTaiSieuThi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaHangHoaTaiSieuThiDmSieuThi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Masieuthi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tensieuthi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHangHoaTaiSieuThiDmSieuThi", x => x.Id);
                });
        }
    }
}
