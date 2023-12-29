using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class DmTaiLieuHuongDanSuDungController : Controller
    {

        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public DmTaiLieuHuongDanSuDungController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            var tentailieu = _db.DmTaiLieuHuongDanSuDung.ToList();
            return View("Views/Admin/Systems/DmTaiLieuHuongDanSuDung/Index.cshtml", tentailieu);

        }

        public async Task<IActionResult> Store(DmTaiLieuHuongDanSuDung request, IFormFile Ipf1)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.thongtin", "Create"))
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

                    var model = new DmTaiLieuHuongDanSuDung
                    {
                        mota = request.mota,
                        Ipf1 = request.Ipf1,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };

                    _db.DmTaiLieuHuongDanSuDung.Add(model);
                    _db.SaveChanges();
                    var tentailieu = _db.DmTaiLieuHuongDanSuDung.ToList();
                    return View("Views/Admin/Systems/DmTaiLieuHuongDanSuDung/Index.cshtml", tentailieu);

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
        }

        //public IActionResult Delete(int id_delete)
        //{
        //    var tailieu_xoa = _db.DmTaiLieuHuongDanSuDung.FirstOrDefault(t => t.Id == id_delete);
        //    _db.DmTaiLieuHuongDanSuDung.Remove(tailieu_xoa);
        //    _db.SaveChanges();
        //    var tentailieu = _db.DmTaiLieuHuongDanSuDung.ToList();
        //    return View("Views/Admin/Systems/DmTaiLieuHuongDanSuDung/Index.cshtml", tentailieu);
        //}
        public IActionResult Delete(int id)
        {
            var taiLieu = _db.DmTaiLieuHuongDanSuDung.Find(id);
            if (taiLieu != null)
            {
                // Xóa tệp tin từ thư mục /Upload/File
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload", "File", taiLieu.Ipf1);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // Xóa thông tin tài liệu khỏi cơ sở dữ liệu
                _db.DmTaiLieuHuongDanSuDung.Remove(taiLieu);
                _db.SaveChanges();
            }

            var tentailieu = _db.DmTaiLieuHuongDanSuDung.ToList();
            return View("Views/Admin/Systems/DmTaiLieuHuongDanSuDung/Index.cshtml", tentailieu);
        }
    }
}
