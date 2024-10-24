using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatCuThe
{
    public class GiaDatCuTheVlDmPPDGDatController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatCuTheVlDmPPDGDatController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaDatCuTheVlDmPPDGDat")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.danhmuc", "Index"))
                {
                    var model = _db.GiaDatCuTheVlDmPPDGDat.ToList();
                    ViewData["Title"] = "Danh mục phương pháp định giá đất cụ thể";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdctvl";
                    ViewData["MenuLv3"] = "menu_dgdctvl_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaDatCuTheVl/DanhMuc/PhuongPhap/Index.cshtml", model);
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

        [Route("GiaDatCuTheVlDmPPDGDat/Store")]
        [HttpPost]
        public JsonResult Store(string Mapp, string Tenpp)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.danhmuc", "Create"))
                {
                    var request = new GiaDatCuTheVlDmPPDGDat
                    {
                        Mapp = Mapp,
                        Tenpp = Tenpp,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaDatCuTheVlDmPPDGDat.Add(request);
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Thêm mới thành công!" };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Bạn không có quyền thực hiện chức năng này!!!" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("GiaDatCuTheVlDmPPDGDat/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.danhmuc", "Edit"))
                {
                    var model = _db.GiaDatCuTheVlDmPPDGDat.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";


                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Mã phương pháp</label>";
                        result += "<input type='text' id='mapp_edit' name='mapp_edit' class='form-control' value='" + model.Mapp + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên phương pháp</label>";
                        result += "<input type='text' id='tenpp_edit' name='tenpp_edit' class='form-control' value='" + model.Tenpp + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
                        result += "</div>";

                        var data = new { status = "success", message = result };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                        return Json(data);
                    }
                }
                else
                {
                    var data = new { status = "error", message = "Bạn không có quyền thực hiện chức năng này!!!" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("GiaDatCuTheVlDmPPDGDat/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Tenpp, string Mapp)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.danhmuc", "Edit"))
                {
                    var model = _db.GiaDatCuTheVlDmPPDGDat.FirstOrDefault(t => t.Id == Id);
                    model.Tenpp = Tenpp;
                    model.Mapp = Mapp;

                    model.Updated_at = DateTime.Now;
                    _db.GiaDatCuTheVlDmPPDGDat.Update(model);
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Cập nhật thành công!" };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Bạn không có quyền thực hiện chức năng này!!!" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("GiaDatCuTheVlDmPPDGDat/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.danhmuc", "Delete"))
                {
                    var model = _db.GiaDatCuTheVlDmPPDGDat.FirstOrDefault(p => p.Id == id_delete);
                    _db.GiaDatCuTheVlDmPPDGDat.Remove(model);

                    var model_ct = _db.GiaDatCuTheVlDmPPDGDatCt.Where(p => p.Mapp == model.Mapp).ToList();
                    _db.GiaDatCuTheVlDmPPDGDatCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDatCuTheVlDmPPDGDat");
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
    }
}
