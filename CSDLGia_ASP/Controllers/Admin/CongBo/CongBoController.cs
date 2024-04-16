//using CSDLGia_ASP.Database;
//using CSDLGia_ASP.Helper;
//using CSDLGia_ASP.Models.Manages.KeKhaiGia;
//using CSDLGia_ASP.ViewModels.Manages.KeKhaiDkg;
//using CSDLGia_ASP.ViewModels.Systems;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace CSDLGia_ASP.Controllers.Admin.CongBo
//{
//    public class CongBoController : Controller
//    {
//        private readonly CSDLGiaDBContext _db;

//        public CongBoController(CSDLGiaDBContext db)
//        {
//            _db = db;
//        }

//        [Route("CongBo/BinhOnGia")]
//        public IActionResult BinhOnGia(string Madv, string Nam, string Manghe)
//        {
//            var dsdonvi = _db.Company.ToList();
//            if (string.IsNullOrEmpty(Madv))
//            {
//                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.Madv).First();
//            }
//            var model = _db.KkMhBog.Where(t => t.Madv == Madv && t.Congbo == "DACONGBO").ToList();

//            if (string.IsNullOrEmpty(Nam))
//            {
//                model = model.ToList();
//            }
//            else
//            {
//                if (Nam != "all")
//                {
//                    model = model.Where(t => t.Ngayhieuluc.Year == int.Parse(Nam)).ToList();
//                }
//                else
//                {
//                    model = model.ToList();
//                }
//            }

//            if (string.IsNullOrEmpty(Manghe))
//            {
//                model = model.ToList();
//            }
//            else
//            {
//                if (Manghe != "all")
//                {
//                    model = model.Where(t => t.Manghe == Manghe).ToList();
//                }
//                else
//                {
//                    model = model.ToList();
//                }

//            }

//            var model_join = (from kkbog in model
//                              join nghe in _db.DmNgheKd on kkbog.Manghe equals nghe.Manghe
//                              select new VMKkMhBog
//                              {
//                                  Id = kkbog.Id,
//                                  Tennghe = nghe.Tennghe,
//                                  Socv = kkbog.Socv,
//                                  Phanloai = kkbog.Phanloai,
//                                  Ngayhieuluc = kkbog.Ngayhieuluc,
//                                  Ngaychuyen = kkbog.Ngaychuyen,
//                                  Trangthai = kkbog.Trangthai,
//                                  Madiaban = kkbog.Madiaban,
//                                  Mahs = kkbog.Mahs,
//                                  Madv = kkbog.Madv,
//                                  Lydo = kkbog.Lydo,
//                                  Macqcq = kkbog.Macqcq,
//                              }).ToList();

//            var dmnghekd = (from comct in _db.CompanyLvCc.Where(t => t.Madv == Madv)
//                            join nghe in _db.DmNgheKd.Where(t => t.Manganh == "BOG") on comct.Manghe equals nghe.Manghe
//                            select new VMCompanyLvCc
//                            {
//                                Id = comct.Id,
//                                Manghe = nghe.Manghe,
//                                Tennghe = nghe.Tennghe,
//                                Phanloai = nghe.Phanloai,
//                                Madv = comct.Madv
//                            });


//            ViewData["DsDonVi"] = dsdonvi;
//            ViewData["Cqcq"] = _db.DsDonVi;
//            ViewData["DmNgheKd"] = dmnghekd;
//            ViewData["Madv"] = Madv;
//            ViewData["Nam"] = Nam;
//            ViewData["Manghe"] = Manghe;
//            ViewBag.bSession = string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")) ? false : true;
//            return View("Views/Admin/Systems/CongBo/BinhOnGia.cshtml", model_join);
//        }

//        [Route("CongBo/DVLT")]
//        public IActionResult DVLT(string Madv, string Nam, string Macskd, string Trangthai)
//        {

//            var Manghe = "DVLT";
//            var dsdonvi = (from cs in _db.KkGiaDvLtCskd
//                           join com in _db.Company on cs.Madv equals com.Madv
//                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == Manghe) on com.Mahs equals lvkd.Mahs
//                           select new VMCompany
//                           {
//                               Id = com.Id,
//                               Manghe = lvkd.Manghe,
//                               Madv = com.Madv,
//                               Madiaban = com.Madiaban,
//                               Mahs = com.Mahs,
//                               Tendn = com.Tendn,
//                               Trangthai = com.Trangthai
//                           }).ToList();

//            if (string.IsNullOrEmpty(Madv))
//            {
//                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.Madv).First();
//            }


//            if (string.IsNullOrEmpty(Nam))
//            {
//                Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
//            }

//            var model_cskd = _db.KkGiaDvLtCskd.Where(t => t.Madv == Madv);

//            if (string.IsNullOrEmpty(Macskd))
//            {
//                Macskd = model_cskd.Select(t => t.Macskd).First();
//            }

//            var comct = _db.CompanyLvCc.Where(t => t.Manghe == Manghe && t.Madv == Madv).ToList();
//            var cskd = _db.KkGiaDvLtCskd.Where(t => t.Madv == Madv).ToList();
//            // var model = _db.KkGia.Where(t => t.Madv == Madv && t.Ngaynhap.Year == int.Parse(Nam) && t.Manghe == Manghe && t.Macskd == Macskd).ToList();
//            var dsDonViCQ = _db.DsDonVi;
//            var qModel = _db.KkGia.AsQueryable(); // Không cần ép kiểu, chỉ cần sử dụng truy vấn EntityQueryable
//            qModel = qModel.Where(x => x.Congbo == "DACONGBO");
//            qModel = qModel.Where(t => t.Madv == Madv && t.Ngaynhap.Year == int.Parse(Nam) && t.Manghe == Manghe && t.Macskd == Macskd);

//            var model = qModel.ToList();
//            foreach (var item in model)
//            {
//                item.Tencqcq = "";
//                var cqcq = dsDonViCQ.FirstOrDefault(x => x.MaDv == item.Macqcq);
//                if (cqcq != null)
//                    item.Tencqcq = cqcq.TenDv;
//            }

//            ViewData["DsDonVi"] = dsdonvi;
//            var check_tt = _db.KkGia.Where(t => t.Manghe == Manghe && t.Trangthai != "DD").Count();
//            ViewData["check_tt"] = check_tt;
//            ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
//            ViewData["Cqcq"] = dsDonViCQ;
//            ViewData["Trangthai"] = Trangthai;
//            ViewData["Cskd"] = _db.KkGiaDvLtCskd.Where(t => t.Madv == Madv);
//            ViewData["Madv"] = Madv;
//            ViewData["Macskd"] = Macskd;
//            ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == Madv).Tendn;
//            ViewData["Nam"] = Nam;
//            ViewData["Manghe"] = Manghe;
//            ViewData["Title"] = "Danh sách hồ sơ dịch vụ lưu trú";
//            ViewBag.bSession = string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")) ? false : true;
//            return View("Views/Admin/Systems/CongBo/DVLT.cshtml", model);
//        }

//        [Route("CongBo/XMTXD")]
//        public IActionResult XMTXD(string Madv, string Nam)
//        {

//            var Manghe = "XMTXD";
//            var dsdonvi = (from com in _db.Company
//                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == Manghe) on com.Mahs equals lvkd.Mahs
//                           select new VMCompany
//                           {
//                               Id = com.Id,
//                               Manghe = lvkd.Manghe,
//                               Madv = com.Madv,
//                               Madiaban = com.Madiaban,
//                               Mahs = com.Mahs,
//                               Tendn = com.Tendn,
//                               Trangthai = com.Trangthai
//                           }).ToList();

//            if (string.IsNullOrEmpty(Madv))
//            {
//                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.Madv).First();
//            }


//            if (string.IsNullOrEmpty(Nam))
//            {
//                Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
//            }

//            var model = _db.KkGia.Where(t => t.Madv == Madv && t.Ngaynhap.Year == int.Parse(Nam) && t.Manghe == Manghe && t.Congbo == "DACONGBO").ToList();


//            ViewData["DsDonVi"] = dsdonvi;
//            var check_tt = _db.KkGia.Where(t => t.Manghe == Manghe && t.Trangthai != "DD" && t.Madv == Madv).Count();
//            ViewData["check_tt"] = check_tt;
//            ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
//            ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
//            ViewData["Madv"] = Madv;

//            ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == Madv).Tendn;
//            ViewData["Nam"] = Nam;
//            ViewData["Manghe"] = Manghe;
//            ViewData["Title"] = "Danh sách hồ sơ giá xi măng thép xây dựng";
//            ViewBag.bSession = string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")) ? false : true;
//            return View("Views/Admin/Systems/CongBo/XMTXD.cshtml", model);
//        }

//        [Route("CongBo/THAN")]
//        public IActionResult THAN(string Madv, string Nam)
//        {
//            var Manghe = "THAN";
//            var dsdonvi = (from com in _db.Company
//                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == Manghe) on com.Mahs equals lvkd.Mahs
//                           select new VMCompany
//                           {
//                               Id = com.Id,
//                               Manghe = lvkd.Manghe,
//                               Madv = com.Madv,
//                               Madiaban = com.Madiaban,
//                               Mahs = com.Mahs,
//                               Tendn = com.Tendn,
//                               Trangthai = com.Trangthai
//                           }).ToList();


//            if (string.IsNullOrEmpty(Madv))
//            {
//                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.Madv).First();
//            }

//            if (string.IsNullOrEmpty(Nam))
//            {
//                Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
//            }

//            var comct = _db.CompanyLvCc.Where(t => t.Manghe == Manghe && t.Madv == Madv).ToList();
//            var model = _db.KkGia.Where(t => t.Madv == Madv && t.Ngaynhap.Year == int.Parse(Nam) && t.Manghe == Manghe && t.Congbo == "DACONGBO").ToList();

//            ViewData["DsDonVi"] = dsdonvi;
//            ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
//            ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
//            ViewData["Madv"] = Madv;
//            ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == Madv).Tendn;
//            ViewData["Nam"] = Nam;
//            ViewData["Manghe"] = Manghe;
//            ViewData["Title"] = "Danh sách hồ sơ giá than";
//            ViewBag.bSession = string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")) ? false : true;
//            return View("Views/Admin/Systems/CongBo/THAN.cshtml", model);
//        }

//        [Route("CongBo/TACN")]
//        public IActionResult TACN(string Madv, string Nam)
//        {

//            var Manghe = "TACN";
//            var dsdonvi = (from com in _db.Company
//                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == Manghe) on com.Mahs equals lvkd.Mahs
//                           select new VMCompany
//                           {
//                               Id = com.Id,
//                               Manghe = lvkd.Manghe,
//                               Madv = com.Madv,
//                               Madiaban = com.Madiaban,
//                               Mahs = com.Mahs,
//                               Tendn = com.Tendn,
//                               Trangthai = com.Trangthai
//                           }).ToList();


//            if (string.IsNullOrEmpty(Madv))
//            {
//                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.Madv).First();
//            }



//            if (string.IsNullOrEmpty(Nam))
//            {
//                Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
//            }

//            var comct = _db.CompanyLvCc.Where(t => t.Manghe == Manghe && t.Madv == Madv).ToList();


//            var model = _db.KkGia.Where(t => t.Madv == Madv && t.Ngaynhap.Year == int.Parse(Nam) && t.Manghe == Manghe && t.Congbo == "DACONGBO").ToList();
//            if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
//            {
//                ViewData["DsDonVi"] = dsdonvi;
//            }
//            else
//            {
//                ViewData["DsDonVi"] = dsdonvi.Where(t => t.Madv == Madv);
//            }

//            ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
//            ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
//            ViewData["Madv"] = Madv;
//            ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == Madv).Tendn;
//            ViewData["Nam"] = Nam;
//            ViewData["Manghe"] = Manghe;
//            ViewData["Title"] = "Danh sách hồ sơ kê khai giá thức ăn chăn nuôi";

//            ViewBag.bSession = string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")) ? false : true;
//            return View("Views/Admin/Systems/CongBo/TACN.cshtml", model);

//        }

//        [Route("CongBo/GIAY")]
//        public IActionResult GIAY(string Madv, string Nam)
//        {
//            var Manghe = "GIAY";
//            var dsdonvi = (from com in _db.Company
//                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == Manghe) on com.Mahs equals lvkd.Mahs
//                           select new VMCompany
//                           {
//                               Id = com.Id,
//                               Manghe = lvkd.Manghe,
//                               Madv = com.Madv,
//                               Madiaban = com.Madiaban,
//                               Mahs = com.Mahs,
//                               Tendn = com.Tendn,
//                               Trangthai = com.Trangthai
//                           }).ToList();


//            if (string.IsNullOrEmpty(Madv))
//            {
//                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.Madv).First();
//            }


//            if (string.IsNullOrEmpty(Nam))
//            {
//                Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
//            }

//            var comct = _db.CompanyLvCc.Where(t => t.Manghe == Manghe && t.Madv == Madv).ToList();


//            var model = _db.KkGia.Where(t => t.Madv == Madv && t.Ngaynhap.Year == int.Parse(Nam) && t.Manghe == Manghe && t.Congbo == "DACONGBO").ToList();

//            ViewData["DsDonVi"] = dsdonvi;



//            ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
//            ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
//            ViewData["Madv"] = Madv;
//            ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == Madv).Tendn;
//            ViewData["Nam"] = Nam;
//            ViewData["Manghe"] = Manghe;
//            ViewData["Title"] = "Danh sách hồ sơ giấy";
//            ViewBag.bSession = string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")) ? false : true;
//            return View("Views/Admin/Systems/CongBo/GIAY.cshtml", model);
//        }

//        [Route("CongBo/SACH")]
//        public IActionResult SACH(string Madv, string Nam)
//        {
//            var Manghe = "SACH";
//            var dsdonvi = (from com in _db.Company
//                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == Manghe) on com.Mahs equals lvkd.Mahs
//                           select new VMCompany
//                           {
//                               Id = com.Id,
//                               Manghe = lvkd.Manghe,
//                               Madv = com.Madv,
//                               Madiaban = com.Madiaban,
//                               Mahs = com.Mahs,
//                               Tendn = com.Tendn,
//                               Trangthai = com.Trangthai
//                           }).ToList();


//            if (string.IsNullOrEmpty(Madv))
//            {
//                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.Madv).First();
//            }


//            if (string.IsNullOrEmpty(Nam))
//            {
//                Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
//            }

//            var comct = _db.CompanyLvCc.Where(t => t.Manghe == Manghe && t.Madv == Madv).ToList();


//            var model = _db.KkGia.Where(t => t.Madv == Madv && t.Ngaynhap.Year == int.Parse(Nam) && t.Manghe == Manghe && t.Congbo == "DACONGBO").ToList();

//            ViewData["DsDonVi"] = dsdonvi;



//            ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
//            ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
//            ViewData["Madv"] = Madv;
//            ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == Madv).Tendn;
//            ViewData["Nam"] = Nam;
//            ViewData["Manghe"] = Manghe;
//            ViewData["Title"] = "Danh sách hồ sơ sách giáo khoa";
//            ViewBag.bSession = string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")) ? false : true;
//            return View("Views/Admin/Systems/CongBo/SACH.cshtml", model);
//        }

//        [Route("CongBo/ETANOL")]
//        public IActionResult ETANOL(string Madv, string Nam)
//        {
//            var Manghe = "ETANOL";
//            var dsdonvi = (from com in _db.Company
//                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == Manghe) on com.Mahs equals lvkd.Mahs
//                           select new VMCompany
//                           {
//                               Id = com.Id,
//                               Manghe = lvkd.Manghe,
//                               Madv = com.Madv,
//                               Madiaban = com.Madiaban,
//                               Mahs = com.Mahs,
//                               Tendn = com.Tendn,
//                               Trangthai = com.Trangthai
//                           }).ToList();


//            if (string.IsNullOrEmpty(Madv))
//            {
//                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.Madv).First();
//            }


//            if (string.IsNullOrEmpty(Nam))
//            {
//                Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
//            }

//            var comct = _db.CompanyLvCc.Where(t => t.Manghe == Manghe && t.Madv == Madv).ToList();


//            var model = _db.KkGia.Where(t => t.Madv == Madv && t.Ngaynhap.Year == int.Parse(Nam) && t.Manghe == Manghe && t.Congbo == "DACONGBO").ToList();

//            ViewData["DsDonVi"] = dsdonvi;



//            ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
//            ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
//            ViewData["Madv"] = Madv;
//            ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == Madv).Tendn;
//            ViewData["Nam"] = Nam;
//            ViewData["Manghe"] = Manghe;
//            ViewData["Title"] = "Danh sách hồ sơ Etanol";
//            ViewBag.bSession = string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")) ? false : true;
//            return View("Views/Admin/Systems/CongBo/ETANOL.cshtml", model);
//        }

//        [Route("CongBo/TPCNTE6T")]
//        public IActionResult TPCNTE6T(string Madv, string Nam)
//        {
//            var Manghe = "TPCNTE6T";
//            var dsdonvi = (from com in _db.Company
//                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == Manghe) on com.Mahs equals lvkd.Mahs
//                           select new VMCompany
//                           {
//                               Id = com.Id,
//                               Manghe = lvkd.Manghe,
//                               Madv = com.Madv,
//                               Madiaban = com.Madiaban,
//                               Mahs = com.Mahs,
//                               Tendn = com.Tendn,
//                               Trangthai = com.Trangthai
//                           }).ToList();


//            if (string.IsNullOrEmpty(Madv))
//            {
//                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.Madv).First();
//            }


//            if (string.IsNullOrEmpty(Nam))
//            {
//                Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
//            }

//            var comct = _db.CompanyLvCc.Where(t => t.Manghe == Manghe && t.Madv == Madv).ToList();


//            var model = _db.KkGia.Where(t => t.Madv == Madv && t.Ngaynhap.Year == int.Parse(Nam) && t.Manghe == Manghe && t.Congbo == "DACONGBO").ToList();

//            ViewData["DsDonVi"] = dsdonvi;

//            ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
//            ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
//            ViewData["Madv"] = Madv;
//            ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == Madv).Tendn;
//            ViewData["Nam"] = Nam;
//            ViewData["Manghe"] = Manghe;
//            ViewData["Title"] = "Danh sách hồ sơ thực phẩm chức năng cho trẻ em 6 tuổi";
//            ViewBag.bSession = string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")) ? false : true;
//            return View("Views/Admin/Systems/CongBo/TPCNTE6T.cshtml", model);
//        }
 
//    }
//}

