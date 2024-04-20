using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class AddKeKhaiDangKyGia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KeKhaiDangKyGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaCsKd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhanLoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reports = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoQD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayQD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoQdLk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayQdLk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayThucHien = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LyDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoHsDuyet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeKhaiDangKyGia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeKhaiDangKyGiaCSKD",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaNghe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaCqCq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaCsKd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenCsKd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeKhaiDangKyGiaCSKD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeKhaiDangKyGiaCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaCsKd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MucGiaKeKhaiLk = table.Column<double>(type: "float", nullable: false),
                    MucGiaKeKhai = table.Column<double>(type: "float", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeKhaiDangKyGiaCt", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeKhaiDangKyGia");

            migrationBuilder.DropTable(
                name: "KeKhaiDangKyGiaCSKD");

            migrationBuilder.DropTable(
                name: "KeKhaiDangKyGiaCt");
        }
    }
}
