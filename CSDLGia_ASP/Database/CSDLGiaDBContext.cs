using CSDLGia_ASP.Models;
using CSDLGia_ASP.Models.Manages.KeKhaiGia;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Manages.KeKhaiDkg;
using CSDLGia_ASP.Models.Systems;
using Microsoft.EntityFrameworkCore;

namespace CSDLGia_ASP.Database
{
    public class CSDLGiaDBContext : DbContext
    {
        public CSDLGiaDBContext(DbContextOptions<CSDLGiaDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblPhanQuyen>().HasKey(e => new
            {
                e.MaChucNang,
                e.TenDangNhap,
            });
        }

        public DbSet<tblHeThong> tblHeThong { get; set; }

        public DbSet<tblDMChucNang> tblDMChucNang { get; set; }

        public DbSet<tblPhanQuyen> tblPhanQuyen { get; set; }


        //Manages:
        //Kê khai giá
        public DbSet<KkGia> KkGia { get; set; }
        public DbSet<KkGiaCt> KkGiaCt { get; set; }
        public DbSet<KkCuocVcHk> KkCuocVcHk { get; set; }
        public DbSet<KkCuocVcHkCt> KkCuocVcHkCt { get; set; }
        public DbSet<KkGiaCatSan> KkGiaCatSan { get; set; }
        public DbSet<KkGiaCatSanCt> KkGiaCatSanCt { get; set; }
        public DbSet<KkGiaDatSanLap> KkGiaDatSanLap { get; set; }
        public DbSet<KkGiaDatSanLapCt> KkGiaDatSanLapCt { get; set; }
        public DbSet<KkGiaDaXayDung> KkGiaDaXayDung { get; set; }
        public DbSet<KkGiaDaXayDungCt> KkGiaDaXayDungCt { get; set; }
        public DbSet<KkGiaDvCang> KkGiaDvCang { get; set; }
        public DbSet<KkGiaDvCangCt> KkGiaDvCangCt { get; set; }
        public DbSet<KkGiaDvCh> KkGiaDvCh { get; set; }
        public DbSet<KkGiaDvChCt> KkGiaDvChCt { get; set; }
        public DbSet<KkGiaDvDlBb> KkGiaDvDlBb { get; set; }
        public DbSet<KkGiaDvDlBbCt> KkGiaDvDlBbCt { get; set; }
        public DbSet<KkGiaDvHdTm> KkGiaDvHdTm { get; set; }
        public DbSet<KkGiaDvHdTmCt> KkGiaDvHdTmCt { get; set; }
        public DbSet<KkGiaDvLt> KkGiaDvLt { get; set; }
        public DbSet<KkGiaDvLtCt> KkGiaDvLtCt { get; set; }
        public DbSet<KkGiaEtanol> KkGiaEtanol { get; set; }
        public DbSet<KkGiaEtanolCt> KkGiaEtanolCt { get; set; }
        public DbSet<KkGiaGiay> KkGiaGiay { get; set; }
        public DbSet<KkGiaGiayCt> KkGiaGiayCt { get; set; }
        public DbSet<KkGiaHpLx> KkGiaHpLx { get; set; }
        public DbSet<KkGiaHpLxCt> KkGiaHpLxCt { get; set; }
        public DbSet<KkGiaKcbTn> KkGiaKcbTn { get; set; }
        public DbSet<KkGiaKcbTnCt> KkGiaKcbTnCt { get; set; }
        public DbSet<KkGiaOtoNkSx> KkGiaOtoNkSx { get; set; }
        public DbSet<KkGiaOtoNkSxCt> KkGiaOtoNkSxCt { get; set; }
        public DbSet<KkGiaSach> KkGiaSach { get; set; }
        public DbSet<KkGiaSachCt> KkGiaSachCt { get; set; }
        public DbSet<KkGiaTaCn> KkGiaTaCn { get; set; }
        public DbSet<KkGiaTaCnCt> KkGiaTaCnCt { get; set; }
        public DbSet<KkGiaThan> KkGiaThan { get; set; }
        public DbSet<KkGiaThanCt> KkGiaThanCt { get; set; }
        public DbSet<KkGiaVeTqKdl> KkGiaVeTqKdl { get; set; }
        public DbSet<KkGiaVeTqKdlCt> KkGiaVeTqKdlCt { get; set; }
        public DbSet<KkGiaVlXd> KkGiaVlXd { get; set; }
        public DbSet<KkGiaVlXdCt> KkGiaVlXdCt { get; set; }
        public DbSet<KkGiaVlXdDm> KkGiaVlXdDm { get; set; }
        public DbSet<KkGiaVtXb> KkGiaVtXb { get; set; }
        public DbSet<KkGiaVtXbCt> KkGiaVtXbCt { get; set; }
        public DbSet<KkGiaVtXk> KkGiaVtXk { get; set; }
        public DbSet<KkGiaVtXkCt> KkGiaVtXkCt { get; set; }
        public DbSet<KkGiaVtXtx> KkGiaVtXtx { get; set; }
        public DbSet<KkGiaVtXtxCt> KkGiaVtXtxCt { get; set; }
        public DbSet<KkGiaXeMayNkSx> KkGiaXeMayNkSx { get; set; }
        public DbSet<KkGiaXeMayNkSxCt> KkGiaXeMayNkSxCt { get; set; }
        public DbSet<KkGiaXmTxd> KkGiaXmTxd { get; set; }
        public DbSet<KkGiaXmTxdCt> KkGiaXmTxdCt { get; set; }
        public DbSet<KkGs> KkGs { get; set; }
        public DbSet<KkGsCt> KkGsCt { get; set; }

        //Kê khai đăng ký giá
        public DbSet<KkDkg> KkDkg { get; set; }
        public DbSet<KkDkgCt> KkDkgCt { get; set; }
        public DbSet<KkDkgCtDf> KkDkgCtDf { get; set; }
        public DbSet<KkMhBog> KkMhBog { get; set; }
        public DbSet<KkMhBogCt> KkMhBogCt { get; set; }

        //Định giá
        public DbSet<GiaBanNhaTaiDinhCu> GiaBanNhaTaiDinhCu { get; set; }
        public DbSet<GiaCuocVanChuyen> GiaCuocVanChuyen { get; set; }
        public DbSet<GiaCuocVanChuyenCt> GiaCuocVanChuyenCt { get; set; }
        public DbSet<GiaDatDiaBan> GiaDatDiaBan { get; set; }
        public DbSet<GiaDatDiaBanCt> GiaDatDiaBanCt { get; set; }
        public DbSet<GiaDatDiaBanTt> GiaDatDiaBanTt { get; set; }
        public DbSet<GiaDatDuAn> GiaDatDuAn { get; set; }
        public DbSet<GiaDatDuAnDm> GiaDatDuAnDm { get; set; }
        public DbSet<GiaDatPhanLoai> GiaDatPhanLoai { get; set; }
        public DbSet<GiaDatPhanLoaiCt> GiaDatPhanLoaiCt { get; set; }
        public DbSet<GiaDatPhanLoaiDm> GiaDatPhanLoaiDm { get; set; }
        public DbSet<GiaDatThiTruong> GiaDatThiTruong { get; set; }
        public DbSet<GiaDatThiTruongCt> GiaDatThiTruongCt { get; set; }
        public DbSet<GiaDauGiaDat> GiaDauGiaDat { get; set; }
        public DbSet<GiaDauGiaDatCt> GiaDauGiaDatCt { get; set; }
        public DbSet<GiaDauGiaDatTs> GiaDauGiaDatTs { get; set; }
        public DbSet<GiaDauGiaDatTsCt> GiaDauGiaDatTsCt { get; set; }
        public DbSet<GiaDvGdDt> GiaDvGdDt { get; set; }
        public DbSet<GiaDvGdDtCt> GiaDvGdDtCt { get; set; }
        public DbSet<GiaDvGdDtDm> GiaDvGdDtDm { get; set; }
        public DbSet<GiaDvKcb> GiaDvKcb { get; set; }
        public DbSet<GiaDvKcbCt> GiaDvKcbCt { get; set; }
        public DbSet<GiaDvKcbDm> GiaDvKcbDm { get; set; }
        public DbSet<GiaGdBatDongSan> GiaGdBatDongSan { get; set; }
        public DbSet<GiaHhDvCn> GiaHhDvCn { get; set; }
        public DbSet<GiaHhDvCnCt> GiaHhDvCnCt { get; set; }
        public DbSet<GiaHhDvCnDm> GiaHhDvCnDm { get; set; }
        public DbSet<GiaKhungGiaDat> GiaKhungGiaDat { get; set; }
        public DbSet<GiaLpTbNha> GiaLpTbNha { get; set; }
        public DbSet<GiaLpTbNhaCtClCl> GiaLpTbNhaCtClCl { get; set; }
        public DbSet<GiaLpTbNhaCtXdm> GiaLpTbNhaCtXdm { get; set; }
        public DbSet<GiaMuaTaiSan> GiaMuaTaiSan { get; set; }
        public DbSet<GiaNuocSh> GiaNuocSh { get; set; }
        public DbSet<GiaNuocShCt> GiaNuocShCt { get; set; }
        public DbSet<GiaNuocShCtDf> GiaNuocShCtDf { get; set; }
        public DbSet<GiaNuocShDm> GiaNuocShDm { get; set; }
        public DbSet<GiaPhiChuyenGia> GiaPhiChuyenGia { get; set; }
        public DbSet<GiaPhiChuyenGiaCt> GiaPhiChuyenGiaCt { get; set; }
        public DbSet<GiaPhiChuyenGiaDm> GiaPhiChuyenGiaDm { get; set; }
        public DbSet<GiaPhiChuyenGiaNhom> GiaPhiChuyenGiaNhom { get; set; }
        public DbSet<GiaRung> GiaRung { get; set; }
        public DbSet<GiaRungCt> GiaRungCt { get; set; }
        public DbSet<GiaSpDvCi> GiaSpDvCi { get; set; }
        public DbSet<GiaSpDvCiCt> GiaSpDvCiCt { get; set; }
        public DbSet<GiaSpDvCiDm> GiaSpDvCiDm { get; set; }
        public DbSet<GiaSpDvCuThe> GiaSpDvCuThe { get; set; }
        public DbSet<GiaSpDvCuTheCt> GiaSpDvCuTheCt { get; set; }
        public DbSet<GiaSpDvKhungGia> GiaSpDvKhungGia { get; set; }
        public DbSet<GiaSpDvKhungGiaCt> GiaSpDvKhungGiaCt { get; set; }
        public DbSet<GiaSpDvKhungGiaDm> GiaSpDvKhungGiaDm { get; set; }
        public DbSet<GiaSpDvToiDa> GiaSpDvToiDa { get; set; }
        public DbSet<GiaSpDvToiDaCt> GiaSpDvToiDaCt { get; set; }
        public DbSet<GiaSpDvToiDaDm> GiaSpDvToiDaDm { get; set; }
        public DbSet<GiaTaiSanCong> GiaTaiSanCong { get; set; }
        public DbSet<GiaTaiSanCongCt> GiaTaiSanCongCt { get; set; }
        public DbSet<GiaTaiSanCongDm> GiaTaiSanCongDm { get; set; }
        public DbSet<GiaThiTruong> GiaThiTruong { get; set; }
        public DbSet<GiaThiTruongCt> GiaThiTruongCt { get; set; }
        public DbSet<GiaThiTruongDm> GiaThiTruongDm { get; set; }
        public DbSet<GiaThiTruongTt> GiaThiTruongTt { get; set; }
        public DbSet<GiaThueMuaNhaXh> GiaThueMuaNhaXh { get; set; }
        public DbSet<GiaThueMuaNhaXhCt> GiaThueMuaNhaXhCt { get; set; }
        public DbSet<GiaThueMuaNhaXhDm> GiaThueMuaNhaXhDm { get; set; }
        public DbSet<GiaThueNhaCongVu> GiaThueNhaCongVu { get; set; }
        public DbSet<GiaThueTaiNguyen> GiaThueTaiNguyen { get; set; }
        public DbSet<GiaThueTaiNguyenCt> GiaThueTaiNguyenCt { get; set; }
        public DbSet<GiaThueTaiNguyenDm> GiaThueTaiNguyenDm { get; set; }
        public DbSet<GiaThueTaiNguyenNhom> GiaThueTaiNguyenNhom { get; set; }
        public DbSet<GiaTroGiaTroCuoc> GiaTroGiaTroCuoc { get; set; }
        public DbSet<GiaTroGiaTroCuocCt> GiaTroGiaTroCuocCt { get; set; }
        public DbSet<GiaTroGiaTroCuocDm> GiaTroGiaTroCuocDm { get; set; }
        public DbSet<GiaVangNgoaiTe> GiaVangNgoaiTe { get; set; }
        public DbSet<GiaVangNgoaiTeCt> GiaVangNgoaiTeCt { get; set; }
        public DbSet<GiaVangNgoaiTeDm> GiaVangNgoaiTeDm { get; set; }

        //Systems:
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyLvCc> CompanyLvCc { get; set; }
        public DbSet<DiaBanHd> DiaBanHd { get; set; }
        public DbSet<Districts> Districts { get; set; }
        public DbSet<DmDvt> DmDvt { get; set; }
        public DbSet<DmHinhThucThanhToan> DmHinhThucThanhToan { get; set; }
        public DbSet<DmLoaiGia> DmLoaiGia { get; set; }
        public DbSet<DmNganhKd> DmNganhKd { get; set; }
        public DbSet<DmNgheKd> DmNgheKd { get; set; }
        public DbSet<DsDiaBan> DsDiaBan { get; set; }
        public DbSet<DsDonVi> DsDonVi { get; set; }
        public DbSet<DsDonViTdg> DsDonViTdg { get; set; }
        public DbSet<DsNhomTaiKhoan> DsNhomTaiKhoan { get; set; }
        public DbSet<DsThamDinhVien> DsThamDinhVien { get; set; }
        public DbSet<DsVanPhong> DsVanPhong { get; set; }
        public DbSet<DsXaPhuong> DsXaPhuong { get; set; }
        public DbSet<GeneralConfigs> GeneralConfigs { get; set; }
        public DbSet<Register> Register { get; set; }
        public DbSet<Towns> Towns { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<GroupPermissions> GroupPermissions { get; set; }
    }
}
