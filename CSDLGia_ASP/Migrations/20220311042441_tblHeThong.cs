using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class tblHeThong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblDMChucNang",
                columns: table => new
                {
                    MaChucNang = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    STT = table.Column<int>(type: "int", nullable: true),
                    KiHieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenChucNang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CapDo = table.Column<int>(type: "int", nullable: true),
                    MaGoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    CongBo = table.Column<bool>(type: "bit", nullable: false),
                    PhanLoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenBangHoSo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenBangChiTiet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlTongHop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DM_DanhSach = table.Column<bool>(type: "bit", nullable: true),
                    DM_ThayDoi = table.Column<bool>(type: "bit", nullable: true),
                    HS_DanhSach = table.Column<bool>(type: "bit", nullable: true),
                    HS_ThayDoi = table.Column<bool>(type: "bit", nullable: true),
                    HS_HoanThanh = table.Column<bool>(type: "bit", nullable: true),
                    K_BaoCao = table.Column<bool>(type: "bit", nullable: true),
                    K_ThongTin = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDMChucNang", x => x.MaChucNang);
                });

            migrationBuilder.CreateTable(
                name: "tblDSDiaBan",
                columns: table => new
                {
                    MaDiaBan = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    STT = table.Column<int>(type: "int", nullable: false),
                    TenDiaBan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDSDiaBan", x => x.MaDiaBan);
                });

            migrationBuilder.CreateTable(
                name: "tblDSDonVi",
                columns: table => new
                {
                    MaDonVi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    STT = table.Column<int>(type: "int", nullable: false),
                    TenDonVi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaDiaBan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaQHNS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThongTinLienHe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailQuanLy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailQuanTri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoNgayLamViec = table.Column<int>(type: "int", nullable: true),
                    TenDonViHienThi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDonViChuQuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChucVuQuanLy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChucVuKyThay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenQuanLy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaDanh = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    STT = table.Column<int>(type: "int", nullable: true),
                    MaDonVi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaNhomTaiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenTaiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: true),
                    ThongBao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLanDangNhap = table.Column<int>(type: "int", nullable: false),
                    NhapLieu = table.Column<bool>(type: "bit", nullable: true),
                    TongHop = table.Column<bool>(type: "bit", nullable: true),
                    QuanTri = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDSTaiKhoan", x => x.TenDangNhap);
                });

            migrationBuilder.CreateTable(
                name: "tblHeThong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaDanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDonVi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoFax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblHeThong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblPhanQuyen",
                columns: table => new
                {
                    MaChucNang = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhanQuyen = table.Column<bool>(type: "bit", nullable: true),
                    DM_DanhSach = table.Column<int>(type: "int", nullable: true),
                    DM_ThayDoi = table.Column<int>(type: "int", nullable: true),
                    HS_DanhSach = table.Column<int>(type: "int", nullable: true),
                    HS_ThayDoi = table.Column<int>(type: "int", nullable: true),
                    HS_HoanThanh = table.Column<int>(type: "int", nullable: true),
                    K_BaoCao = table.Column<int>(type: "int", nullable: true),
                    K_ThongTin = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPhanQuyen", x => new { x.MaChucNang, x.TenDangNhap });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblDMChucNang");

            migrationBuilder.DropTable(
                name: "tblDSDiaBan");

            migrationBuilder.DropTable(
                name: "tblDSDonVi");

            migrationBuilder.DropTable(
                name: "tblDSTaiKhoan");

            migrationBuilder.DropTable(
                name: "tblHeThong");

            migrationBuilder.DropTable(
                name: "tblPhanQuyen");
        }
    }
}
