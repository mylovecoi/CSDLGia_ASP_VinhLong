using CSDLGia_ASP.DAO;
using CSDLGia_ASP.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CSDLGia_ASP.Controllers.HeThong
{
    public class DSTaiKhoanController : Controller
    {
        private readonly CSDLGiaDBContext _dbGia;

        public DSTaiKhoanController(CSDLGiaDBContext dbGia)
        {
            _dbGia = dbGia;
        }

        [Route("DangNhap")]
        [HttpGet]
        public IActionResult DangNhap()
        {
            ViewBag.tblHeThong = new HeThongDao(_dbGia).GetTblHeThong();
            ViewData["Title"] = "Đăng nhập hệ thống";
            return View("Views/Hethong/TaiKhoan/DangNhap.cshtml");
        }

        [Route("DangNhap")]
        [HttpPost]
        public IActionResult KiemTraTaiKhoan()
        {
            string tenDangNhap = HttpContext.Request.Form["tenDangNhap"];
            string matKhau = HamDungChung.StringtoMD5(HttpContext.Request.Form["matKhau"]);
            var taiKhoan = new DSTaiKhoanDao(_dbGia).chkTaiKhoan(tenDangNhap);
            if (taiKhoan == null)
            {
                ViewData["ThongBaoLoi"] = "Tài khoản truy cập hoặc mật khẩu không đúng.";
                ViewData["url"] = "/DangNhap";
                return View("Views/BaoLoi/ThongBaoLoi.cshtml");
            }
            if (taiKhoan.TrangThai == false)
            {
                ViewData["ThongBaoLoi"] = "Tài khoản đang bị khóa. Bạn hãy liên hệ với người quản trị để mở khóa tài khoản.";
                ViewData["url"] = "/DangNhap";
                return View("Views/BaoLoi/ThongBaoLoi.cshtml");
            }
            if (taiKhoan.MatKhau != matKhau)
            {
                ViewData["ThongBaoLoi"] = "Tài khoản truy cập hoặc mật khẩu không đúng.";
                ViewData["url"] = "/DangNhap";
                return View("Views/BaoLoi/ThongBaoLoi.cshtml");
            }

            // Gán lại thông tin chức năng


            var thongTinTK = new DSTaiKhoanDao(_dbGia).GetTaiKhoan(tenDangNhap);
            HttpContext.Session.SetString("CSDLGia", JsonConvert.SerializeObject(thongTinTK));

            //Sử dụng kiểu Dictionary để xem có truy xuất nhanh hơn ko
            HttpContext.Session.SetString("ChucNang", JsonConvert.SerializeObject(new DMChucNangDao(_dbGia).ChucNang()));

            HttpContext.Session.SetString("PhanQuyen", JsonConvert.SerializeObject(new PhanQuyenDao(_dbGia).GetPhanQuyen(tenDangNhap)));

            if (thongTinTK.CapDo != "SSA")
            {

            }
            //var permissions = _db.Permissions.Where(p => p.Username == username);
            //if (permissions.Count() != 0)
            //{
            //    HttpContext.Session.SetString("Permission", JsonConvert.SerializeObject(permissions));
            //}
            //else
            //{
            //    var per_default = _db.Permissions.Where(p => p.Username == model.PhanLoai);
            //    HttpContext.Session.SetString("Permission", JsonConvert.SerializeObject(per_default));
            //}


            return RedirectToAction("Index", "Home");

            //return View("Views/Admin/Systems/Login/Login.cshtml");
        }

        [Route("Logout")]
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("Permission");
            HttpContext.Session.Remove("SsAdmin");
            return RedirectToAction("Login", "Login");
        }
    }
}
