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

        [Route("DanhMucDvKcb/GetMaSapXep")]
        [HttpPost]
        public JsonResult GetMaSapXep(string Manhom)
        {
            var i = 0;
            var data = _db.GiaDvKcbDm.Where(t => t.Manhom == Manhom);

            if (data.Any())
            {
                i = data.Max(x => x.Sapxep);
            }

            return Json(i);
        }

        [Route("DanhMucDvKcb/Store")]
        [HttpPost]
        public JsonResult Store(string Manhom, string Tenspdv, string Madichvu, string Dvt,  string Ghichu, string Hienthi, int Sapxep)
        {
            var model = new GiaDvKcbDm
            {
                Manhom = Manhom,
                Madichvu = Madichvu,
                Maspdv = Manhom + DateTime.Now.ToString("yyMMddfffssmmHH"),
                Tenspdv = Tenspdv,
                Dvt = Dvt,
                Ghichu = Ghichu,
                Hienthi = Hienthi,
                Sapxep = Sapxep,
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
                result += "<label><b>Mã dịch vụ</b></label>";
                result += "<input id='madichvu_edit' name='madichvu_edit'  value='" + model.Madichvu + "' class='form-control' /> ";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên dịch vụ*</b></label>";
                result += "<input type='text' id='tenspdv_edit' name='tenspdv_edit' value='" + model.Tenspdv + "' class='form-control' required/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Ghi chú</b></label>";
                result += "<input type='text' id='ghichu_edit' name='ghichu_edit' value='" + model.Ghichu + "' class='form-control' rows='2'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Hiển thị</b></label>";
                result += "<input type='text' id='hienthi_edit' name='hienthi_edit' value='" + model.Hienthi + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Sắp xếp</b></label>";
                result += "<input type='number' id='sapxep_edit' name='sapxep_edit' value='" + model.Sapxep + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<input hidden id='id_edit' name='id_edit'  value='" + model.Id + "' /> ";
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

        [Route("DanhMucDvKcb/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Tenspdv, string Madichvu, string Ghichu, string Hienthi, int Sapxep)
        {
            var model = _db.GiaDvKcbDm.FirstOrDefault(t => t.Id == Id);
            model.Tenspdv = Tenspdv;
            model.Madichvu = Madichvu;
            model.Ghichu = Ghichu;
            model.Hienthi = Hienthi;
            model.Sapxep = Sapxep;
            model.Updated_at = DateTime.Now;
            _db.GiaDvKcbDm.Update(model);
            _db.SaveChanges();
            /*if (Dvt != null)
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
            }*/
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
