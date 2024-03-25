using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaoDucDaoTao
{
    public class GiaoDucDaoTaoNhomController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaoDucDaoTaoNhomController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [HttpGet("DanhMucNhomGiaGiaoDucDaoTao")]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.danhmuc", "Index"))
                {
                    var model = _db.GiaDvGdDtNhom;
                    if (model.Any())
                    {
                        ViewData["SapXep"] = model.Max(t => t.SapXep) + 1;
                    }
                    else
                    {
                        ViewData["SapXep"] = 1;
                    }
                    ViewData["Title"] = "Danh mục giá dịch vụ giáo dục đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/DanhMuc/NhomDM.cshtml", model);
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

        [HttpPost]
        public IActionResult Store(string MaNhom_create, string TenNhom_create, int SapXep_create) {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.danhmuc", "Create"))
                {
                    var model = new GiaDvGdDtNhom
                    {
                        MaNhom = MaNhom_create,
                        TenNhom = TenNhom_create,
                        SapXep = SapXep_create
                    };
                    _db.GiaDvGdDtNhom.Add(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaoDucDaoTaoNhom");                    
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

        [Route("DanhMucNhomGiaGiaoDucDaoTao/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaDvGdDtNhom.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã nhóm*</b></label>";
                result += "<input type='text' id='MaNhom_edit' name='MaNhom_edit' value='" + model.MaNhom + "' class='form-control'/>";
                result += "</div></div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên nhóm</b></label>";
                result += "<input type='text' id='TenNhom_edit' name='TenNhom_edit' value='" + model.TenNhom + "' class='form-control'/>";
                result += "<input hidden id='id_edit' name='id_edit'  value='" + model.Id + "' /> ";
                result += "</div></div>";
                result += "</div></div>";


                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }


        [Route("DanhMucNhomGiaGiaoDucDaoTao/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string MaNhom, string TenNhom)
        {
            var model = _db.GiaDvGdDtNhom.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                model.MaNhom = MaNhom;
                model.TenNhom = TenNhom;

                var data = new { status = "success", message = "Ok" };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.danhmuc", "Delete"))
                {
                    var model = _db.GiaDvGdDtNhom.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaDvGdDtNhom.Remove(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaoDucDaoTaoNhom");
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
