using CSDLGia_ASP.Database;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems.DanhMucChucNang
{
    public class DanhMucChucNangController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DanhMucChucNangController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        // Menu 1
        public IActionResult Index1()
        {
            var chucnang = _db.DanhMucChucNang.Where(t => t.Capdo == "0").ToList();
            return View("Views/Admin/Systems/DanhMucChucNang/Index1.cshtml", chucnang);
        }

        //CSDLGia_ASP.Models.Systems.DanhMucChucNang
        public IActionResult Store1(CSDLGia_ASP.Models.Systems.DanhMucChucNang request)
        {
            var model = new CSDLGia_ASP.Models.Systems.DanhMucChucNang
            {
                Maso = request.Maso,
                Capdo = request.Capdo,
                Maso_goc = request.Maso_goc,
                Menu = request.Menu,
                Mota = request.Mota,
                Created_at = request.Created_at,
                Updated_at = request.Updated_at,
            };
            _db.DanhMucChucNang.Add(model);
            _db.SaveChanges();

            var chucnang = _db.DanhMucChucNang.Where(t => t.Capdo == "0").ToList();
            return View("Views/Admin/Systems/DanhMucChucNang/Index1.cshtml", chucnang);
        }

        public IActionResult Update1(int Id_edit, string Maso_edit, string Menu_edit, string Mota_edit)
        {
            var model = _db.DanhMucChucNang.FirstOrDefault(t => t.Id == Id_edit);

            model.Maso = Maso_edit;
            model.Menu = Menu_edit;
            model.Mota = Mota_edit;

            _db.DanhMucChucNang.Update(model);
            _db.SaveChanges();

            var chucnang = _db.DanhMucChucNang.Where(t => t.Capdo == "0").ToList();
            return View("Views/Admin/Systems/DanhMucChucNang/Index1.cshtml", chucnang);
        }

        public IActionResult Delete1(string Maso_delete)
        {
            var model = _db.DanhMucChucNang.FirstOrDefault(t => t.Maso == Maso_delete);

            _db.DanhMucChucNang.Remove(model);
            _db.SaveChanges();
            var chucnang1 = _db.DanhMucChucNang.Where(t => t.Capdo == "0").ToList();
            return View("Views/Admin/Systems/DanhMucChucNang/Index1.cshtml", chucnang1);
        }


        //Menu2
        public IActionResult Index2(string maso1)
        {
            ViewData["maso1"] = maso1;
            var tenchucnang = _db.DanhMucChucNang.Where(cn => cn.Maso == maso1).Select(cn => cn.Menu).FirstOrDefault();
            ViewData["dschucnang"] = tenchucnang;
            var model = _db.DanhMucChucNang.Where(t => t.Maso_goc == maso1).ToList();
            return View("Views/Admin/Systems/DanhMucChucNang/Index2.cshtml", model);
        }

        public IActionResult Store2(CSDLGia_ASP.Models.Systems.DanhMucChucNang request)
        {
            var model = new CSDLGia_ASP.Models.Systems.DanhMucChucNang
            {
                Maso = request.Maso,
                Capdo = request.Capdo,
                Maso_goc = request.Maso_goc,
                Menu = request.Menu,
                Mota = request.Mota,
                Created_at = request.Created_at,
                Updated_at = request.Updated_at,
            };
            _db.DanhMucChucNang.Add(model);
            _db.SaveChanges();

            var tenchucnang = _db.DanhMucChucNang.Where(cn => cn.Maso == request.Maso_goc).Select(cn => cn.Menu).FirstOrDefault();
            ViewData["dschucnang"] = tenchucnang;

            ViewData["maso1"] = request.Maso_goc;
            var chucnang1 = _db.DanhMucChucNang.Where(t => t.Maso_goc == request.Maso_goc).ToList();
            return View("Views/Admin/Systems/DanhMucChucNang/Index2.cshtml", chucnang1);
        }

        public IActionResult Update2(int Id_edit, string Maso_edit, string Menu_edit, string Mota_edit, string Maso_goc_edit)
        {
            var model = _db.DanhMucChucNang.FirstOrDefault(t => t.Id == Id_edit);

            model.Maso = Maso_edit;
            model.Menu = Menu_edit;
            model.Mota = Mota_edit;
            model.Maso_goc = Maso_goc_edit;
            _db.DanhMucChucNang.Update(model);
            _db.SaveChanges();
            var chucnang = _db.DanhMucChucNang.Where(t => t.Maso_goc == Maso_goc_edit).ToList();
            var tenchucnang = _db.DanhMucChucNang.Where(t => t.Maso == Maso_goc_edit).Select(cn => cn.Menu).FirstOrDefault();
            ViewData["dschucnang"] = tenchucnang;
            ViewData["maso1"] = Maso_goc_edit;
            return View("Views/Admin/Systems/DanhMucChucNang/Index2.cshtml", chucnang);
        }

        public IActionResult Delete2(string Maso_delete, string Maso_goc_delete)
        {
            var model = _db.DanhMucChucNang.FirstOrDefault(t => t.Maso == Maso_delete);
            _db.DanhMucChucNang.Remove(model);
            _db.SaveChanges();
            ViewData["maso1"] = Maso_goc_delete;
            var tenchucnang = _db.DanhMucChucNang.Where(t => t.Maso == Maso_goc_delete).Select(cn => cn.Menu).FirstOrDefault();
            ViewData["dschucnang"] = tenchucnang;

            var chucnang1 = _db.DanhMucChucNang.Where(t => t.Maso_goc == Maso_goc_delete).ToList();
            return View("Views/Admin/Systems/DanhMucChucNang/Index2.cshtml", chucnang1);
        }




        //Menu3
        public IActionResult Index3(string maso2)
        {
            ViewData["maso2"] = maso2;
            var tenchucnang = _db.DanhMucChucNang.Where(cn => cn.Maso == maso2).Select(cn => cn.Menu).FirstOrDefault();
            ViewData["dschucnang2"] = tenchucnang;
            var model = _db.DanhMucChucNang.Where(t => t.Maso_goc == maso2).ToList();
            return View("Views/Admin/Systems/DanhMucChucNang/Index3.cshtml", model);
        }

        public IActionResult Store3(CSDLGia_ASP.Models.Systems.DanhMucChucNang request)
        {
            var model = new CSDLGia_ASP.Models.Systems.DanhMucChucNang
            {
                Maso = request.Maso,
                Capdo = request.Capdo,
                Maso_goc = request.Maso_goc,
                Menu = request.Menu,
                Mota = request.Mota,
                Created_at = request.Created_at,
                Updated_at = request.Updated_at,
            };
            ViewData["maso2"] = request.Maso_goc;
            _db.DanhMucChucNang.Add(model);
            _db.SaveChanges();
            var tenchucnang = _db.DanhMucChucNang.Where(cn => cn.Maso == request.Maso_goc).Select(cn => cn.Menu).FirstOrDefault();
            ViewData["dschucnang2"] = tenchucnang;

            var chucnang1 = _db.DanhMucChucNang.Where(t => t.Maso_goc == request.Maso_goc).ToList();
            return View("Views/Admin/Systems/DanhMucChucNang/Index3.cshtml", chucnang1);

        }
        public IActionResult Update3(int Id_edit, string Maso_edit, string Menu_edit, string Mota_edit, string Maso_goc_edit)
        {
            var model = _db.DanhMucChucNang.FirstOrDefault(t => t.Id == Id_edit);

            model.Maso = Maso_edit;
            model.Menu = Menu_edit;
            model.Mota = Mota_edit;
            model.Maso_goc = Maso_goc_edit;
            _db.DanhMucChucNang.Update(model);
            _db.SaveChanges();
            var chucnang = _db.DanhMucChucNang.Where(t => t.Maso_goc == Maso_goc_edit).ToList();

            var tenchucnang = _db.DanhMucChucNang.Where(cn => cn.Maso == Maso_goc_edit).Select(cn => cn.Menu).FirstOrDefault();
            ViewData["dschucnang2"] = tenchucnang;
            ViewData["maso1"] = Maso_goc_edit;
            return View("Views/Admin/Systems/DanhMucChucNang/Index3.cshtml", chucnang);
        }

        public IActionResult Delete3(string Maso_delete, string Maso_goc_delete)
        {
            var model = _db.DanhMucChucNang.FirstOrDefault(t => t.Maso == Maso_delete);
            _db.DanhMucChucNang.Remove(model);
            _db.SaveChanges();
            ViewData["maso1"] = Maso_goc_delete;
            var tenchucnang = _db.DanhMucChucNang.Where(t => t.Maso == Maso_goc_delete).Select(cn => cn.Menu).FirstOrDefault();
            ViewData["dschucnang"] = tenchucnang;

            var chucnang1 = _db.DanhMucChucNang.Where(t => t.Maso_goc == Maso_goc_delete).ToList();
            return View("Views/Admin/Systems/DanhMucChucNang/Index3.cshtml", chucnang1);
        }
    }
}
