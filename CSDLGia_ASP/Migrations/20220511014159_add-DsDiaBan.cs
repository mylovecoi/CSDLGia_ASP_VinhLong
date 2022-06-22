using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class addDsDiaBan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblDSDiaBan");

            migrationBuilder.DropTable(
                name: "tblDSDonVi");

            migrationBuilder.DropTable(
                name: "tblDSTaiKhoan");
        }

        /*protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "DsDiaBan",
                columns: table => new
                {
                    MaDiaBan = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDiaBan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DsDiaBan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDSDonVi",
                columns: table => new
                {
                    MaDonVi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChucVuKyThay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChucVuQuanLy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaDanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailQuanLy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailQuanTri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaDiaBan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaQHNS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STT = table.Column<int>(type: "int", nullable: false),
                    SoNgayLamViec = table.Column<int>(type: "int", nullable: true),
                    TenDonVi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenDonViChuQuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDonViHienThi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenQuanLy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThongTinLienHe = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDSDonVi", x => x.MaDonVi);
                });

            migrationBuilder.CreateTable(
                name: "tblDSTaiKhoan",
                columns: table => new
                {
                    TenDangNhap = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaDonVi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaNhomTaiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NhapLieu = table.Column<bool>(type: "bit", nullable: true),
                    QuanTri = table.Column<bool>(type: "bit", nullable: true),
                    STT = table.Column<int>(type: "int", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLanDangNhap = table.Column<int>(type: "int", nullable: false),
                    TenTaiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThongBao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongHop = table.Column<bool>(type: "bit", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDSTaiKhoan", x => x.TenDangNhap);
                });
        }*/
    }
}
