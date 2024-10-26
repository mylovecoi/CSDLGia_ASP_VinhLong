using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class adddm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DmGiaTriTaiSan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaGiaTriTaiSan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenGiaTriTaiSan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmGiaTriTaiSan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmLinhVuc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLinhVuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenLinhVuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmLinhVuc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmLoaiHinhDoanhNghiep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLoaiHinhDoanhNghiep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenLoaiHinhDoanhNghiep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmLoaiHinhDoanhNghiep", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmPhanLoaiGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhanLoaiGia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenPhanLoaiGia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmPhanLoaiGia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmPhanLoaiGiaDat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhanLoaiGiaDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenPhanLoaiGiaDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmPhanLoaiGiaDat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmPhanLoaiHangHoaDichVu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHangHoaDichVu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenHangHoaDichVu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmPhanLoaiHangHoaDichVu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmPhanLoaiKhuVuc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhuVuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenKhuVuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmPhanLoaiKhuVuc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmPhanLoaiNhomHangHoaDichVu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhomHangHoaDichVu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenNhomHangHoaDichVu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmPhanLoaiNhomHangHoaDichVu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmPhanLoaiTaiSan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTaiSan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenTaiSan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmPhanLoaiTaiSan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmPhuongPhapThamDinh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhuongPhapThamDinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenPhuongPhapThamDinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmPhuongPhapThamDinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmThuocTinh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaThuocTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenThuocTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmThuocTinh", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DmGiaTriTaiSan");

            migrationBuilder.DropTable(
                name: "DmLinhVuc");

            migrationBuilder.DropTable(
                name: "DmLoaiHinhDoanhNghiep");

            migrationBuilder.DropTable(
                name: "DmPhanLoaiGia");

            migrationBuilder.DropTable(
                name: "DmPhanLoaiGiaDat");

            migrationBuilder.DropTable(
                name: "DmPhanLoaiHangHoaDichVu");

            migrationBuilder.DropTable(
                name: "DmPhanLoaiKhuVuc");

            migrationBuilder.DropTable(
                name: "DmPhanLoaiNhomHangHoaDichVu");

            migrationBuilder.DropTable(
                name: "DmPhanLoaiTaiSan");

            migrationBuilder.DropTable(
                name: "DmPhuongPhapThamDinh");

            migrationBuilder.DropTable(
                name: "DmThuocTinh");
        }
    }
}
