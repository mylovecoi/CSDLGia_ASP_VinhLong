using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class addThamDinhGiaHD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThamDinhGiaHD",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaHoiDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToTung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanCuPhapLy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TheoDeNghi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CapHoiDong = table.Column<int>(type: "int", nullable: false),
                    LoaiHoiDong = table.Column<int>(type: "int", nullable: false),
                    SoQD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayQD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoQuanBanHanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenHoiDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChuTichHoiDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChucVu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NhiemVuHoiDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiDungQD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaTinhApDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaHuyenApDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileQD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileQD_Base64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThamDinhGiaHD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThamDinhGiaHDCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHoiDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STT = table.Column<int>(type: "int", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChucVu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VaiTro = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThamDinhGiaHDCt", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThamDinhGiaHD");

            migrationBuilder.DropTable(
                name: "ThamDinhGiaHDCt");
        }
    }
}
