
using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class ThongKeController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ThongKeController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("ThongKe/DanhSach")]
        [HttpGet]
        public IActionResult Index(int Nam, string Madv, int Thang)
        {      
            // Giá thuê mặt đất mặt nước
            var countGiaThueMatDatMatNuocCHTthang = _db.GiaThueMatDatMatNuoc.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaThueMatDatMatNuocCHTthang = countGiaThueMatDatMatNuocCHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaThueMatDatMatNuocCHTthang = countGiaThueMatDatMatNuocCHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaThueMatDatMatNuocCHTthang = countGiaThueMatDatMatNuocCHTthang.Count();

            var countGiaThueMatDatMatNuocHTthang = _db.GiaThueMatDatMatNuoc.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaThueMatDatMatNuocHTthang = countGiaThueMatDatMatNuocHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaThueMatDatMatNuocHTthang = countGiaThueMatDatMatNuocHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaThueMatDatMatNuocHTthang = countGiaThueMatDatMatNuocHTthang.Count();

            var countGiaThueMatDatMatNuocCHT = _db.GiaThueMatDatMatNuoc.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaThueMatDatMatNuocCHT = countGiaThueMatDatMatNuocCHT.Count();

            var countGiaThueMatDatMatNuocHT = _db.GiaThueMatDatMatNuoc.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv)); 
            int tongGiaThueMatDatMatNuocHT = countGiaThueMatDatMatNuocHT.Count();


            // Giá rừng
            var countGiaRungCHTthang = _db.GiaRung.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaRungCHTthang = countGiaRungCHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaRungCHTthang = countGiaRungCHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaRungCHTthang = countGiaRungCHTthang.Count();

            var countGiaRungHTthang = _db.GiaRung.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaRungHTthang = countGiaRungHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaRungHTthang = countGiaRungHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaRungHTthang = countGiaRungHTthang.Count();

            var countGiaRungCHT = _db.GiaRung.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaRungCHT = countGiaRungCHT.Count();

            var countGiaRungHT = _db.GiaRung.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaRungHT = countGiaRungHT.Count();


            // Giá thuê nhà xã hội
            var countGiaThueMuaNhaXhCHTthang = _db.GiaThueMuaNhaXh.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaThueMuaNhaXhCHTthang = countGiaThueMuaNhaXhCHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaThueMuaNhaXhCHTthang = countGiaThueMuaNhaXhCHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaThueMuaNhaXhCHTthang = countGiaThueMuaNhaXhCHTthang.Count();

            var countGiaThueMuaNhaXhHTthang = _db.GiaThueMuaNhaXh.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaThueMuaNhaXhHTthang = countGiaThueMuaNhaXhHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaThueMuaNhaXhHTthang = countGiaThueMuaNhaXhHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaThueMuaNhaXhHTthang = countGiaThueMuaNhaXhHTthang.Count();

            var countGiaThueMuaNhaXhCHT = _db.GiaThueMuaNhaXh.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaThueMuaNhaXhCHT = countGiaThueMuaNhaXhCHT.Count();

            var countGiaThueMuaNhaXhHT = _db.GiaThueMuaNhaXh.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaThueMuaNhaXhHT = countGiaThueMuaNhaXhHT.Count();


            // Giá tài sản công
            var countGiaTaiSanCongCHTthang = _db.GiaTaiSanCong.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaTaiSanCongCHTthang = countGiaTaiSanCongCHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaTaiSanCongCHTthang = countGiaTaiSanCongCHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaTaiSanCongCHTthang = countGiaTaiSanCongCHTthang.Count();

            var countGiaTaiSanCongHTthang = _db.GiaTaiSanCong.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaTaiSanCongHTthang = countGiaTaiSanCongHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaTaiSanCongHTthang = countGiaTaiSanCongHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaTaiSanCongHTthang = countGiaTaiSanCongHTthang.Count();

            var countGiaTaiSanCongCHT = _db.GiaTaiSanCong.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaTaiSanCongCHT = countGiaTaiSanCongCHT.Count();

            var countGiaTaiSanCongHT = _db.GiaTaiSanCong.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaTaiSanCongHT = countGiaTaiSanCongHT.Count();


            // Giá giao dịch bất động sản
            var countGiaGiaoDichBDSCHTthang = _db.GiaGiaoDichBDS.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaGiaoDichBDSCHTthang = countGiaGiaoDichBDSCHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaGiaoDichBDSCHTthang = countGiaGiaoDichBDSCHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaGiaoDichBDSCHTthang = countGiaGiaoDichBDSCHTthang.Count();

            var countGiaGiaoDichBDSHTthang = _db.GiaGiaoDichBDS.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaGiaoDichBDSHTthang = countGiaGiaoDichBDSHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaGiaoDichBDSHTthang = countGiaGiaoDichBDSHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaGiaoDichBDSHTthang = countGiaGiaoDichBDSHTthang.Count();

            var countGiaGiaoDichBDSCHT = _db.GiaGiaoDichBDS.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaGiaoDichBDSCHT = countGiaGiaoDichBDSCHT.Count();

            var countGiaGiaoDichBDSHT = _db.GiaGiaoDichBDS.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaGiaoDichBDSHT = countGiaGiaoDichBDSHT.Count();

            // Giá nước sinh hoạt
            var countGiaNuocShCHTthang = _db.GiaNuocSh.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaNuocShCHTthang = countGiaNuocShCHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaNuocShCHTthang = countGiaNuocShCHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaNuocShCHTthang = countGiaNuocShCHTthang.Count();

            var countGiaNuocShHTthang = _db.GiaNuocSh.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaNuocShHTthang = countGiaNuocShHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaNuocShHTthang = countGiaNuocShHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaNuocShHTthang = countGiaNuocShHTthang.Count();

            var countGiaNuocShCHT = _db.GiaNuocSh.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaNuocShCHT = countGiaNuocShCHT.Count();

            var countGiaNuocShHT = _db.GiaNuocSh.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaNuocShHT = countGiaNuocShHT.Count();


            // Giá thuê tài sản công
            var countGiaThueTaiSanCongCHTthang = _db.GiaThueTaiSanCong.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaThueTaiSanCongCHTthang = countGiaThueTaiSanCongCHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaThueTaiSanCongCHTthang = countGiaThueTaiSanCongCHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaThueTaiSanCongCHTthang = countGiaThueTaiSanCongCHTthang.Count();

            var countGiaThueTaiSanCongHTthang = _db.GiaThueTaiSanCong.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaThueTaiSanCongHTthang = countGiaThueTaiSanCongHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaThueTaiSanCongHTthang = countGiaThueTaiSanCongHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaThueTaiSanCongHTthang = countGiaThueTaiSanCongHTthang.Count();

            var countGiaThueTaiSanCongCHT = _db.GiaThueTaiSanCong.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaThueTaiSanCongCHT = countGiaThueTaiSanCongCHT.Count();

            var countGiaThueTaiSanCongHT = _db.GiaThueTaiSanCong.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaThueTaiSanCongHT = countGiaThueTaiSanCongHT.Count();

            //Giá SP, DVCI, DVSNC, HH-DV đặt hàng

            var countGiaSpDvCongIchCHTthang = _db.GiaSpDvCongIch.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaSpDvCongIchCHTthang = countGiaSpDvCongIchCHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaSpDvCongIchCHTthang = countGiaSpDvCongIchCHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaSpDvCongIchCHTthang = countGiaSpDvCongIchCHTthang.Count();

            var countGiaSpDvCongIchHTthang = _db.GiaSpDvCongIch.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaSpDvCongIchHTthang = countGiaSpDvCongIchHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaSpDvCongIchHTthang = countGiaSpDvCongIchHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaSpDvCongIchHTthang = countGiaSpDvCongIchHTthang.Count();

            var countGiaSpDvCongIchCHT = _db.GiaSpDvCongIch.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaSpDvCongIchCHT = countGiaSpDvCongIchCHT.Count();

            var countGiaSpDvCongIchHT = _db.GiaSpDvCongIch.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaSpDvCongIchHT = countGiaSpDvCongIchHT.Count();


            //Giá giáo dục đào tạo

            var countGiaDvGdDtCHTthang = _db.GiaDvGdDt.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaDvGdDtCHTthang = countGiaDvGdDtCHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaDvGdDtCHTthang = countGiaDvGdDtCHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaDvGdDtCHTthang = countGiaDvGdDtCHTthang.Count();

            var countGiaDvGdDtHTthang = _db.GiaDvGdDt.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaDvGdDtHTthang = countGiaDvGdDtHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaDvGdDtHTthang = countGiaDvGdDtHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaDvGdDtHTthang = countGiaDvGdDtHTthang.Count();

            var countGiaDvGdDtCHT = _db.GiaDvGdDt.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaDvGdDtCHT = countGiaDvGdDtCHT.Count();

            var countGiaDvGdDtHT = _db.GiaDvGdDt.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaDvGdDtHT = countGiaDvGdDtHT.Count();


            //Giá dịch vụ khám chữa bệnh

            var countGiaDvKcbCHTthang = _db.GiaDvKcb.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaDvKcbCHTthang = countGiaDvKcbCHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaDvKcbCHTthang = countGiaDvKcbCHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaDvKcbCHTthang = countGiaDvKcbCHTthang.Count();

            var countGiaDvKcbHTthang = _db.GiaDvKcb.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaDvKcbHTthang = countGiaDvKcbHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaDvKcbHTthang = countGiaDvKcbHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int tongGiaDvKcbHTthang = countGiaDvKcbHTthang.Count();

            var countGiaDvKcbCHT = _db.GiaDvKcb.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaDvKcbCHT = countGiaDvKcbCHT.Count();

            var countGiaDvKcbHT = _db.GiaDvKcb.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int tongGiaDvKcbHT = countGiaDvKcbHT.Count();



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





            };

            ViewData["Title"] = " Thống kê hồ sơ của đơn vị hành chính";
            ViewData["MenuLv1"] = "menu_thongke";
            ViewData["MenuLv2"] = "menu_thongke_donvi";
            ViewData["DsDonvi"] = _db.DsDonVi;
            ViewData["Nam"] = Nam;
            ViewData["Madv"] = Madv;
            ViewData["Thang"] = Thang;

            return View("Views/Admin/Systems/ThongKe/Index.cshtml", viewModel);
        }


    }
}




