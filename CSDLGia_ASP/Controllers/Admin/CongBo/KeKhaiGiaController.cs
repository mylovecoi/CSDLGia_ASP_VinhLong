using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.CongBo
{
    public class KeKhaiGiaController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KeKhaiGiaController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("CongBo/DichVuLuuTru")]
        public IActionResult DichVuLuuTru(string Madv, string Nam, string Macskd, string Trangthai)
        {

            var Manghe = "DVLT";
            var dsdonvi = (from cs in _db.KkGiaDvLtCskd
                           join com in _db.Company on cs.Madv equals com.Madv
                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == Manghe) on com.Mahs equals lvkd.Mahs
                           select new VMCompany
                           {
                               Id = com.Id,
                               Manghe = lvkd.Manghe,
                               Madv = com.Madv,
                               Madiaban = com.Madiaban,
                               Mahs = com.Mahs,
                               Tendn = com.Tendn,
                               Trangthai = com.Trangthai
                           }).ToList();

            if (string.IsNullOrEmpty(Madv))
            {
                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.Madv).First();
            }


            if (string.IsNullOrEmpty(Nam))
            {
                Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
            }

            var model_cskd = _db.KkGiaDvLtCskd.Where(t => t.Madv == Madv);

            if (string.IsNullOrEmpty(Macskd))
            {
                Macskd = model_cskd.Select(t => t.Macskd).First();
            }

            var comct = _db.CompanyLvCc.Where(t => t.Manghe == Manghe && t.Madv == Madv).ToList();
            var cskd = _db.KkGiaDvLtCskd.Where(t => t.Madv == Madv).ToList();
            // var model = _db.KkGia.Where(t => t.Madv == Madv && t.Ngaynhap.Year == int.Parse(Nam) && t.Manghe == Manghe && t.Macskd == Macskd).ToList();
            var dsDonViCQ = _db.DsDonVi;
            var qModel = _db.KkGia.AsQueryable(); // Không cần ép kiểu, chỉ cần sử dụng truy vấn EntityQueryable
            qModel = qModel.Where(x => x.Congbo == "DACONGBO");
            qModel = qModel.Where(t => t.Madv == Madv && t.Ngaynhap.Year == int.Parse(Nam) && t.Manghe == Manghe && t.Macskd == Macskd);

            var model = qModel.ToList();
            foreach (var item in model)
            {
                item.Tencqcq = "";
                var cqcq = dsDonViCQ.FirstOrDefault(x => x.MaDv == item.Macqcq);
                if (cqcq != null)
                    item.Tencqcq = cqcq.TenDv;
            }

            ViewData["DsDonVi"] = dsdonvi;
            var check_tt = _db.KkGia.Where(t => t.Manghe == Manghe && t.Trangthai != "DD").Count();
            ViewData["check_tt"] = check_tt;
            ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
            ViewData["Cqcq"] = dsDonViCQ;
            ViewData["Trangthai"] = Trangthai;
            ViewData["Cskd"] = _db.KkGiaDvLtCskd.Where(t => t.Madv == Madv);
            ViewData["Madv"] = Madv;
            ViewData["Macskd"] = Macskd;
            ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == Madv).Tendn;
            ViewData["Nam"] = Nam;
            ViewData["Manghe"] = Manghe;
            ViewData["Title"] = "Danh sách hồ sơ dịch vụ lưu trú";
            ViewBag.bSession = string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")) ? false : true;
            return View("Views/Admin/Systems/CongBo/DVLT.cshtml", model);
        }

    }
}
