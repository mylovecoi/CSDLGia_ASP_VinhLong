using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class YKienGopYController : Controller
    {

        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public YKienGopYController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            var danhsachykien = _db.YKienGopY.ToList();
            return View("Views/Admin/Systems/YKienGopY/Index.cshtml", danhsachykien);

        }



        public async Task<IActionResult> Store(YKienGopY request, IFormFile Ipf1)
        {

            if (Ipf1 != null && Ipf1.Length > 0)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(Ipf1.FileName);
                string extension = Path.GetExtension(Ipf1.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Upload/File", filename);
                using (var FileStream = new FileStream(path, FileMode.Create))
                {
                    await Ipf1.CopyToAsync(FileStream);
                }
                request.Ipf1 = filename;
            }

            var model = new YKienGopY
            {
                TieuDe = request.TieuDe,
                NoiDung = request.NoiDung,
                Ipf1 = request.Ipf1,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };

            _db.YKienGopY.Add(model);
            _db.SaveChanges();
            var tentailieu = _db.YKienGopY.ToList();
            return View("Views/Admin/Systems/YKienGopY/Index.cshtml", tentailieu);

        }


        public IActionResult Delete(int id)
        {
            var taiLieu = _db.YKienGopY.Find(id);
            if (taiLieu != null)
            {
                // Xóa tệp tin từ thư mục /Upload/File
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload", "File", taiLieu.Ipf1);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // Xóa thông tin tài liệu khỏi cơ sở dữ liệu
                _db.YKienGopY.Remove(taiLieu);
                _db.SaveChanges();
            }

            var tentailieu = _db.YKienGopY.ToList();
            return View("Views/Admin/Systems/YKienGopY/Index.cshtml", tentailieu);
        }
    }
}
