using CSDLGia_ASP.Models;
using CSDLGia_ASP.Models.Manages.KeKhaiGia;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Manages.KeKhaiDkg;
using CSDLGia_ASP.Models.Manages.VbQlNn;
using CSDLGia_ASP.Models.Manages.ThamDinhGia;
using CSDLGia_ASP.Models.Manages.ChiSoGiaTd;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Models.Systems.API;
using Microsoft.EntityFrameworkCore;
using CSDLGia_ASP.Models.Temp.TempSystems;
using CSDLGia_ASP.Models.Temp.TempManages.TempKeKhaiGia;
using System.Collections.Generic;

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
        public DbSet<KkCuocVcHkCt> KkCuocVcHkCt { get; set; }
        public DbSet<KkGiaCatSanCt> KkGiaCatSanCt { get; set; }
        public DbSet<KkGiaDatSanLapCt> KkGiaDatSanLapCt { get; set; }
        public DbSet<KkGiaDaXayDungCt> KkGiaDaXayDungCt { get; set; }
        public DbSet<KkGiaDvCangCt> KkGiaDvCangCt { get; set; }
        public DbSet<KkGiaDvChCt> KkGiaDvChCt { get; set; }
        public DbSet<KkGiaDvDlBbCt> KkGiaDvDlBbCt { get; set; }
        public DbSet<KkGiaDvHdTmCt> KkGiaDvHdTmCt { get; set; }
        public DbSet<KkGiaDvLtCskd> KkGiaDvLtCskd { get; set; }
        public DbSet<KkGiaDvLtCt> KkGiaDvLtCt { get; set; }
        public DbSet<KkGiaEtanolCt> KkGiaEtanolCt { get; set; }
        public DbSet<KkGiaGiayCt> KkGiaGiayCt { get; set; }
        public DbSet<KkGiaHpLxCt> KkGiaHpLxCt { get; set; }
        public DbSet<KkGiaKcbTnCt> KkGiaKcbTnCt { get; set; }
        public DbSet<KkGiaOtoNkSxCt> KkGiaOtoNkSxCt { get; set; }
        public DbSet<KkGiaSachCt> KkGiaSachCt { get; set; }
        public DbSet<KkGiaTaCnCt> KkGiaTaCnCt { get; set; }
        public DbSet<KkGiaThanCt> KkGiaThanCt { get; set; }
        public DbSet<KkGiaVeTqKdlCt> KkGiaVeTqKdlCt { get; set; }
        public DbSet<KkGiaVlXd> KkGiaVlXd { get; set; }
        public DbSet<KkGiaVlXdCt> KkGiaVlXdCt { get; set; }
        public DbSet<KkGiaVlXdDm> KkGiaVlXdDm { get; set; }
        public DbSet<KkGiaVtXbCt> KkGiaVtXbCt { get; set; }
        public DbSet<KkGiaVtXkCt> KkGiaVtXkCt { get; set; }
        public DbSet<KkGiaVtXtxCt> KkGiaVtXtxCt { get; set; }
        public DbSet<KkGiaXeMayNkSxCt> KkGiaXeMayNkSxCt { get; set; }
        public DbSet<KkGiaXmTxdCt> KkGiaXmTxdCt { get; set; }
        public DbSet<KkGsCt> KkGsCt { get; set; }
        public DbSet<KkGiaSieuThiCt> KkGiaSieuThiCt { get; set; }

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
        public DbSet<GiaDatPhanLoaiExcel> GiaDatPhanLoaiExcel { get; set; }

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
        public DbSet<GiaDvKcbNhom> GiaDvKcbNhom { get; set; }
        
        public DbSet<GiaGocVlxdTh> GiaGocVlxdTh { get; set; }
        public DbSet<GiaHhDvCn> GiaHhDvCn { get; set; }
        public DbSet<GiaHhDvCnCt> GiaHhDvCnCt { get; set; }
        public DbSet<GiaHhDvCnDm> GiaHhDvCnDm { get; set; }
        public DbSet<GiaHhDvkNhom> GiaHhDvkNhom { get; set; }
        public DbSet<GiaHhDvkDm> GiaHhDvkDm { get; set; }
        public DbSet<GiaHhDvkDmDv> GiaHhDvkDmDv { get; set; }
        public DbSet<GiaHhDvk> GiaHhDvk { get; set; }
        public DbSet<GiaHhDvkCt> GiaHhDvkCt { get; set; }
        public DbSet<GiaHhDvkTh> GiaHhDvkTh { get; set; }
        public DbSet<GiaHhDvkCtTh> GiaHhDvkCtTh { get; set; }
        public DbSet<GiaKhungGiaDat> GiaKhungGiaDat { get; set; }
        public DbSet<GiaKhungGiaDatCt> GiaKhungGiaDatCt { get; set; }
        public DbSet<GiaLpTbNha> GiaLpTbNha { get; set; }
        public DbSet<GiaLpTbNhaCtClCl> GiaLpTbNhaCtClCl { get; set; }
        public DbSet<GiaLpTbNhaCtXdm> GiaLpTbNhaCtXdm { get; set; }
        public DbSet<GiaMuaTaiSan> GiaMuaTaiSan { get; set; }

        public DbSet<GiaNuocSh> GiaNuocSh { get; set; }
        public DbSet<GiaNuocShCt> GiaNuocShCt { get; set; }
        public DbSet<GiaNuocShCtDf> GiaNuocShCtDf { get; set; }
        public DbSet<GiaNuocShDmVung> GiaNuocShDmVung { get; set; }
        public DbSet<GiaNuocShDmKhung> GiaNuocShDmKhung { get; set; }

        public DbSet<GiaPhiChuyenGia> GiaPhiChuyenGia { get; set; }
        public DbSet<GiaPhiChuyenGiaCt> GiaPhiChuyenGiaCt { get; set; }
        public DbSet<GiaPhiChuyenGiaDm> GiaPhiChuyenGiaDm { get; set; }
        public DbSet<GiaPhiChuyenGiaNhom> GiaPhiChuyenGiaNhom { get; set; }
        public DbSet<GiaPhiLePhi> GiaPhiLePhi { get; set; }
        public DbSet<GiaPhiLePhiCt> GiaPhiLePhiCt { get; set; }
        public DbSet<GiaPhiLePhiDm> GiaPhiLePhiDm { get; set; }
        public DbSet<GiaRung> GiaRung { get; set; }
        public DbSet<GiaRungCt> GiaRungCt { get; set; }
        public DbSet<GiaRungDm> GiaRungDm { get; set; }
        public DbSet<GiaSpDvCi> GiaSpDvCi { get; set; }
        public DbSet<GiaSpDvCiCt> GiaSpDvCiCt { get; set; }
        public DbSet<GiaSpDvCiDm> GiaSpDvCiDm { get; set; }
        public DbSet<GiaSpDvCuThe> GiaSpDvCuThe { get; set; }
        public DbSet<GiaSpDvCuTheCt> GiaSpDvCuTheCt { get; set; }
        public DbSet<GiaSpDvCuTheDm> GiaSpDvCuTheDm { get; set; }
        public DbSet<GiaSpDvCuTheNhom> GiaSpDvCuTheNhom { get; set; }
        public DbSet<GiaSpDvKhungGia> GiaSpDvKhungGia { get; set; }
        public DbSet<GiaSpDvKhungGiaCt> GiaSpDvKhungGiaCt { get; set; }
        public DbSet<GiaSpDvKhungGiaDm> GiaSpDvKhungGiaDm { get; set; }
        public DbSet<GiaSpDvKhungGiaNhom> GiaSpDvKhungGiaNhom { get; set; }
        public DbSet<GiaSpDvToiDa> GiaSpDvToiDa { get; set; }
        public DbSet<GiaSpDvToiDaCt> GiaSpDvToiDaCt { get; set; }
        public DbSet<GiaSpDvToiDaDm> GiaSpDvToiDaDm { get; set; }
        public DbSet<GiaSpDvToiDaNhom> GiaSpDvToiDaNhom { get; set; }
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
        public DbSet<GiaThueTaiSanCong> GiaThueTaiSanCong { get; set; }
        public DbSet<GiaThueTaiSanCongCt> GiaThueTaiSanCongCt { get; set; }
        public DbSet<GiaThueTaiSanCongDm> GiaThueTaiSanCongDm { get; set; }
        public DbSet<GiaTroGiaTroCuoc> GiaTroGiaTroCuoc { get; set; }
        public DbSet<GiaTroGiaTroCuocCt> GiaTroGiaTroCuocCt { get; set; }
        public DbSet<GiaTroGiaTroCuocDm> GiaTroGiaTroCuocDm { get; set; }
        public DbSet<GiaVangNgoaiTe> GiaVangNgoaiTe { get; set; }
        public DbSet<GiaVangNgoaiTeCt> GiaVangNgoaiTeCt { get; set; }
        public DbSet<GiaVangNgoaiTeDm> GiaVangNgoaiTeDm { get; set; }

        public DbSet<GiaThueMatDatMatNuoc> GiaThueMatDatMatNuoc { get; set; }
        public DbSet<GiaThueMatDatMatNuocCt> GiaThueMatDatMatNuocCt { get; set; }
        public DbSet<GiaThueMatDatMatNuocDm> GiaThueMatDatMatNuocDm { get; set; }

        public DbSet<GiaTaiSanTths> GiaTaiSanTths { get; set; }
        public DbSet<GiaTaiSanTthsCt> GiaTaiSanTthsCt { get; set; }
        public DbSet<GiaHhHaiQuanXnk> GiaHhHaiQuanXnk { get; set; }
        public DbSet<GiaHhHaiQuanXnkCt> GiaHhHaiQuanXnkCt { get; set; }
        public DbSet<GiaHhHaiQuanXnkDm> GiaHhHaiQuanXnkDm { get; set; }
        public DbSet<GiaHhHaiQuanXnkThue> GiaHhHaiQuanXnkThue { get; set; }

        // Định giá cây Trồng Vật Nuôi
        public DbSet<GiaCayTrongVatNuoi> GiaCayTrongVatNuoi { get; set; }
        public DbSet<GiaCayTrongVatNuoiCt> GiaCayTrongVatNuoiCt { get; set; }
        public DbSet<GiaCayTrongVatNuoiDm> GiaCayTrongVatNuoiDm { get; set; }
        public DbSet<GiaCayTrongVatNuoiNhom> GiaCayTrongVatNuoiNhom { get; set; }

        // Định giá xây dựng mới
        public DbSet<GiaXayDungMoi> GiaXayDungMoi { get; set; }
        public DbSet<GiaXayDungMoiCt> GiaXayDungMoiCt { get; set; }
        public DbSet<GiaXayDungMoiDm> GiaXayDungMoiDm { get; set; }
        public DbSet<GiaXayDungMoiNhom> GiaXayDungMoiNhom { get; set; }

        // Định giá thuê nhà ở sinh viên
        public DbSet<GiaThueNhaSV> GiaThueNhaSV { get; set; }
        public DbSet<GiaThueNhaSVCt> GiaThueNhaSVCt { get; set; }
        public DbSet<GiaThueNhaSVDm> GiaThueNhaSVDm { get; set; }
        public DbSet<GiaThueNhaSVNhom> GiaThueNhaSVNhom { get; set; }


        // Định giá giao dịch bất động sản
        public DbSet<GiaGiaoDichBDS> GiaGiaoDichBDS { get; set; }
        public DbSet<GiaGiaoDichBDSCt> GiaGiaoDichBDSCt { get; set; }
        public DbSet<GiaGiaoDichBDSDm> GiaGiaoDichBDSDm { get; set; }
       

        //Tham Dinh Gia
        public DbSet<ThamDinhGia> ThamDinhGia { get; set; }
        public DbSet<ThamDinhGiaCt> ThamDinhGiaCt { get; set; }
        public DbSet<ThamDinhGiaDv> ThamDinhGiaDv { get; set; }
        public DbSet<ThamDinhGiaDmHh> ThamDinhGiaDmHh { get; set; }

        //VBQLNN
        public DbSet<VbQlNn> VbQlNn { get; set; }

        //CPI
        public DbSet<ChiSoGiaTd> ChiSoGiaTd { get; set; }
        public DbSet<ChiSoGiaTdDd> ChiSoGiaTdDd { get; set; }
        public DbSet<ChiSoGiaTdDm> ChiSoGiaTdDm { get; set; }
        public DbSet<ChiSoGiaTdDmCt> ChiSoGiaTdDmCt { get; set; }
        public DbSet<ChiSoGiaTdDmCtDd> ChiSoGiaTdDmCtDd { get; set; }
        public DbSet<ChiSoGiaTdHh> ChiSoGiaTdHh { get; set; }
        public DbSet<ChiSoGiaTdHhCt> ChiSoGiaTdHhCt { get; set; }



        //Systems:
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyLvCc> CompanyLvCc { get; set; }
        /*public DbSet<DiaBanHd> DiaBanHd { get; set; }*/
        public DbSet<Districts> Districts { get; set; }
        public DbSet<DmDvt> DmDvt { get; set; }
        public DbSet<DmHinhThucThanhToan> DmHinhThucThanhToan { get; set; }
        public DbSet<DmLoaiGia> DmLoaiGia { get; set; }
        public DbSet<DmNganhKd> DmNganhKd { get; set; }
        public DbSet<DmNgheKd> DmNgheKd { get; set; }
        public DbSet<DmNhomHh> DmNhomHh { get; set; }
        public DbSet<DmLoaiDat> DmLoaiDat { get; set; }
        public DbSet<DsDiaBan> DsDiaBan { get; set; }
        public DbSet<DsDonVi> DsDonVi { get; set; }
        /*public DbSet<DsDonViTdg> DsDonViTdg { get; set; }*/
        public DbSet<DsNhomTaiKhoan> DsNhomTaiKhoan { get; set; }
        public DbSet<DsThamDinhVien> DsThamDinhVien { get; set; }
        public DbSet<DsVanPhong> DsVanPhong { get; set; }
        public DbSet<DsXaPhuong> DsXaPhuong { get; set; }
        public DbSet<GeneralConfigs> GeneralConfigs { get; set; }
        public DbSet<Register> Register { get; set; }
        public DbSet<Towns> Towns { get; set; }
        public DbSet<TtDnTd> TtDnTd { get; set; }
        public DbSet<TtDnTdCt> TtDnTdCt { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<GroupPermissions> GroupPermissions { get; set; }
        public DbSet<NhatKySuDung> NhatKySuDung { get; set; }

        //API
        public DbSet<KetNoiAPI> KetNoiAPI { get; set; }
        public DbSet<KetNoiAPI_HoSo> KetNoiAPI_HoSo { get; set; }
        public DbSet<KetNoiAPI_HoSo_ChiTiet> KetNoiAPI_HoSo_ChiTiet { get; set; }
        public DbSet<DmChucnang> DmChucnang { get; set; }

        //Danh mục
        public DbSet<DmChiTieuKinhTeViMo> DmChiTieuKinhTeViMo { get; set; }
        public DbSet<DmSieuThi> DmSieuThi { get; set; }
        public DbSet<DmTaiLieuHuongDanSuDung> DmTaiLieuHuongDanSuDung { get; set; }

        // Danh sách chức năng

        public DbSet<DanhMucChucNang> DanhMucChucNang { get; set; }
        

        /*//Test API
        public DbSet<WeatherForecast> WeatherForecast { get; set; }*/
    }
}
