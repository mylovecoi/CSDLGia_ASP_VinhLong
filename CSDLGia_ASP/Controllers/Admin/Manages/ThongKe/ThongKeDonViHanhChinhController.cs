using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Services;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ThongKe
{
    public class ThongKeDonViHanhChinhController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ThongKeDonViHanhChinhController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("ThongKeDonViHanhChinh/DanhSach")]
        [HttpGet]
        public IActionResult Index(int Nam, string Madv, int Thang)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "thongke.thongkehethong.nhaplieudonvihanhchinh", "Index"))
                {
                    // Giá thuê mặt đất mặt nước

                    // Nhập mới trong tháng
                    var countGiaThueMatDatMatNuocCHTthang = _db.GiaThueMatDatMatNuoc.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaThueMatDatMatNuocCHTthang = countGiaThueMatDatMatNuocCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaThueMatDatMatNuocCHTthang = countGiaThueMatDatMatNuocCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaThueMatDatMatNuocCHTthang = countGiaThueMatDatMatNuocCHTthang.Count();

                    // Hoàn thành trong tháng
                    var countGiaThueMatDatMatNuocHTthang = _db.GiaThueMatDatMatNuoc.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaThueMatDatMatNuocHTthang = countGiaThueMatDatMatNuocHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaThueMatDatMatNuocHTthang = countGiaThueMatDatMatNuocHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaThueMatDatMatNuocHTthang = countGiaThueMatDatMatNuocHTthang.Count();

                    // Nhập mới tất cả
                    var countGiaThueMatDatMatNuocCHT = _db.GiaThueMatDatMatNuoc.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaThueMatDatMatNuocCHT = countGiaThueMatDatMatNuocCHT.Count();

                    // Hoàn than tất cả
                    var countGiaThueMatDatMatNuocHT = _db.GiaThueMatDatMatNuoc.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaThueMatDatMatNuocHT = countGiaThueMatDatMatNuocHT.Count();


                    // Giá rừng
                    var countGiaRungCHTthang = _db.GiaRung.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaRungCHTthang = countGiaRungCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaRungCHTthang = countGiaRungCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaRungCHTthang = countGiaRungCHTthang.Count();

                    var countGiaRungHTthang = _db.GiaRung.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaRungHTthang = countGiaRungHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaRungHTthang = countGiaRungHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaRungHTthang = countGiaRungHTthang.Count();

                    var countGiaRungCHT = _db.GiaRung.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaRungCHT = countGiaRungCHT.Count();

                    var countGiaRungHT = _db.GiaRung.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaRungHT = countGiaRungHT.Count();


                    // Giá thuê nhà xã hội
                    var countGiaThueMuaNhaXhCHTthang = _db.GiaThueMuaNhaXh.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaThueMuaNhaXhCHTthang = countGiaThueMuaNhaXhCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaThueMuaNhaXhCHTthang = countGiaThueMuaNhaXhCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaThueMuaNhaXhCHTthang = countGiaThueMuaNhaXhCHTthang.Count();

                    var countGiaThueMuaNhaXhHTthang = _db.GiaThueMuaNhaXh.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaThueMuaNhaXhHTthang = countGiaThueMuaNhaXhHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaThueMuaNhaXhHTthang = countGiaThueMuaNhaXhHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaThueMuaNhaXhHTthang = countGiaThueMuaNhaXhHTthang.Count();

                    var countGiaThueMuaNhaXhCHT = _db.GiaThueMuaNhaXh.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaThueMuaNhaXhCHT = countGiaThueMuaNhaXhCHT.Count();

                    var countGiaThueMuaNhaXhHT = _db.GiaThueMuaNhaXh.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaThueMuaNhaXhHT = countGiaThueMuaNhaXhHT.Count();


                    // Giá tài sản công
                    var countGiaTaiSanCongCHTthang = _db.GiaTaiSanCong.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaTaiSanCongCHTthang = countGiaTaiSanCongCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaTaiSanCongCHTthang = countGiaTaiSanCongCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaTaiSanCongCHTthang = countGiaTaiSanCongCHTthang.Count();

                    var countGiaTaiSanCongHTthang = _db.GiaTaiSanCong.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaTaiSanCongHTthang = countGiaTaiSanCongHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaTaiSanCongHTthang = countGiaTaiSanCongHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaTaiSanCongHTthang = countGiaTaiSanCongHTthang.Count();

                    var countGiaTaiSanCongCHT = _db.GiaTaiSanCong.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaTaiSanCongCHT = countGiaTaiSanCongCHT.Count();

                    var countGiaTaiSanCongHT = _db.GiaTaiSanCong.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaTaiSanCongHT = countGiaTaiSanCongHT.Count();


                    // Giá giao dịch bất động sản
                    var countGiaGiaoDichBDSCHTthang = _db.GiaGiaoDichBDS.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaGiaoDichBDSCHTthang = countGiaGiaoDichBDSCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaGiaoDichBDSCHTthang = countGiaGiaoDichBDSCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaGiaoDichBDSCHTthang = countGiaGiaoDichBDSCHTthang.Count();

                    var countGiaGiaoDichBDSHTthang = _db.GiaGiaoDichBDS.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaGiaoDichBDSHTthang = countGiaGiaoDichBDSHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaGiaoDichBDSHTthang = countGiaGiaoDichBDSHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaGiaoDichBDSHTthang = countGiaGiaoDichBDSHTthang.Count();

                    var countGiaGiaoDichBDSCHT = _db.GiaGiaoDichBDS.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaGiaoDichBDSCHT = countGiaGiaoDichBDSCHT.Count();

                    var countGiaGiaoDichBDSHT = _db.GiaGiaoDichBDS.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaGiaoDichBDSHT = countGiaGiaoDichBDSHT.Count();

                    // Giá nước sinh hoạt
                    var countGiaNuocShCHTthang = _db.GiaNuocSh.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaNuocShCHTthang = countGiaNuocShCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaNuocShCHTthang = countGiaNuocShCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaNuocShCHTthang = countGiaNuocShCHTthang.Count();

                    var countGiaNuocShHTthang = _db.GiaNuocSh.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaNuocShHTthang = countGiaNuocShHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaNuocShHTthang = countGiaNuocShHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaNuocShHTthang = countGiaNuocShHTthang.Count();

                    var countGiaNuocShCHT = _db.GiaNuocSh.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaNuocShCHT = countGiaNuocShCHT.Count();

                    var countGiaNuocShHT = _db.GiaNuocSh.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaNuocShHT = countGiaNuocShHT.Count();


                    // Giá thuê tài sản công
                    var countGiaThueTaiSanCongCHTthang = _db.GiaThueTaiSanCong.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaThueTaiSanCongCHTthang = countGiaThueTaiSanCongCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaThueTaiSanCongCHTthang = countGiaThueTaiSanCongCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaThueTaiSanCongCHTthang = countGiaThueTaiSanCongCHTthang.Count();

                    var countGiaThueTaiSanCongHTthang = _db.GiaThueTaiSanCong.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaThueTaiSanCongHTthang = countGiaThueTaiSanCongHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaThueTaiSanCongHTthang = countGiaThueTaiSanCongHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaThueTaiSanCongHTthang = countGiaThueTaiSanCongHTthang.Count();

                    var countGiaThueTaiSanCongCHT = _db.GiaThueTaiSanCong.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaThueTaiSanCongCHT = countGiaThueTaiSanCongCHT.Count();

                    var countGiaThueTaiSanCongHT = _db.GiaThueTaiSanCong.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaThueTaiSanCongHT = countGiaThueTaiSanCongHT.Count();

                    //Giá SP, DVCI, DVSNC, HH-DV đặt hàng

                    var countGiaSpDvCongIchCHTthang = _db.GiaSpDvCongIch.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaSpDvCongIchCHTthang = countGiaSpDvCongIchCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaSpDvCongIchCHTthang = countGiaSpDvCongIchCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaSpDvCongIchCHTthang = countGiaSpDvCongIchCHTthang.Count();

                    var countGiaSpDvCongIchHTthang = _db.GiaSpDvCongIch.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaSpDvCongIchHTthang = countGiaSpDvCongIchHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaSpDvCongIchHTthang = countGiaSpDvCongIchHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaSpDvCongIchHTthang = countGiaSpDvCongIchHTthang.Count();

                    var countGiaSpDvCongIchCHT = _db.GiaSpDvCongIch.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaSpDvCongIchCHT = countGiaSpDvCongIchCHT.Count();

                    var countGiaSpDvCongIchHT = _db.GiaSpDvCongIch.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaSpDvCongIchHT = countGiaSpDvCongIchHT.Count();


                    //Giá giáo dục đào tạo

                    var countGiaDvGdDtCHTthang = _db.GiaDvGdDt.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaDvGdDtCHTthang = countGiaDvGdDtCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaDvGdDtCHTthang = countGiaDvGdDtCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaDvGdDtCHTthang = countGiaDvGdDtCHTthang.Count();

                    var countGiaDvGdDtHTthang = _db.GiaDvGdDt.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaDvGdDtHTthang = countGiaDvGdDtHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaDvGdDtHTthang = countGiaDvGdDtHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaDvGdDtHTthang = countGiaDvGdDtHTthang.Count();

                    var countGiaDvGdDtCHT = _db.GiaDvGdDt.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaDvGdDtCHT = countGiaDvGdDtCHT.Count();

                    var countGiaDvGdDtHT = _db.GiaDvGdDt.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaDvGdDtHT = countGiaDvGdDtHT.Count();


                    //Giá dịch vụ khám chữa bệnh

                    var countGiaDvKcbCHTthang = _db.GiaDvKcb.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaDvKcbCHTthang = countGiaDvKcbCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaDvKcbCHTthang = countGiaDvKcbCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaDvKcbCHTthang = countGiaDvKcbCHTthang.Count();

                    var countGiaDvKcbHTthang = _db.GiaDvKcb.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaDvKcbHTthang = countGiaDvKcbHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaDvKcbHTthang = countGiaDvKcbHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaDvKcbHTthang = countGiaDvKcbHTthang.Count();

                    var countGiaDvKcbCHT = _db.GiaDvKcb.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaDvKcbCHT = countGiaDvKcbCHT.Count();

                    var countGiaDvKcbHT = _db.GiaDvKcb.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaDvKcbHT = countGiaDvKcbHT.Count();


                    //Mức trợ giá trợ cước

                    var countGiaTroGiaTroCuocCHTthang = _db.GiaTroGiaTroCuoc.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaTroGiaTroCuocCHTthang = countGiaTroGiaTroCuocCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaTroGiaTroCuocCHTthang = countGiaTroGiaTroCuocCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaTroGiaTroCuocCHTthang = countGiaTroGiaTroCuocCHTthang.Count();

                    var countGiaTroGiaTroCuocHTthang = _db.GiaTroGiaTroCuoc.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaTroGiaTroCuocHTthang = countGiaTroGiaTroCuocHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaTroGiaTroCuocHTthang = countGiaTroGiaTroCuocHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaTroGiaTroCuocHTthang = countGiaTroGiaTroCuocHTthang.Count();

                    var countGiaTroGiaTroCuocCHT = _db.GiaTroGiaTroCuoc.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaTroGiaTroCuocCHT = countGiaTroGiaTroCuocCHT.Count();

                    var countGiaTroGiaTroCuocHT = _db.GiaTroGiaTroCuoc.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaTroGiaTroCuocHT = countGiaTroGiaTroCuocHT.Count();


                    //Giá thuế tài nguyên

                    var countGiaThueTaiNguyenCHTthang = _db.GiaThueTaiNguyen.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaThueTaiNguyenCHTthang = countGiaThueTaiNguyenCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaThueTaiNguyenCHTthang = countGiaThueTaiNguyenCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaThueTaiNguyenCHTthang = countGiaThueTaiNguyenCHTthang.Count();

                    var countGiaThueTaiNguyenHTthang = _db.GiaThueTaiNguyen.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaThueTaiNguyenHTthang = countGiaThueTaiNguyenHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaThueTaiNguyenHTthang = countGiaThueTaiNguyenHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaThueTaiNguyenHTthang = countGiaThueTaiNguyenHTthang.Count();

                    var countGiaThueTaiNguyenCHT = _db.GiaThueTaiNguyen.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaThueTaiNguyenCHT = countGiaThueTaiNguyenCHT.Count();

                    var countGiaThueTaiNguyenHT = _db.GiaThueTaiNguyen.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaThueTaiNguyenHT = countGiaThueTaiNguyenHT.Count();


                    //Giá hàng hóa tại siêu thị

                    var countGiaHangHoaTaiSieuThiCHTthang = _db.GiaHangHoaTaiSieuThi.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaHangHoaTaiSieuThiCHTthang = countGiaHangHoaTaiSieuThiCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaHangHoaTaiSieuThiCHTthang = countGiaHangHoaTaiSieuThiCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaHangHoaTaiSieuThiCHTthang = countGiaHangHoaTaiSieuThiCHTthang.Count();

                    var countGiaHangHoaTaiSieuThiHTthang = _db.GiaHangHoaTaiSieuThi.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaHangHoaTaiSieuThiHTthang = countGiaHangHoaTaiSieuThiHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaHangHoaTaiSieuThiHTthang = countGiaHangHoaTaiSieuThiHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaHangHoaTaiSieuThiHTthang = countGiaHangHoaTaiSieuThiHTthang.Count();

                    var countGiaHangHoaTaiSieuThiCHT = _db.GiaHangHoaTaiSieuThi.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaHangHoaTaiSieuThiCHT = countGiaHangHoaTaiSieuThiCHT.Count();

                    var countGiaHangHoaTaiSieuThiHT = _db.GiaHangHoaTaiSieuThi.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaHangHoaTaiSieuThiHT = countGiaHangHoaTaiSieuThiHT.Count();



                    //Giá sản phẩm dịch vụ cụ thể

                    var countGiaSpDvCuTheCHTthang = _db.GiaSpDvCuThe.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaSpDvCuTheCHTthang = countGiaSpDvCuTheCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaSpDvCuTheCHTthang = countGiaSpDvCuTheCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaSpDvCuTheCHTthang = countGiaSpDvCuTheCHTthang.Count();

                    var countGiaSpDvCuTheHTthang = _db.GiaSpDvCuThe.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaSpDvCuTheHTthang = countGiaSpDvCuTheHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaSpDvCuTheHTthang = countGiaSpDvCuTheHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaSpDvCuTheHTthang = countGiaSpDvCuTheHTthang.Count();

                    var countGiaSpDvCuTheCHT = _db.GiaSpDvCuThe.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaSpDvCuTheCHT = countGiaSpDvCuTheCHT.Count();

                    var countGiaSpDvCuTheHT = _db.GiaSpDvCuThe.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaSpDvCuTheHT = countGiaSpDvCuTheHT.Count();



                    //Giá sản phẩm dịch vụ tối đa

                    var countGiaSpDvToiDaCHTthang = _db.GiaSpDvToiDa.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaSpDvToiDaCHTthang = countGiaSpDvToiDaCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaSpDvToiDaCHTthang = countGiaSpDvToiDaCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaSpDvToiDaCHTthang = countGiaSpDvToiDaCHTthang.Count();

                    var countGiaSpDvToiDaHTthang = _db.GiaSpDvToiDa.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaSpDvToiDaHTthang = countGiaSpDvToiDaHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaSpDvToiDaHTthang = countGiaSpDvToiDaHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaSpDvToiDaHTthang = countGiaSpDvToiDaHTthang.Count();

                    var countGiaSpDvToiDaCHT = _db.GiaSpDvToiDa.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaSpDvToiDaCHT = countGiaSpDvToiDaCHT.Count();

                    var countGiaSpDvToiDaHT = _db.GiaSpDvToiDa.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaSpDvToiDaHT = countGiaSpDvToiDaHT.Count();


                    //Giá sản phẩm dịch vụ khung giá

                    var countGiaSpDvKhungGiaCHTthang = _db.GiaSpDvKhungGia.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaSpDvKhungGiaCHTthang = countGiaSpDvKhungGiaCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaSpDvKhungGiaCHTthang = countGiaSpDvKhungGiaCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaSpDvKhungGiaCHTthang = countGiaSpDvKhungGiaCHTthang.Count();

                    var countGiaSpDvKhungGiaHTthang = _db.GiaSpDvKhungGia.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaSpDvKhungGiaHTthang = countGiaSpDvKhungGiaHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaSpDvKhungGiaHTthang = countGiaSpDvKhungGiaHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaSpDvKhungGiaHTthang = countGiaSpDvKhungGiaHTthang.Count();

                    var countGiaSpDvKhungGiaCHT = _db.GiaSpDvKhungGia.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaSpDvKhungGiaCHT = countGiaSpDvKhungGiaCHT.Count();

                    var countGiaSpDvKhungGiaHT = _db.GiaSpDvKhungGia.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaSpDvKhungGiaHT = countGiaSpDvKhungGiaHT.Count();

                    //Giá đất cụ thể

                    var countGiaDatCuTheVlCHTthang = _db.GiaDatCuTheVl.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaDatCuTheVlCHTthang = countGiaDatCuTheVlCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaDatCuTheVlCHTthang = countGiaDatCuTheVlCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaDatCuTheVlCHTthang = countGiaDatCuTheVlCHTthang.Count();

                    var countGiaDatCuTheVlHTthang = _db.GiaDatCuTheVl.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaDatCuTheVlHTthang = countGiaDatCuTheVlHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaDatCuTheVlHTthang = countGiaDatCuTheVlHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaDatCuTheVlHTthang = countGiaDatCuTheVlHTthang.Count();

                    var countGiaDatCuTheVlCHT = _db.GiaDatCuTheVl.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaDatCuTheVlCHT = countGiaDatCuTheVlCHT.Count();

                    var countGiaDatCuTheVlHT = _db.GiaDatCuTheVl.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaDatCuTheVlHT = countGiaDatCuTheVlHT.Count();

                    //Bảng giá đất

                    var countGiaDatDiaBanCHTthang = _db.GiaDatDiaBan.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaDatDiaBanCHTthang = countGiaDatDiaBanCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaDatDiaBanCHTthang = countGiaDatDiaBanCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaDatDiaBanCHTthang = countGiaDatDiaBanCHTthang.Count();

                    var countGiaDatDiaBanHTthang = _db.GiaDatDiaBan.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaDatDiaBanHTthang = countGiaDatDiaBanHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaDatDiaBanHTthang = countGiaDatDiaBanHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaDatDiaBanHTthang = countGiaDatDiaBanHTthang.Count();

                    var countGiaDatDiaBanCHT = _db.GiaDatDiaBan.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaDatDiaBanCHT = countGiaDatDiaBanCHT.Count();

                    var countGiaDatDiaBanHT = _db.GiaDatDiaBan.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaDatDiaBanHT = countGiaDatDiaBanHT.Count();


                    //Giá đất giao dịch thực tế trên thị trường

                    var countGiaGiaoDichDatCHTthang = _db.GiaGiaoDichDat.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaGiaoDichDatCHTthang = countGiaGiaoDichDatCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaGiaoDichDatCHTthang = countGiaGiaoDichDatCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaGiaoDichDatCHTthang = countGiaGiaoDichDatCHTthang.Count();

                    var countGiaGiaoDichDatHTthang = _db.GiaGiaoDichDat.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaGiaoDichDatHTthang = countGiaGiaoDichDatHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaGiaoDichDatHTthang = countGiaGiaoDichDatHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaGiaoDichDatHTthang = countGiaGiaoDichDatHTthang.Count();

                    var countGiaGiaoDichDatCHT = _db.GiaGiaoDichDat.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaGiaoDichDatCHT = countGiaGiaoDichDatCHT.Count();

                    var countGiaGiaoDichDatHT = _db.GiaGiaoDichDat.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaGiaoDichDatHT = countGiaGiaoDichDatHT.Count();


                    //Giá trúng đấu giá quyền sử dụng đất
                    var countGiaDauGiaDatCHTthang = _db.GiaDauGiaDat.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaDauGiaDatCHTthang = countGiaDauGiaDatCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaDauGiaDatCHTthang = countGiaDauGiaDatCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaDauGiaDatCHTthang = countGiaDauGiaDatCHTthang.Count();

                    var countGiaDauGiaDatHTthang = _db.GiaDauGiaDat.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaDauGiaDatHTthang = countGiaDauGiaDatHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaDauGiaDatHTthang = countGiaDauGiaDatHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaDauGiaDatHTthang = countGiaDauGiaDatHTthang.Count();

                    var countGiaDauGiaDatCHT = _db.GiaDauGiaDat.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaDauGiaDatCHT = countGiaDauGiaDatCHT.Count();

                    var countGiaDauGiaDatHT = _db.GiaDauGiaDat.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaDauGiaDatHT = countGiaDauGiaDatHT.Count();

                    //Giá trúng thầu của HH-DV được mua sắm theo QĐ của PL về đấu thầu
                    var countGiaMuaTaiSanCHTthang = _db.GiaMuaTaiSan.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaMuaTaiSanCHTthang = countGiaMuaTaiSanCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaMuaTaiSanCHTthang = countGiaMuaTaiSanCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaMuaTaiSanCHTthang = countGiaMuaTaiSanCHTthang.Count();

                    var countGiaMuaTaiSanHTthang = _db.GiaMuaTaiSan.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaMuaTaiSanHTthang = countGiaMuaTaiSanHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaMuaTaiSanHTthang = countGiaMuaTaiSanHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaMuaTaiSanHTthang = countGiaMuaTaiSanHTthang.Count();

                    var countGiaMuaTaiSanCHT = _db.GiaMuaTaiSan.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaMuaTaiSanCHT = countGiaMuaTaiSanCHT.Count();

                    var countGiaMuaTaiSanHT = _db.GiaMuaTaiSan.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaMuaTaiSanHT = countGiaMuaTaiSanHT.Count();


                    //Giá hàng hóa dịch vụ khác
                    var countGiaHhDvkCHTthang = _db.GiaHhDvk.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaHhDvkCHTthang = countGiaHhDvkCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaHhDvkCHTthang = countGiaHhDvkCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaHhDvkCHTthang = countGiaHhDvkCHTthang.Count();

                    var countGiaHhDvkHTthang = _db.GiaHhDvk.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaHhDvkHTthang = countGiaHhDvkHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaHhDvkHTthang = countGiaHhDvkHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaHhDvkHTthang = countGiaHhDvkHTthang.Count();

                    var countGiaHhDvkCHT = _db.GiaHhDvk.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaHhDvkCHT = countGiaHhDvkCHT.Count();

                    var countGiaHhDvkHT = _db.GiaHhDvk.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaHhDvkHT = countGiaHhDvkHT.Count();

                    //Các loại giá khác
                    // Giá HH-DV khác theo quy định của pháp luật chuyên ngành

                    var countGiaHhDvCnCHTthang = _db.GiaHhDvCn.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaHhDvCnCHTthang = countGiaHhDvCnCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaHhDvCnCHTthang = countGiaHhDvCnCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaHhDvCnCHTthang = countGiaHhDvCnCHTthang.Count();

                    var countGiaHhDvCnHTthang = _db.GiaHhDvCn.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaHhDvCnHTthang = countGiaHhDvCnHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaHhDvCnHTthang = countGiaHhDvCnHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaHhDvCnHTthang = countGiaHhDvCnHTthang.Count();

                    var countGiaHhDvCnCHT = _db.GiaHhDvCn.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaHhDvCnCHT = countGiaHhDvCnCHT.Count();

                    var countGiaHhDvCnHT = _db.GiaHhDvCn.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaHhDvCnHT = countGiaHhDvCnHT.Count();


                    // Giá lệ phí trước bạ

                    var countGiaPhiLePhiCHTthang = _db.GiaPhiLePhi.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaPhiLePhiCHTthang = countGiaPhiLePhiCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaPhiLePhiCHTthang = countGiaPhiLePhiCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaPhiLePhiCHTthang = countGiaPhiLePhiCHTthang.Count();

                    var countGiaPhiLePhiHTthang = _db.GiaPhiLePhi.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaPhiLePhiHTthang = countGiaPhiLePhiHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaPhiLePhiHTthang = countGiaPhiLePhiHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaPhiLePhiHTthang = countGiaPhiLePhiHTthang.Count();

                    var countGiaPhiLePhiCHT = _db.GiaPhiLePhi.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaPhiLePhiCHT = countGiaPhiLePhiCHT.Count();

                    var countGiaPhiLePhiHT = _db.GiaPhiLePhi.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaPhiLePhiHT = countGiaPhiLePhiHT.Count();



                    // Giá phí lệ phí

                    var countPhiLePhiCHTthang = _db.PhiLePhi.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countPhiLePhiCHTthang = countPhiLePhiCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countPhiLePhiCHTthang = countPhiLePhiCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongPhiLePhiCHTthang = countPhiLePhiCHTthang.Count();

                    var countPhiLePhiHTthang = _db.PhiLePhi.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countPhiLePhiHTthang = countPhiLePhiHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countPhiLePhiHTthang = countPhiLePhiHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongPhiLePhiHTthang = countPhiLePhiHTthang.Count();

                    var countPhiLePhiCHT = _db.PhiLePhi.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongPhiLePhiCHT = countPhiLePhiCHT.Count();

                    var countPhiLePhiHT = _db.PhiLePhi.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongPhiLePhiHT = countPhiLePhiHT.Count();


                    // Giá vật liệu xây dựng

                    var countGiaVatLieuXayDungCHTthang = _db.GiaVatLieuXayDung.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaVatLieuXayDungCHTthang = countGiaVatLieuXayDungCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaVatLieuXayDungCHTthang = countGiaVatLieuXayDungCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaVatLieuXayDungCHTthang = countGiaVatLieuXayDungCHTthang.Count();

                    var countGiaVatLieuXayDungHTthang = _db.GiaVatLieuXayDung.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countGiaVatLieuXayDungHTthang = countGiaVatLieuXayDungHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countGiaVatLieuXayDungHTthang = countGiaVatLieuXayDungHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongGiaVatLieuXayDungHTthang = countGiaVatLieuXayDungHTthang.Count();

                    var countGiaVatLieuXayDungCHT = _db.GiaVatLieuXayDung.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaVatLieuXayDungCHT = countGiaVatLieuXayDungCHT.Count();

                    var countGiaVatLieuXayDungHT = _db.GiaVatLieuXayDung.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongGiaVatLieuXayDungHT = countGiaVatLieuXayDungHT.Count();


                    // Thẩm định giá 

                    var countThamDinhGiaCHTthang = _db.ThamDinhGia.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countThamDinhGiaCHTthang = countThamDinhGiaCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countThamDinhGiaCHTthang = countThamDinhGiaCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongThamDinhGiaCHTthang = countThamDinhGiaCHTthang.Count();

                    var countThamDinhGiaHTthang = _db.ThamDinhGia.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    if (Nam > 0) { countThamDinhGiaHTthang = countThamDinhGiaHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countThamDinhGiaHTthang = countThamDinhGiaHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongThamDinhGiaHTthang = countThamDinhGiaHTthang.Count();

                    var countThamDinhGiaCHT = _db.ThamDinhGia.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongThamDinhGiaCHT = countThamDinhGiaCHT.Count();

                    var countThamDinhGiaHT = _db.ThamDinhGia.Where(x => (x.Trangthai != "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
                    int tongThamDinhGiaHT = countThamDinhGiaHT.Count();



                    // Kê khai giá

                    var countKeKhaiDangKyGiaCHTthang = _db.KeKhaiDangKyGia.Where(x => (x.TrangThai == "CC"));
                    if (Nam > 0) { countKeKhaiDangKyGiaCHTthang = countKeKhaiDangKyGiaCHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countKeKhaiDangKyGiaCHTthang = countKeKhaiDangKyGiaCHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongKeKhaiDangKyGiaCHTthang = countKeKhaiDangKyGiaCHTthang.Count();

                    var countKeKhaiDangKyGiaHTthang = _db.KeKhaiDangKyGia.Where(x => (x.TrangThai != "CC"));
                    if (Nam > 0) { countKeKhaiDangKyGiaHTthang = countKeKhaiDangKyGiaHTthang.Where(x => x.Thoidiem.Year == Nam); }
                    if (Thang > 0) { countKeKhaiDangKyGiaHTthang = countKeKhaiDangKyGiaHTthang.Where(x => x.Thoidiem.Month == Thang); }
                    int tongKeKhaiDangKyGiaHTthang = countKeKhaiDangKyGiaHTthang.Count();

                    var countKeKhaiDangKyGiaCHT = _db.KeKhaiDangKyGia.Where(x => (x.TrangThai == "CC"));
                    int tongKeKhaiDangKyGiaCHT = countKeKhaiDangKyGiaCHT.Count();

                    var countKeKhaiDangKyGiaHT = _db.KeKhaiDangKyGia.Where(x => (x.TrangThai != "CC"));
                    int tongKeKhaiDangKyGiaHT = countKeKhaiDangKyGiaHT.Count();


                    var viewModel = new VMThongKe
                    {
                        countGiaThueMatDatMatNuocCHTthang = tongGiaThueMatDatMatNuocCHTthang,
                        countGiaThueMatDatMatNuocHTthang = tongGiaThueMatDatMatNuocHTthang,
                        countGiaThueMatDatMatNuocCHT = tongGiaThueMatDatMatNuocCHT,
                        countGiaThueMatDatMatNuocHT = tongGiaThueMatDatMatNuocHT,

                        countGiaRungCHTthang = tongGiaRungCHTthang,
                        countGiaRungHTthang = tongGiaRungHTthang,
                        countGiaRungCHT = tongGiaRungCHT,
                        countGiaRungHT = tongGiaRungHT,

                        countGiaThueMuaNhaXhCHTthang = tongGiaThueMuaNhaXhCHTthang,
                        countGiaThueMuaNhaXhHTthang = tongGiaThueMuaNhaXhHTthang,
                        countGiaThueMuaNhaXhCHT = tongGiaThueMuaNhaXhCHT,
                        countGiaThueMuaNhaXhHT = tongGiaThueMuaNhaXhHT,


                        countGiaTaiSanCongCHTthang = tongGiaTaiSanCongCHTthang,
                        countGiaTaiSanCongHTthang = tongGiaTaiSanCongHTthang,
                        countGiaTaiSanCongCHT = tongGiaTaiSanCongCHT,
                        countGiaTaiSanCongHT = tongGiaTaiSanCongHT,


                        countGiaGiaoDichBDSCHTthang = tongGiaGiaoDichBDSCHTthang,
                        countGiaGiaoDichBDSHTthang = tongGiaGiaoDichBDSHTthang,
                        countGiaGiaoDichBDSCHT = tongGiaGiaoDichBDSCHT,
                        countGiaGiaoDichBDSHT = tongGiaGiaoDichBDSHT,

                        countGiaNuocShCHTthang = tongGiaNuocShCHTthang,
                        countGiaNuocShHTthang = tongGiaNuocShHTthang,
                        countGiaNuocShCHT = tongGiaNuocShCHT,
                        countGiaNuocShHT = tongGiaNuocShHT,

                        countGiaThueTaiSanCongCHTthang = tongGiaThueTaiSanCongCHTthang,
                        countGiaThueTaiSanCongHTthang = tongGiaThueTaiSanCongHTthang,
                        countGiaThueTaiSanCongCHT = tongGiaThueTaiSanCongCHT,
                        countGiaThueTaiSanCongHT = tongGiaThueTaiSanCongHT,

                        countGiaSpDvCongIchCHTthang = tongGiaSpDvCongIchCHTthang,
                        countGiaSpDvCongIchHTthang = tongGiaSpDvCongIchHTthang,
                        countGiaSpDvCongIchCHT = tongGiaSpDvCongIchCHT,
                        countGiaSpDvCongIchHT = tongGiaSpDvCongIchHT,

                        countGiaDvGdDtCHTthang = tongGiaDvGdDtCHTthang,
                        countGiaDvGdDtHTthang = tongGiaDvGdDtHTthang,
                        countGiaDvGdDtCHT = tongGiaDvGdDtCHT,
                        countGiaDvGdDtHT = tongGiaDvGdDtHT,

                        countGiaDvKcbCHTthang = tongGiaDvKcbCHTthang,
                        countGiaDvKcbHTthang = tongGiaDvKcbHTthang,
                        countGiaDvKcbCHT = tongGiaDvKcbCHT,
                        countGiaDvKcbHT = tongGiaDvKcbHT,

                        countGiaTroGiaTroCuocCHTthang = tongGiaTroGiaTroCuocCHTthang,
                        countGiaTroGiaTroCuocHTthang = tongGiaTroGiaTroCuocHTthang,
                        countGiaTroGiaTroCuocCHT = tongGiaTroGiaTroCuocCHT,
                        countGiaTroGiaTroCuocHT = tongGiaTroGiaTroCuocHT,


                        countGiaThueTaiNguyenCHTthang = tongGiaThueTaiNguyenCHTthang,
                        countGiaThueTaiNguyenHTthang = tongGiaThueTaiNguyenHTthang,
                        countGiaThueTaiNguyenCHT = tongGiaThueTaiNguyenCHT,
                        countGiaThueTaiNguyenHT = tongGiaThueTaiNguyenHT,


                        countGiaHangHoaTaiSieuThiCHTthang = tongGiaHangHoaTaiSieuThiCHTthang,
                        countGiaHangHoaTaiSieuThiHTthang = tongGiaHangHoaTaiSieuThiHTthang,
                        countGiaHangHoaTaiSieuThiCHT = tongGiaHangHoaTaiSieuThiCHT,
                        countGiaHangHoaTaiSieuThiHT = tongGiaHangHoaTaiSieuThiHT,


                        countGiaSpDvCuTheCHTthang = tongGiaSpDvCuTheCHTthang,
                        countGiaSpDvCuTheHTthang = tongGiaSpDvCuTheHTthang,
                        countGiaSpDvCuTheCHT = tongGiaSpDvCuTheCHT,
                        countGiaSpDvCuTheHT = tongGiaSpDvCuTheHT,


                        countGiaSpDvToiDaCHTthang = tongGiaSpDvToiDaCHTthang,
                        countGiaSpDvToiDaHTthang = tongGiaSpDvToiDaHTthang,
                        countGiaSpDvToiDaCHT = tongGiaSpDvToiDaCHT,
                        countGiaSpDvToiDaHT = tongGiaSpDvToiDaHT,

                        countGiaSpDvKhungGiaCHTthang = tongGiaSpDvKhungGiaCHTthang,
                        countGiaSpDvKhungGiaHTthang = tongGiaSpDvKhungGiaHTthang,
                        countGiaSpDvKhungGiaCHT = tongGiaSpDvKhungGiaCHT,
                        countGiaSpDvKhungGiaHT = tongGiaSpDvKhungGiaHT,

                        countGiaDatCuTheVlCHTthang = tongGiaDatCuTheVlCHTthang,
                        countGiaDatCuTheVlHTthang = tongGiaDatCuTheVlHTthang,
                        countGiaDatCuTheVlCHT = tongGiaDatCuTheVlCHT,
                        countGiaDatCuTheVlHT = tongGiaDatCuTheVlHT,

                        countGiaDatDiaBanCHTthang = tongGiaDatDiaBanCHTthang,
                        countGiaDatDiaBanHTthang = tongGiaDatDiaBanHTthang,
                        countGiaDatDiaBanCHT = tongGiaDatDiaBanCHT,
                        countGiaDatDiaBanHT = tongGiaDatDiaBanHT,

                        countGiaGiaoDichDatCHTthang = tongGiaGiaoDichDatCHTthang,
                        countGiaGiaoDichDatHTthang = tongGiaGiaoDichDatHTthang,
                        countGiaGiaoDichDatCHT = tongGiaGiaoDichDatCHT,
                        countGiaGiaoDichDatHT = tongGiaGiaoDichDatHT,


                        countGiaDauGiaDatCHTthang = tongGiaDauGiaDatCHTthang,
                        countGiaDauGiaDatHTthang = tongGiaDauGiaDatHTthang,
                        countGiaDauGiaDatCHT = tongGiaDauGiaDatCHT,
                        countGiaDauGiaDatHT = tongGiaDauGiaDatHT,


                        countGiaMuaTaiSanCHTthang = tongGiaMuaTaiSanCHTthang,
                        countGiaMuaTaiSanHTthang = tongGiaMuaTaiSanHTthang,
                        countGiaMuaTaiSanCHT = tongGiaMuaTaiSanCHT,
                        countGiaMuaTaiSanHT = tongGiaMuaTaiSanHT,

                        countGiaHhDvkCHTthang = tongGiaHhDvkCHTthang,
                        countGiaHhDvkHTthang = tongGiaHhDvkHTthang,
                        countGiaHhDvkCHT = tongGiaHhDvkCHT,
                        countGiaHhDvkHT = tongGiaHhDvkHT,

                        countGiaHhDvCnCHTthang = tongGiaHhDvCnCHTthang,
                        countGiaHhDvCnHTthang = tongGiaHhDvCnHTthang,
                        countGiaHhDvCnCHT = tongGiaHhDvCnCHT,
                        countGiaHhDvCnHT = tongGiaHhDvCnHT,

                        countGiaPhiLePhiCHTthang = tongGiaPhiLePhiCHTthang,
                        countGiaPhiLePhiHTthang = tongGiaPhiLePhiHTthang,
                        countGiaPhiLePhiCHT = tongGiaPhiLePhiCHT,
                        countGiaPhiLePhiHT = tongGiaPhiLePhiHT,


                        countPhiLePhiCHTthang = tongPhiLePhiCHTthang,
                        countPhiLePhiHTthang = tongPhiLePhiHTthang,
                        countPhiLePhiCHT = tongPhiLePhiCHT,
                        countPhiLePhiHT = tongPhiLePhiHT,


                        countGiaVatLieuXayDungCHTthang = tongGiaVatLieuXayDungCHTthang,
                        countGiaVatLieuXayDungHTthang = tongGiaVatLieuXayDungHTthang,
                        countGiaVatLieuXayDungCHT = tongGiaVatLieuXayDungCHT,
                        countGiaVatLieuXayDungHT = tongGiaVatLieuXayDungHT,

                        countThamDinhGiaCHTthang = tongThamDinhGiaCHTthang,
                        countThamDinhGiaHTthang = tongThamDinhGiaHTthang,
                        countThamDinhGiaCHT = tongThamDinhGiaCHT,
                        countThamDinhGiaHT = tongThamDinhGiaHT,


                    };

                    ViewData["Title"] = " Thống kê hồ sơ của đơn vị hành chính";
                    ViewData["MenuLv1"] = "menu_thongke";
                    ViewData["MenuLv2"] = "menu_thongke_donvi";
                    ViewData["DsDonvi"] = _db.DsDonVi;
                    ViewData["Nam"] = Nam;
                    //ViewData["Madv"] = Madv;
                    //ViewData["Thang"] = Thang;

                    ViewData["Nam"] = Nam == 0 ? DateTime.Now.Year : Nam;
                    ViewData["Thang"] = Thang == 0 ? DateTime.Now.Month : Thang;

                    return View("Views/Admin/Manages/ThongKe/DonViHanhChinh/Index.cshtml", viewModel);

                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

        }//
    }
}
