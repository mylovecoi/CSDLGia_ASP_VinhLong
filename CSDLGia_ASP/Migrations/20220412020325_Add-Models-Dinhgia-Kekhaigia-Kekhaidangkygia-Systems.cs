using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CSDLGia_ASP.Migrations
{
    public partial class AddModelsDinhgiaKekhaigiaKekhaidangkygiaSystems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
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
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tailieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giayphepkd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Settingdvvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vtxk = table.Column<double>(type: "float", nullable: false),
                    Vtxb = table.Column<double>(type: "float", nullable: false),
                    Vtxtx = table.Column<double>(type: "float", nullable: false),
                    Vtch = table.Column<double>(type: "float", nullable: false),
                    Loaihinhhd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Xangdau = table.Column<double>(type: "float", nullable: false),
                    Dien = table.Column<double>(type: "float", nullable: false),
                    Khidau = table.Column<double>(type: "float", nullable: false),
                    Phan = table.Column<double>(type: "float", nullable: false),
                    Thuocbvtv = table.Column<double>(type: "float", nullable: false),
                    Vacxingsgc = table.Column<double>(type: "float", nullable: false),
                    Muoi = table.Column<double>(type: "float", nullable: false),
                    Suate6t = table.Column<double>(type: "float", nullable: false),
                    Duong = table.Column<double>(type: "float", nullable: false),
                    Thocgao = table.Column<double>(type: "float", nullable: false),
                    Thuocpcb = table.Column<double>(type: "float", nullable: false),
                    Kiemtra = table.Column<bool>(type: "bit", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyLvCc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_CompanyLvCc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiaBanHd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaBanHd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diachi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloaiql = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttlienhe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emailql = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emailqt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvhienthi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcqhienthi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmDvt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmDvt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmHinhThucThanhToan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahinhthucthanhtoan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenhinhthucthanhtoan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmHinhThucThanhToan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmLoaiGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maloaigia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenloaigia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmLoaiGia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmNganhKd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manganh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennganh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmNganhKd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DmNgheKd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manganh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manghe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennghe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DmNgheKd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DsDonViTdg",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tendv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    diachi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nguoidaidien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    chucvu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sothe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngaycap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DsDonViTdg", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DsNhomTaiKhoan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Permission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macdinh = table.Column<bool>(type: "bit", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Chucnang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DsNhomTaiKhoan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DsThamDinhVien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GIAY_CN_DU_DK_DKKD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEN_TIENG_VIET = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HO_TEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NGAY_SINH = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GIOI_TINH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CMT_HO_CHIEU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NGAY_CAP_CMT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NOI_CAP_CMT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NGUYEN_QUAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TINH_THANH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DIA_CHI_THUONG_TRU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DIA_CHI_TAM_TRU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DIEN_THOAI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SO_THE_TDV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NGAY_CAP_THE_TDV = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LA_NGUOI_DAI_DIEN_PL = table.Column<bool>(type: "bit", nullable: false),
                    LA_LANH_DAO_DN = table.Column<bool>(type: "bit", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DsThamDinhVien", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DsVanPhong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vanphong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hoten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chucvu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Skype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DsVanPhong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DsXaPhuong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DsXaPhuong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tendonvi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maqhns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diachi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thutruong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ketoan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoilapbieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diadanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Setting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtinhd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihanlt = table.Column<double>(type: "float", nullable: false),
                    Thoihanvt = table.Column<double>(type: "float", nullable: false),
                    Thoihangs = table.Column<double>(type: "float", nullable: false),
                    Thoihantacn = table.Column<double>(type: "float", nullable: false),
                    Sodvvt = table.Column<double>(type: "float", nullable: false),
                    Emailql = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvhienthi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcqhienthi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sudungemail = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaBanNhaTaiDinhCu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenduan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dongiathuemua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dongiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiemkc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiemht = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaBanNhaTaiDinhCu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaCuocVanChuyen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thaotac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaCuocVanChuyen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaCuocVanChuyenCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tencuoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tukm = table.Column<double>(type: "float", nullable: false),
                    Denkm = table.Column<double>(type: "float", nullable: false),
                    Bachh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giavc1 = table.Column<double>(type: "float", nullable: false),
                    Giavc2 = table.Column<double>(type: "float", nullable: false),
                    Giavc3 = table.Column<double>(type: "float", nullable: false),
                    Giavc4 = table.Column<double>(type: "float", nullable: false),
                    Giavc5 = table.Column<double>(type: "float", nullable: false),
                    Gc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaCuocVanChuyenCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatDiaBan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Noidung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatDiaBan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatDiaBanCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maloaidat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Khuvuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diemdau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diemcuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loaiduong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mdsd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giavt1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Giavt2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Giavt3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Giavt4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Giavt5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Hesok = table.Column<double>(type: "float", nullable: false),
                    Sapxep = table.Column<double>(type: "float", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatDiaBanCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatDiaBanTt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngayqd_banhanh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayqd_apdung = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatDiaBanTt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatDuAn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenduan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dientich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loaidat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenduong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loaiduong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vitri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdgiadato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdgiadattmdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdgiadatsxkd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdgiadatnn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdgiadatnuoits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdgiadatmuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdpddato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdpddattmdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdpddatsxkd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdpddatnn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdpddatnuoits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdpddatmuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhomduan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thaotac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatDuAn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatDuAnDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manhomduan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhomduan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatDuAnDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatPhanLoai",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vitri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maloaidat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientich = table.Column<double>(type: "float", nullable: false),
                    Giatri = table.Column<double>(type: "float", nullable: false),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thaotac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatPhanLoai", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatPhanLoaiCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maloaidat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Khuvuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vitri = table.Column<int>(type: "int", nullable: false),
                    Banggiadat = table.Column<double>(type: "float", nullable: false),
                    Giacuthe = table.Column<double>(type: "float", nullable: false),
                    Hesodc = table.Column<double>(type: "float", nullable: false),
                    Sapxep = table.Column<double>(type: "float", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatPhanLoaiCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatPhanLoaiDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Loaidat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mavitri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenvitri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giatri = table.Column<double>(type: "float", nullable: false),
                    Mahuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatPhanLoaiDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatThiTruong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Khuvuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenduan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqdpagia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqddaugia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqdgiakhoidiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqdkqdaugia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatThiTruong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatThiTruongCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loaidat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Khuvuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientich = table.Column<double>(type: "float", nullable: false),
                    Giaquydinh = table.Column<double>(type: "float", nullable: false),
                    Giathitruong = table.Column<double>(type: "float", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tenkhudat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diachi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqdban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoigianban = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Soqdgiakd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoigiangiakd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dientichdat = table.Column<double>(type: "float", nullable: false),
                    Dongiadat = table.Column<double>(type: "float", nullable: false),
                    Giatridat = table.Column<double>(type: "float", nullable: false),
                    Dientichts = table.Column<double>(type: "float", nullable: false),
                    Dongiats = table.Column<double>(type: "float", nullable: false),
                    Giatrits = table.Column<double>(type: "float", nullable: false),
                    Tonggiatri = table.Column<double>(type: "float", nullable: false),
                    Giadaugia = table.Column<double>(type: "float", nullable: false),
                    Hdban = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatThiTruongCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDauGiaDat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Khuvuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenduan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqdpagia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqddaugia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqdgiakhoidiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqdkqdaugia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDauGiaDat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDauGiaDatCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Loaidat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Khuvuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientich = table.Column<double>(type: "float", nullable: false),
                    Giakhoidiem = table.Column<double>(type: "float", nullable: false),
                    Giadaugia = table.Column<double>(type: "float", nullable: false),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Solo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sothua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tobanbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sotobanbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sotobando = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDauGiaDatCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDauGiaDatTs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenduan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dientichdat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientichsanxd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqdpagia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqddaugia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqdgiakhoidiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqdkqdaugia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDauGiaDatTs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDauGiaDatTsCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Loaidat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenduong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loaiduong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vitri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdgiadato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdgiadattmdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdgiadatsxkd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdgiadatnn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdgiadatnuoits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdgiadatmuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdpddato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdpddattmdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdpddatsxkd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdpddatnn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdpddatnuoits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdpddatmuoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qdpdgiatstd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kqgiadaugiadat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kqgiadaugiats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kqgiadaugiadatts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientichdat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientichsanxd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDauGiaDatTsCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDvGdDt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tunam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dennam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDvGdDt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDvGdDtCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Namapdung1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giathanhthi1 = table.Column<double>(type: "float", nullable: false),
                    Gianongthon1 = table.Column<double>(type: "float", nullable: false),
                    Giamiennui1 = table.Column<double>(type: "float", nullable: false),
                    Namapdung2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giathanhthi2 = table.Column<double>(type: "float", nullable: false),
                    Gianongthon2 = table.Column<double>(type: "float", nullable: false),
                    Giamiennui2 = table.Column<double>(type: "float", nullable: false),
                    Namapdung3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giathanhthi3 = table.Column<double>(type: "float", nullable: false),
                    Gianongthon3 = table.Column<double>(type: "float", nullable: false),
                    Giamiennui3 = table.Column<double>(type: "float", nullable: false),
                    Namapdung4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giathanhthi4 = table.Column<double>(type: "float", nullable: false),
                    Gianongthon4 = table.Column<double>(type: "float", nullable: false),
                    Giamiennui4 = table.Column<double>(type: "float", nullable: false),
                    Namapdung5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giathanhthi5 = table.Column<double>(type: "float", nullable: false),
                    Gianongthon5 = table.Column<double>(type: "float", nullable: false),
                    Giamiennui5 = table.Column<double>(type: "float", nullable: false),
                    Gc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDvGdDtCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDvGdDtDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hientrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDvGdDtDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDvKcb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenbv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dongia = table.Column<double>(type: "float", nullable: false),
                    Ttqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thaotac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDvKcb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDvKcbCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giadv = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madichvu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenspdv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDvKcbCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDvKcbDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hientrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madichvu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDvKcbDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaGdBatDongSan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kyhieuvb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvbanhanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngayapdung = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tieude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaGdBatDongSan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaHhDvCn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thaotac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHhDvCn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaHhDvCnCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dongia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHhDvCnCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaHhDvCnDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hientrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHhDvCnDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaKhungGiaDat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kyhieuvb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvbanhanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngayapdung = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tieude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaKhungGiaDat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaLpTbNha",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaybh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dvbh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichuxdm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichuclcl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaLpTbNha", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaLpTbNhaCtClCl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capnha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoigiansd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tylehm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaLpTbNhaCtClCl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaLpTbNhaCtXdm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loaict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capnha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dongia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaLpTbNhaCtXdm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaMuaTaiSan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngayqd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Thongtinqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhathau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaMuaTaiSan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaNuocSh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tunam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dennam = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaNuocSh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaNuocShCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madoituong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doituongsd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giacothue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phibvmttyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phibvmt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thanhtien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Namchuathue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giachuathue = table.Column<double>(type: "float", nullable: false),
                    Namchuathue1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giachuathue1 = table.Column<double>(type: "float", nullable: false),
                    Namchuathue2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giachuathue2 = table.Column<double>(type: "float", nullable: false),
                    Namchuathue3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giachuathue3 = table.Column<double>(type: "float", nullable: false),
                    Namchuathue4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giachuathue4 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaNuocShCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaNuocShCtDf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doituong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giachuathue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giacothue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phibvmttyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phibvmt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thanhtien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaNuocShCtDf", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaNuocShDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madoituong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doituongsd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaNuocShDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaPhiChuyenGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaPhiChuyenGia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaPhiChuyenGiaCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mucgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaPhiChuyenGiaCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaPhiChuyenGiaDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenphi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tengia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaPhiChuyenGiaDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaPhiChuyenGiaNhom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaPhiChuyenGiaNhom", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaRung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenduan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientich = table.Column<double>(type: "float", nullable: false),
                    Dongia = table.Column<double>(type: "float", nullable: false),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaRung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaRungCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Noidung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientich = table.Column<double>(type: "float", nullable: false),
                    Dientichsd = table.Column<double>(type: "float", nullable: false),
                    Giatri = table.Column<double>(type: "float", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Giakhoidiem = table.Column<double>(type: "float", nullable: false),
                    Dongia = table.Column<double>(type: "float", nullable: false),
                    Dvthue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diachi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqdpd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoigianpd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Soqdgkd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoigiangkd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Thuetungay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Thuedenngay = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaRungCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaSpDvCi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thaotac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaSpDvCi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaSpDvCiCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dongia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaSpDvCiCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaSpDvCiDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hientrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaSpDvCiDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaSpDvCuThe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thaotac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaSpDvCuThe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaSpDvCuTheCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mucgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phanloaidv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaSpDvCuTheCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaSpDvKhungGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thaotac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaSpDvKhungGia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaSpDvKhungGiaCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giatoithieu = table.Column<double>(type: "float", nullable: false),
                    Giatoida = table.Column<double>(type: "float", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phanloaidv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaSpDvKhungGiaCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaSpDvKhungGiaDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hientrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaSpDvKhungGiaDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaSpDvToiDa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thaotac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaSpDvToiDa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaSpDvToiDaCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dongia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phanloaidv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaSpDvToiDaCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaSpDvToiDaDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hientrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaSpDvToiDaDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaTaiSanCong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mataisan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtinhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tungay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Denngay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Giathue = table.Column<double>(type: "float", nullable: false),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Giapheduyet = table.Column<double>(type: "float", nullable: false),
                    Giaconlai = table.Column<double>(type: "float", nullable: false),
                    Giaban = table.Column<double>(type: "float", nullable: false),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dacdiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tentaisan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaTaiSanCong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaTaiSanCongCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mataisan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tentaisan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dacdiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giathue = table.Column<double>(type: "float", nullable: false),
                    Giaban = table.Column<double>(type: "float", nullable: false),
                    Giapheduyet = table.Column<double>(type: "float", nullable: false),
                    Giaconlai = table.Column<double>(type: "float", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaTaiSanCongCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaTaiSanCongDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mataisan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tentaisan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giatri = table.Column<double>(type: "float", nullable: false),
                    Hientrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaTaiSanCongDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaThiTruong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Thanglk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Namlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sobc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaybc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mahuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Matt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaThiTruong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaThiTruongCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenhh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dacdiemkt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loaigia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dongia = table.Column<double>(type: "float", nullable: false),
                    Nguontt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaThiTruongCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaThiTruongDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenhh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dacdiemkt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaThiTruongDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaThiTruongTt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaThiTruongTt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaThueMuaNhaXh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dongia = table.Column<double>(type: "float", nullable: false),
                    Tungay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Denngay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dongiathue = table.Column<double>(type: "float", nullable: false),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaThueMuaNhaXh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaThueMuaNhaXhCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dongia = table.Column<double>(type: "float", nullable: false),
                    Dongiathue = table.Column<double>(type: "float", nullable: false),
                    Tungay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Denngay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dvthue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hdthue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ththue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diachi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqdpd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoigianpd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Soqddg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoigiandg = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaThueMuaNhaXhCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaThueMuaNhaXhDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diachi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Donviql = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoigian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dientich = table.Column<double>(type: "float", nullable: false),
                    Hientrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaThueMuaNhaXhDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaThueNhaCongVu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenduan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dientich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dongiathue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiemkc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiemht = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaThueNhaCongVu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaThueTaiNguyen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiemlk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Soqdlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cqbh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaThueTaiNguyen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaThueTaiNguyenCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaThueTaiNguyenCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaThueTaiNguyenDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cap5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sapxep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaThueTaiNguyenDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaThueTaiNguyenNhom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sapxep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaThueTaiNguyenNhom", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaTroGiaTroCuoc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thaotac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaTroGiaTroCuoc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaTroGiaTroCuocCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dongia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaTroGiaTroCuocCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaTroGiaTroCuocDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenspdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hientrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaTroGiaTroCuocDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaVangNgoaiTe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiemlk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Soqdlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thongtin_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaVangNgoaiTe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaVangNgoaiTeCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahhdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenhhdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dacdiemkt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Xuatxu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loaigia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguontt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaVangNgoaiTeCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaVangNgoaiTeDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahhdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenhhdv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dacdiemkt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Xuatxu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaVangNgoaiTeDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeKhaiGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhanLoaiGia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeKhaiGia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeKhaiGiaCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeKhaiGiaCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkCuocVcHk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkCuocVcHk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkCuocVcHkCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkCuocVcHkCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkDkg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theoqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dtlh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pldn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipt1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipt2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipt3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipt4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipt5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkDkg", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkDkgCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maxa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenhh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quycach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giakk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkdonvisxkd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkqcpc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nksanluongdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nksanluongtt = table.Column<double>(type: "float", nullable: false),
                    Nksanluonggc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkgiamuackdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkgiamuacktt = table.Column<double>(type: "float", nullable: false),
                    Nkgiamuackgc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkthuedvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkthuett = table.Column<double>(type: "float", nullable: false),
                    Nkthueghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkthuettdbdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkthuettdbtt = table.Column<double>(type: "float", nullable: false),
                    Nkthuettdbgc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkthuephikdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkthuephiktt = table.Column<double>(type: "float", nullable: false),
                    Nkthuephikgc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nktienkdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nktienktt = table.Column<double>(type: "float", nullable: false),
                    Nktienkgc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkchiphitcdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkchiphitctt = table.Column<double>(type: "float", nullable: false),
                    Nkchiphitcgc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkchiphibhdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkchiphibhtt = table.Column<double>(type: "float", nullable: false),
                    Nkchiphibhgc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkchiphiqldvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkchiphiqltt = table.Column<double>(type: "float", nullable: false),
                    Nkchiphiqlgc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkgiathanh1spdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkgiathanh1sptt = table.Column<double>(type: "float", nullable: false),
                    Nkgiathanh1spgc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkloinhuandkdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkloinhuandktt = table.Column<double>(type: "float", nullable: false),
                    Nkloinhuandkgc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkthuegtgtkdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkthuegtgtktt = table.Column<double>(type: "float", nullable: false),
                    Nkthuegtgtkgc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkgiabandkdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkgiabandktt = table.Column<double>(type: "float", nullable: false),
                    Nkgiabandkgc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkgtgiamuack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkgtthuenk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkgtthuettdb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkgtthuephik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkgttienk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkgtchiphitc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkgtchiphibh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkgtchiphiql = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkgtloinhuandk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkgtthuegtgt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nkgtgiabandk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxdonvisxkd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxqcpc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxchiphinvldvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxchiphinvlsl = table.Column<double>(type: "float", nullable: false),
                    Sxchiphinvldg = table.Column<double>(type: "float", nullable: false),
                    Sxchiphincdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxchiphincsl = table.Column<double>(type: "float", nullable: false),
                    Sxchiphincdg = table.Column<double>(type: "float", nullable: false),
                    Sxchiphinvpxdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxchiphinvpxsl = table.Column<double>(type: "float", nullable: false),
                    Sxchiphinvpxdg = table.Column<double>(type: "float", nullable: false),
                    Sxchiphivldvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxchiphivlsl = table.Column<double>(type: "float", nullable: false),
                    Sxchiphivldg = table.Column<double>(type: "float", nullable: false),
                    Sxchiphidcsxdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxchiphidcsxsl = table.Column<double>(type: "float", nullable: false),
                    Sxchiphidcsxdg = table.Column<double>(type: "float", nullable: false),
                    Sxchiphikhtscddvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxchiphikhtscdsl = table.Column<double>(type: "float", nullable: false),
                    Sxchiphikhtscddg = table.Column<double>(type: "float", nullable: false),
                    Sxchiphidvmndvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxchiphidvmnsl = table.Column<double>(type: "float", nullable: false),
                    Sxchiphidvmndg = table.Column<double>(type: "float", nullable: false),
                    Sxchiphitienkdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxchiphitienksl = table.Column<double>(type: "float", nullable: false),
                    Sxchiphitienkdg = table.Column<double>(type: "float", nullable: false),
                    Sxchiphibhdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxchiphibhsl = table.Column<double>(type: "float", nullable: false),
                    Sxchiphibhdg = table.Column<double>(type: "float", nullable: false),
                    Sxchiphiqldndvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxchiphiqldnsl = table.Column<double>(type: "float", nullable: false),
                    Sxchiphiqldndg = table.Column<double>(type: "float", nullable: false),
                    Sxchiphitcdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxchiphitcsl = table.Column<double>(type: "float", nullable: false),
                    Sxchiphitcdg = table.Column<double>(type: "float", nullable: false),
                    Sxloinhuandkdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxloinhuandksl = table.Column<double>(type: "float", nullable: false),
                    Sxloinhuandkdg = table.Column<double>(type: "float", nullable: false),
                    Sxgiabanctdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxgiabanctsl = table.Column<double>(type: "float", nullable: false),
                    Sxgiabanctdg = table.Column<double>(type: "float", nullable: false),
                    Sxthuettdbdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxthuettdbsl = table.Column<double>(type: "float", nullable: false),
                    Sxthuettdbdg = table.Column<double>(type: "float", nullable: false),
                    Sxthuegtgtdvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxthuegtgtsl = table.Column<double>(type: "float", nullable: false),
                    Sxthuegtgtdg = table.Column<double>(type: "float", nullable: false),
                    Sxgiabandvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxgiabansl = table.Column<double>(type: "float", nullable: false),
                    Sxgiabandg = table.Column<double>(type: "float", nullable: false),
                    Sxgtchiphisx = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxgtchiphibh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxgtchiphiqldn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxgtchiphitc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxgtloinhuandk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxgtthuettdb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxgtthuegtgt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sxgtgiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkDkgCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkDkgCtDf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maxa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenhh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quycach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkDkgCtDf", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaCatSan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaCatSan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaCatSanCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaCatSanCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDatSanLap",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaDatSanLap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDatSanLapCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaDatSanLapCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDaXayDung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaDaXayDung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDaXayDungCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaDaXayDungCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDvCang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaDvCang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDvCangCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaDvCangCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDvCh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaDvCh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDvChCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaDvChCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDvDlBb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaDvDlBb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDvDlBbCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaDvDlBbCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDvHdTm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaDvHdTm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDvHdTmCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaDvHdTmCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDvLt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macskd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dtll = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ptnguyennhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chinhsachkm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Macqcq1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaDvLt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaDvLtCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macskd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaDvLtCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaEtanol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaEtanol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaEtanolCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaEtanolCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaGiay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaGiay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaGiayCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaGiayCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaHpLx",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaHpLx", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaHpLxCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaHpLxCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaKcbTn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaKcbTn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaKcbTnCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaKcbTnCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaOtoNkSx",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaOtoNkSx", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaOtoNkSxCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaOtoNkSxCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaSach",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaSach", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaSachCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaSachCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaTaCn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaTaCn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaTaCnCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaTaCnCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaThan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaThan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaThanCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaThanCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaVeTqKdl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaVeTqKdl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaVeTqKdlCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaVeTqKdlCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaVlXd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaVlXd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaVlXdCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaVlXdCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaVlXdDm",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaVlXdDm", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaVtXb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaVtXb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaVtXbCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaVtXbCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaVtXk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaVtXk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaVtXkCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaVtXkCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaVtXtx",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaVtXtx", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaVtXtxCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaVtXtxCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaXeMayNkSx",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaXeMayNkSx", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaXeMayNkSxCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaXeMayNkSxCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaXmTxd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaXmTxd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGiaXmTxdCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaXmTxdCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ytcauthanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thydggadgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkGsCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGsCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkMhBog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manghe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theoqd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycvlk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dtll = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pldn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ptnguyennhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chinhsachkm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Congbo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thaotac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lichsu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_h = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_h = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_t = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_t = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Macqcq_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaynhan_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sohsnhan_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoichuyen_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaychuyen_ad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lydo_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai_ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hoso = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkMhBog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KkMhBogCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madiaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenhh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quycach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plhh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giakk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkMhBogCt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Register",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maxa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diachi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diadanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chucdanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoiky = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Noidknopthue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tailieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giayphepkd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Settingdvvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vtxk = table.Column<double>(type: "float", nullable: false),
                    Vtxb = table.Column<double>(type: "float", nullable: false),
                    Vtxtx = table.Column<double>(type: "float", nullable: false),
                    Vtch = table.Column<double>(type: "float", nullable: false),
                    Loaihinhhd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Xangdau = table.Column<double>(type: "float", nullable: false),
                    Dien = table.Column<double>(type: "float", nullable: false),
                    Khidau = table.Column<double>(type: "float", nullable: false),
                    Phan = table.Column<double>(type: "float", nullable: false),
                    Thuocbvtv = table.Column<double>(type: "float", nullable: false),
                    Vacxingsgc = table.Column<double>(type: "float", nullable: false),
                    Muoi = table.Column<double>(type: "float", nullable: false),
                    Suate6t = table.Column<double>(type: "float", nullable: false),
                    Duong = table.Column<double>(type: "float", nullable: false),
                    Thocgao = table.Column<double>(type: "float", nullable: false),
                    Thuocpcb = table.Column<double>(type: "float", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diachi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttlienhe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emailql = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emailqt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Songaylv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvhienthi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcqhienthi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chucvuky = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chucvukythay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nguoiky = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diadanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maxa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sadmin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Permission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emailxt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ttnguoitao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Manhomtk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chucnang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Solandn = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "CompanyLvCc");

            migrationBuilder.DropTable(
                name: "DiaBanHd");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "DmDvt");

            migrationBuilder.DropTable(
                name: "DmHinhThucThanhToan");

            migrationBuilder.DropTable(
                name: "DmLoaiGia");

            migrationBuilder.DropTable(
                name: "DmNganhKd");

            migrationBuilder.DropTable(
                name: "DmNgheKd");

            migrationBuilder.DropTable(
                name: "DsDonViTdg");

            migrationBuilder.DropTable(
                name: "DsNhomTaiKhoan");

            migrationBuilder.DropTable(
                name: "DsThamDinhVien");

            migrationBuilder.DropTable(
                name: "DsVanPhong");

            migrationBuilder.DropTable(
                name: "DsXaPhuong");

            migrationBuilder.DropTable(
                name: "GeneralConfigs");

            migrationBuilder.DropTable(
                name: "GiaBanNhaTaiDinhCu");

            migrationBuilder.DropTable(
                name: "GiaCuocVanChuyen");

            migrationBuilder.DropTable(
                name: "GiaCuocVanChuyenCt");

            migrationBuilder.DropTable(
                name: "GiaDatDiaBan");

            migrationBuilder.DropTable(
                name: "GiaDatDiaBanCt");

            migrationBuilder.DropTable(
                name: "GiaDatDiaBanTt");

            migrationBuilder.DropTable(
                name: "GiaDatDuAn");

            migrationBuilder.DropTable(
                name: "GiaDatDuAnDm");

            migrationBuilder.DropTable(
                name: "GiaDatPhanLoai");

            migrationBuilder.DropTable(
                name: "GiaDatPhanLoaiCt");

            migrationBuilder.DropTable(
                name: "GiaDatPhanLoaiDm");

            migrationBuilder.DropTable(
                name: "GiaDatThiTruong");

            migrationBuilder.DropTable(
                name: "GiaDatThiTruongCt");

            migrationBuilder.DropTable(
                name: "GiaDauGiaDat");

            migrationBuilder.DropTable(
                name: "GiaDauGiaDatCt");

            migrationBuilder.DropTable(
                name: "GiaDauGiaDatTs");

            migrationBuilder.DropTable(
                name: "GiaDauGiaDatTsCt");

            migrationBuilder.DropTable(
                name: "GiaDvGdDt");

            migrationBuilder.DropTable(
                name: "GiaDvGdDtCt");

            migrationBuilder.DropTable(
                name: "GiaDvGdDtDm");

            migrationBuilder.DropTable(
                name: "GiaDvKcb");

            migrationBuilder.DropTable(
                name: "GiaDvKcbCt");

            migrationBuilder.DropTable(
                name: "GiaDvKcbDm");

            migrationBuilder.DropTable(
                name: "GiaGdBatDongSan");

            migrationBuilder.DropTable(
                name: "GiaHhDvCn");

            migrationBuilder.DropTable(
                name: "GiaHhDvCnCt");

            migrationBuilder.DropTable(
                name: "GiaHhDvCnDm");

            migrationBuilder.DropTable(
                name: "GiaKhungGiaDat");

            migrationBuilder.DropTable(
                name: "GiaLpTbNha");

            migrationBuilder.DropTable(
                name: "GiaLpTbNhaCtClCl");

            migrationBuilder.DropTable(
                name: "GiaLpTbNhaCtXdm");

            migrationBuilder.DropTable(
                name: "GiaMuaTaiSan");

            migrationBuilder.DropTable(
                name: "GiaNuocSh");

            migrationBuilder.DropTable(
                name: "GiaNuocShCt");

            migrationBuilder.DropTable(
                name: "GiaNuocShCtDf");

            migrationBuilder.DropTable(
                name: "GiaNuocShDm");

            migrationBuilder.DropTable(
                name: "GiaPhiChuyenGia");

            migrationBuilder.DropTable(
                name: "GiaPhiChuyenGiaCt");

            migrationBuilder.DropTable(
                name: "GiaPhiChuyenGiaDm");

            migrationBuilder.DropTable(
                name: "GiaPhiChuyenGiaNhom");

            migrationBuilder.DropTable(
                name: "GiaRung");

            migrationBuilder.DropTable(
                name: "GiaRungCt");

            migrationBuilder.DropTable(
                name: "GiaSpDvCi");

            migrationBuilder.DropTable(
                name: "GiaSpDvCiCt");

            migrationBuilder.DropTable(
                name: "GiaSpDvCiDm");

            migrationBuilder.DropTable(
                name: "GiaSpDvCuThe");

            migrationBuilder.DropTable(
                name: "GiaSpDvCuTheCt");

            migrationBuilder.DropTable(
                name: "GiaSpDvKhungGia");

            migrationBuilder.DropTable(
                name: "GiaSpDvKhungGiaCt");

            migrationBuilder.DropTable(
                name: "GiaSpDvKhungGiaDm");

            migrationBuilder.DropTable(
                name: "GiaSpDvToiDa");

            migrationBuilder.DropTable(
                name: "GiaSpDvToiDaCt");

            migrationBuilder.DropTable(
                name: "GiaSpDvToiDaDm");

            migrationBuilder.DropTable(
                name: "GiaTaiSanCong");

            migrationBuilder.DropTable(
                name: "GiaTaiSanCongCt");

            migrationBuilder.DropTable(
                name: "GiaTaiSanCongDm");

            migrationBuilder.DropTable(
                name: "GiaThiTruong");

            migrationBuilder.DropTable(
                name: "GiaThiTruongCt");

            migrationBuilder.DropTable(
                name: "GiaThiTruongDm");

            migrationBuilder.DropTable(
                name: "GiaThiTruongTt");

            migrationBuilder.DropTable(
                name: "GiaThueMuaNhaXh");

            migrationBuilder.DropTable(
                name: "GiaThueMuaNhaXhCt");

            migrationBuilder.DropTable(
                name: "GiaThueMuaNhaXhDm");

            migrationBuilder.DropTable(
                name: "GiaThueNhaCongVu");

            migrationBuilder.DropTable(
                name: "GiaThueTaiNguyen");

            migrationBuilder.DropTable(
                name: "GiaThueTaiNguyenCt");

            migrationBuilder.DropTable(
                name: "GiaThueTaiNguyenDm");

            migrationBuilder.DropTable(
                name: "GiaThueTaiNguyenNhom");

            migrationBuilder.DropTable(
                name: "GiaTroGiaTroCuoc");

            migrationBuilder.DropTable(
                name: "GiaTroGiaTroCuocCt");

            migrationBuilder.DropTable(
                name: "GiaTroGiaTroCuocDm");

            migrationBuilder.DropTable(
                name: "GiaVangNgoaiTe");

            migrationBuilder.DropTable(
                name: "GiaVangNgoaiTeCt");

            migrationBuilder.DropTable(
                name: "GiaVangNgoaiTeDm");

            migrationBuilder.DropTable(
                name: "KeKhaiGia");

            migrationBuilder.DropTable(
                name: "KeKhaiGiaCt");

            migrationBuilder.DropTable(
                name: "KkCuocVcHk");

            migrationBuilder.DropTable(
                name: "KkCuocVcHkCt");

            migrationBuilder.DropTable(
                name: "KkDkg");

            migrationBuilder.DropTable(
                name: "KkDkgCt");

            migrationBuilder.DropTable(
                name: "KkDkgCtDf");

            migrationBuilder.DropTable(
                name: "KkGiaCatSan");

            migrationBuilder.DropTable(
                name: "KkGiaCatSanCt");

            migrationBuilder.DropTable(
                name: "KkGiaDatSanLap");

            migrationBuilder.DropTable(
                name: "KkGiaDatSanLapCt");

            migrationBuilder.DropTable(
                name: "KkGiaDaXayDung");

            migrationBuilder.DropTable(
                name: "KkGiaDaXayDungCt");

            migrationBuilder.DropTable(
                name: "KkGiaDvCang");

            migrationBuilder.DropTable(
                name: "KkGiaDvCangCt");

            migrationBuilder.DropTable(
                name: "KkGiaDvCh");

            migrationBuilder.DropTable(
                name: "KkGiaDvChCt");

            migrationBuilder.DropTable(
                name: "KkGiaDvDlBb");

            migrationBuilder.DropTable(
                name: "KkGiaDvDlBbCt");

            migrationBuilder.DropTable(
                name: "KkGiaDvHdTm");

            migrationBuilder.DropTable(
                name: "KkGiaDvHdTmCt");

            migrationBuilder.DropTable(
                name: "KkGiaDvLt");

            migrationBuilder.DropTable(
                name: "KkGiaDvLtCt");

            migrationBuilder.DropTable(
                name: "KkGiaEtanol");

            migrationBuilder.DropTable(
                name: "KkGiaEtanolCt");

            migrationBuilder.DropTable(
                name: "KkGiaGiay");

            migrationBuilder.DropTable(
                name: "KkGiaGiayCt");

            migrationBuilder.DropTable(
                name: "KkGiaHpLx");

            migrationBuilder.DropTable(
                name: "KkGiaHpLxCt");

            migrationBuilder.DropTable(
                name: "KkGiaKcbTn");

            migrationBuilder.DropTable(
                name: "KkGiaKcbTnCt");

            migrationBuilder.DropTable(
                name: "KkGiaOtoNkSx");

            migrationBuilder.DropTable(
                name: "KkGiaOtoNkSxCt");

            migrationBuilder.DropTable(
                name: "KkGiaSach");

            migrationBuilder.DropTable(
                name: "KkGiaSachCt");

            migrationBuilder.DropTable(
                name: "KkGiaTaCn");

            migrationBuilder.DropTable(
                name: "KkGiaTaCnCt");

            migrationBuilder.DropTable(
                name: "KkGiaThan");

            migrationBuilder.DropTable(
                name: "KkGiaThanCt");

            migrationBuilder.DropTable(
                name: "KkGiaVeTqKdl");

            migrationBuilder.DropTable(
                name: "KkGiaVeTqKdlCt");

            migrationBuilder.DropTable(
                name: "KkGiaVlXd");

            migrationBuilder.DropTable(
                name: "KkGiaVlXdCt");

            migrationBuilder.DropTable(
                name: "KkGiaVlXdDm");

            migrationBuilder.DropTable(
                name: "KkGiaVtXb");

            migrationBuilder.DropTable(
                name: "KkGiaVtXbCt");

            migrationBuilder.DropTable(
                name: "KkGiaVtXk");

            migrationBuilder.DropTable(
                name: "KkGiaVtXkCt");

            migrationBuilder.DropTable(
                name: "KkGiaVtXtx");

            migrationBuilder.DropTable(
                name: "KkGiaVtXtxCt");

            migrationBuilder.DropTable(
                name: "KkGiaXeMayNkSx");

            migrationBuilder.DropTable(
                name: "KkGiaXeMayNkSxCt");

            migrationBuilder.DropTable(
                name: "KkGiaXmTxd");

            migrationBuilder.DropTable(
                name: "KkGiaXmTxdCt");

            migrationBuilder.DropTable(
                name: "KkGs");

            migrationBuilder.DropTable(
                name: "KkGsCt");

            migrationBuilder.DropTable(
                name: "KkMhBog");

            migrationBuilder.DropTable(
                name: "KkMhBogCt");

            migrationBuilder.DropTable(
                name: "Register");

            migrationBuilder.DropTable(
                name: "Towns");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
