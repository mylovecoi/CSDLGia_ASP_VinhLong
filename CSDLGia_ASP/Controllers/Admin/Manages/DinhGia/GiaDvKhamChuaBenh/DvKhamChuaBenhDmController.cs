using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDvKhamChuaBenh
{
    public class DvKhamChuaBenhDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DvKhamChuaBenhDmController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhMucDvKcb")]
        [HttpGet]
        public IActionResult Index(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.danhmuc", "Index"))
                {
                    var model = _db.GiaDvKcbDm.Where(t => t.Manhom == Manhom).ToList();
                    var Tennhom = _db.GiaDvKcbNhom.Where(t => t.Manhom == Manhom).FirstOrDefault();
                    ViewData["DmDvt"] = _db.DmDvt.ToList();
                    ViewData["Manhom"] = Manhom;
                    ViewData["Tennhom"] = Tennhom.Tennhom;
                    ViewData["Title"] = "Danh mục giá dịch vụ khám chữa bệnh";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/DanhMuc/Index.cshtml", model);
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


        [Route("DanhMucDvKcb/Store")]
        [HttpPost]
        public JsonResult Store(string Tenspdv, string Madichvu, string Dvt, string Manhom)
        {
            var model = new GiaDvKcbDm
            {
                Tenspdv = Tenspdv,
                Madichvu = Madichvu,
                Dvt = Dvt,
                Manhom = Manhom,
                Maspdv = DateTime.Now.ToString("yyMMddfffssmmHH"),
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaDvKcbDm.Add(model);
            _db.SaveChanges();

            if (Dvt != null)
            {
                var dvt = _db.DmDvt.Where(t => t.Dvt == Dvt).ToList();
                if (dvt.Count == 0)
                {
                    var new_dvt = new DmDvt
                    {
                        Dvt = Dvt,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.DmDvt.AddRange(new_dvt);
                    _db.SaveChanges();
                }
            }

            var data = new { status = "success" };
            return Json(data);
        }


        [Route("DanhMucDvKcb/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaDvKcbDm.FirstOrDefault(t => t.Id == Id);
            _db.GiaDvKcbDm.Remove(model);
            _db.SaveChanges();
            var data = new { status = "success" };
            return Json(data);
        }


        [Route("DanhMucDvKcb/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaDvKcbDm.FirstOrDefault(p => p.Id == Id);
            var DmDvt = _db.DmDvt.ToList();
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn vị tính*</b></label>";
                result += "<input id='madichvu_edit' name='madichvu_edit'  value='" + model.Madichvu + "' class='form-control' /> ";
                result += "</div></div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên dịch vụ*</b></label>";
                result += "<input type='text' id='tenspdv_edit' name='tenspdv_edit' value='" + model.Tenspdv + "' class='form-control' required/>";
                result += "</div></div>";
                result += "<div class='col-xl-11'>";
                result += "<label class='form-control-label'><b>Đơn vị tính*</b></label>";
                result += "<select type='text' id='Dvt_edit' name='Dvt_edit' class='form-control'>";
                result += " <option value = ''> ---Select--- </ option >";
                foreach (var item in DmDvt)
                {
                    result += " <option value = '" + item.Dvt + "' " + (item.Dvt == model.Dvt ? "selected" : "") + ">" + item.Dvt + "</ option >";
                }
                result += "</select>";
                result += "</div>";
                result += " <div class='col-md-1' style='padding-left: 0px'>";
                result += " <label class='control-label'>&nbsp;&nbsp;&nbsp;</label>";
                result += " <button type ='button' class='btn btn-default' style='border:rgba(0, 0, 0, 0.1) solid 0.05px' data-target='#Dvt_edit_Modal' data-toggle='modal'>";
                result += " <i class='fa fa-plus'></i>";
                result += " </button>";
                result += "</div>";
                result += "</div>";

                result += "<input hidden id='id_edit' name='id_edit'  value='" + model.Id + "' /> ";
                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }
        [Route("DanhMucDvKcb/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Tenspdv, string Madichvu, string Dvt)
        {
            var model = _db.GiaDvKcbDm.FirstOrDefault(t => t.Id == Id);
            model.Tenspdv = Tenspdv;
            model.Madichvu = Madichvu;
            model.Dvt = Dvt;
            model.Updated_at = DateTime.Now;
            _db.GiaDvKcbDm.Update(model);
            _db.SaveChanges();
            if (Dvt != null)
            {
                var dvt = _db.DmDvt.Where(t => t.Dvt == Dvt).ToList();
                if (dvt.Count == 0)
                {
                    var new_dvt = new DmDvt
                    {
                        Dvt = Dvt,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.DmDvt.AddRange(new_dvt);
                    _db.SaveChanges();
                }
            }
            var data = new { status = "success" };
            return Json(data);
        }


        [Route("DanhMucDvKcb/Excel")]
        [HttpPost]
        public async Task<JsonResult> Excel(string Manhom, string Madichvu, string Ten, string Phanloai,
            string Dvt, int Sheet, int LineStart, int LineStop, IFormFile FormFile)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.danhmuc", "Edit"))
                {
                    LineStart = LineStart == 0 ? 1 : LineStart;
                    //var list_add = new List<GiaDvKcbDm>();
                    int sheet = Sheet == 0 ? 0 : (Sheet - 1);
                    using (var stream = new MemoryStream())
                    {
                        await FormFile.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
                            var rowcount = worksheet.Dimension.Rows;
                            LineStop = LineStop > rowcount ? rowcount : LineStop;
                            //var n = 1;
                            for (int row = LineStart; row <= LineStop; row++)
                            {
                                var list_add = new GiaDvKcbDm
                                {
                                    Manhom = Manhom,
                                    Created_at = DateTime.Now,
                                    Updated_at = DateTime.Now,
                                    Maspdv = DateTime.Now.ToString("yyMMddfffssmmHH"),

                                    Madichvu = worksheet.Cells[row, Int16.Parse(Madichvu)].Value.ToString() != null ?
                                                worksheet.Cells[row, Int16.Parse(Madichvu)].Value.ToString().Trim() : "",

                                    Tenspdv = worksheet.Cells[row, Int16.Parse(Ten)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Ten)].Value.ToString().Trim() : "",

                                    Phanloai = worksheet.Cells[row, Int16.Parse(Phanloai)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Phanloai)].Value.ToString().Trim() : "",

                                    Dvt = worksheet.Cells[row, Int16.Parse(Dvt)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Dvt)].Value.ToString().Trim() : "",


                                };
                                _db.GiaDvKcbDm.Add(list_add);
                            }

                        }
                    }
                    //_db.GiaDvKcbDm.AddRange(list_add);
                    _db.SaveChanges();

                    var data = new { status = "success" };
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




    }
}
