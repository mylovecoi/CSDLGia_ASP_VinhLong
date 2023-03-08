using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.ChiSoGiaTd;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.ChiSoGiaTd;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CSDLGia_ASP.Models.Systems;
using System.ComponentModel;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using OfficeOpenXml.Style;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ChiSoGiaTd
{
    public class ChiSoGiaTdHhController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ChiSoGiaTdHhController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("Danhmuchanghoa")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.hanghoa", "Index") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.ChiSoGiaTdHh.Where(x=>x.Matt=="1");
                    ViewData["model"] = model;
                    ViewData["danhsach"] = _db.ChiSoGiaTd;
                    ViewData["nhom"] = _db.ChiSoGiaTdDm.Where(x=>x.Matt=="1");
                    ViewData["dvtinh"] = _db.DmDvt.ToList();
                    ViewData["Title"] = " Thông tin chi tiết danh mục";
                    ViewData["MenuLv1"] = "menu_csg";
                    ViewData["MenuLv2"] = "menu_csgHh";
                    ViewData["MenuLv3"] = "menu_csgHh_2019";
                    return View("Views/Admin/Manages/ChiSoGiaTd/Hanghoa/Index.cshtml");

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
        [Route("Danhmuchanghoa/Group")]
        [HttpGet]
        public IActionResult Group(string Matt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.hanghoa", "Index") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    List<ChiSoGiaTdHh> model = new List<ChiSoGiaTdHh>();
                    if (string.IsNullOrEmpty(Matt))
                    {
                        model = _db.ChiSoGiaTdHh.Where(x => x.Matt != "1").ToList();
                        ViewData["matt"] = _db.ChiSoGiaTdDm.FirstOrDefault(x => x.Matt != "1").Matt;
                    }
                    else
                    {
                        model = _db.ChiSoGiaTdHh.Where(x => x.Matt == Matt).ToList();
                        ViewData["matt"] = Matt;
                    }
                    ViewData["model"] = model;
                    ViewData["danhsach"] = _db.ChiSoGiaTd;
                    
                    ViewData["listTt"] = _db.ChiSoGiaTdDm.Where(x => x.Matt != "1");
                    ViewData["dvtinh"] = _db.DmDvt.ToList();
                    ViewData["Title"] = " Thông tin chi tiết danh mục";
                    ViewData["MenuLv1"] = "menu_csg";
                    ViewData["MenuLv2"] = "menu_csgHh";
                    ViewData["MenuLv3"] = "menu_csgHh_group";
                    return View("Views/Admin/Manages/ChiSoGiaTd/Hanghoa/Group.cshtml");

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
        private string checkManhom(int tiento, int hauto)
        {
            if (hauto == 9)
            {
                tiento = tiento + 1;
                hauto = 0;
                return Convert.ToString(tiento) + Convert.ToString(hauto);
            }
            else
            {
                hauto += 1;
                return Convert.ToString(tiento) + Convert.ToString(hauto);
            }
        }
        private string getMasogoc(string Masonhom)
        {
            if (Masonhom == "1")
            {
                var list = _db.ChiSoGiaTdDm.OrderByDescending(x => x.Id).FirstOrDefault(x => x.Masonhomhanghoa == Masonhom).Masogoc;
                int tiento = int.Parse(list.Substring(0, 1));
                int hauto = int.Parse(list.Substring(1, 1));
                return checkManhom(tiento, hauto);
            }
            else if (Masonhom == "2")
            {
                var list = _db.ChiSoGiaTdDm.OrderByDescending(x => x.Id).FirstOrDefault(x => x.Masonhomhanghoa == Masonhom).Masogoc;
                /*int tiento = int.Parse(list.Substring(2, 1));
                int hauto = int.Parse(list.Substring(3, 1));
                return list.Substring(0, 2) + checkManhom(tiento, hauto);*/
                int manhom = int.Parse(list.Substring(2, 1));
                return list.Substring(0, 2) + manhom;
            }
            else if (Masonhom == "3")
            {
                var list = _db.ChiSoGiaTdDm.OrderByDescending(x => x.Id).FirstOrDefault(x => x.Masonhomhanghoa == Masonhom).Masogoc;
                int tiento = int.Parse(list.Substring(3, 1));
                int hauto = int.Parse(list.Substring(4, 1));
                return list.Substring(0, 3) + checkManhom(tiento, hauto);
            }
            else
            {
                var list = _db.ChiSoGiaTdDm.OrderByDescending(x => x.Id).FirstOrDefault(x => x.Masonhomhanghoa == Masonhom).Masogoc;
                int tiento = int.Parse(list.Substring(5, 1));
                int hauto = int.Parse(list.Substring(6, 1));
                return list.Substring(0, 5) + checkManhom(tiento, hauto);
            }
        }
        [Route("Danhmuchanghoa/Store")]
        [HttpPost]
        public JsonResult Store(string Masonhom, string Tenhanghoa, string Dvt, string Baocao)
        {
            var takeMasogoc = getMasogoc(Masonhom);
            var model = new ChiSoGiaTdHh
            {
                Masogoc = takeMasogoc,
                Masonhomhanghoa = Masonhom,
                Tenhanghoa = Tenhanghoa,
                Dvt = Dvt,
                Trangthai = Baocao,
                //Nambaocao = Nambaocao,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.ChiSoGiaTdHh.Add(model);
            var donvitinh = _db.DmDvt;
            foreach (var dvt in donvitinh)
            {
                if (Dvt != dvt.Dvt)
                {
                    var newDvt = new DmDvt
                    {
                        Dvt = Dvt,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.DmDvt.Add(newDvt);
                }
                break;
            }
            _db.SaveChanges();
            string result = GetData();
            var data = new { status = "success", message = result };
            return Json(data);
        }
        public string GetData()
        {
            var model = _db.ChiSoGiaTdHh;
            var chisogia = Helpers.NhomChiSoGia();
            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover' id='sample_3'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th rowspan='2'>STT</th>";
            result += "<th colspan='4'>Phân nhóm</th>";
            result += "<th rowspan='2'>Mã số</th>";
            result += "<th rowspan='2'>Tên mặt hàng</th>";
            result += "<th rowspan='2'>Đơn vị tính</th>";
            result += "<th rowspan='2'>Giá gốc</th>";
            result += "<th rowspan='2'>Quyền số thành thị</th>";
            result += "<th rowspan='2'>Quyền số nông thôn</th>";
            result += "<th rowspan='2'>Hiện thị báo cáo</th>";
            result += "<th rowspan='2' width='10%'>Thao tác</th>";
            result += "</tr>";
            result += "<tr>";
            result += "<th>Nhóm I</th>";
            result += "<th>Nhóm II</th>";
            result += "<th>Nhóm II</th>";
            result += "<th>Nhóm IV</th>";
            result += "</tr></thead><tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + (record++) + "</td>";
                foreach (var csg in chisogia)
                {
                    if (csg == item.Masonhomhanghoa)
                    {
                        result += "<td class='text-center active'>-</td>";
                    }
                    else
                    {
                        result += "<td class='text-center'></td>";
                    }
                }
                result += "<td>" + item.Masogoc + "</td>";
                result += "<td>" + item.Tenhanghoa + "</td>";
                result += "<td>" + item.Dvt + "</td>";
                result += "<td>" + item.Trangthai + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-edit btn-icon' title='Sửa'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Xóa'";
                result += " data-target='#Delete_Modal' data-toggle='modal' onclick='SetDelete(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-trash text-danger'></i>";
                result += "</button>";
                result += "</td></tr>";
            }
            result += "<tbody>";
            return result;
        }
        [Route("Danhmuchanghoa/Edit")]
        [HttpPost]
        public JsonResult Edit(int id)
        {
            var model = _db.ChiSoGiaTdHh.FirstOrDefault(x => x.Id == id);
            var donvitinh = _db.DmDvt;
            if (model != null)
            {
                var result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<input type='hidden' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên hàng hoá</b></label>";
                result += "<input type='text' id='tenhanghoa_edit' name='tenhanghoa_edit' value='" + model.Tenhanghoa + "' class='form-control' />";
                result += "</div></div>";
                result += "<div class='col-xl-5'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn vị tính</b></label>";
                result += "<select class='form-control' id='dvtinh_edit' name='dvtinh_edit'>";
                foreach (var dvt in donvitinh)
                {
                    if (dvt.Dvt == model.Dvt)
                    {
                        result += "<option value='" + dvt.Dvt + "' selected>" + dvt.Dvt + "</option>";
                    }
                    else
                    {
                        result += "<option value='" + dvt.Dvt + "'>" + dvt.Dvt + "</option>";
                    }
                }
                result += "</select>";
                result += "</div></div>";
                result += "<div class='col-xl-1' style='margin-top:25px'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<button type='button' class='btn btn-default' data-target='#dvtinhModalEdit' data-toggle='modal'>";
                result += "<i class='fa fa-plus'></i>";
                result += "</button>";
                result += "</div></div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Hiển thị báo cáo</b></label>";
                result += "<select class='form-control' id='baocao_edit' name='baocao_edit'>";
                if (model.Trangthai == "Đang theo dõi")
                {
                    result += "<option value='Đang theo dõi' selected>Đang theo dõi</option>";
                    result += "<option value='Không theo dõi'>Không theo dõi</option>";
                }
                else
                {
                    result += "<option value='Không theo dõi' selected>Không theo dõi</option>";
                    result += "<option value='Đang theo dõi'>Đang theo dõi</option>";
                }
                result += "</div></div>";
                result += "</select>";
                result += "</div></div></div>";
                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa" };
                return Json(data);
            }
        }
        [Route("Danhmuchanghoa/Update")]
        [HttpPost]
        public JsonResult Update(int Id,string Tenhanghoa, string Dvt, string Baocao)
        {
            var model = _db.ChiSoGiaTdHh.FirstOrDefault(x => x.Id == Id);
            model.Tenhanghoa = Tenhanghoa;
            model.Dvt = Dvt;
            model.Trangthai = Baocao;
            //model.Nambaocao = Nambaocao;
            _db.ChiSoGiaTdHh.Update(model);
            var donvitinh = _db.DmDvt;
            foreach (var dvt in donvitinh)
            {
                if (Dvt != dvt.Dvt)
                {
                    var newDvt = new DmDvt
                    {
                        Dvt = Dvt,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.DmDvt.Add(newDvt);
                }
                break;
            }
            _db.SaveChanges();
            string result = GetData();
            var data = new { status = "success", message = result };
            return Json(data);
        }
        [Route("Danhmuchanghoa/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.ChiSoGiaTdHh.FirstOrDefault(t => t.Id == Id);
            _db.ChiSoGiaTdHh.Remove(model);
            _db.SaveChanges();
            var result = GetData();
            var data = new { status = "success", message = result };
            return Json(data);
        }
        [Route("Danhmuchanghoa/Import")]
        [HttpPost]
        public async Task<JsonResult> Import(string Mahanghoa, string Tennhomhang, string Dvt, string Magoc,
              string Nam,string Matt,double Gia,int Sheet, int LineStart, int LineStop, IFormFile FormFile)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.danhmuc", "Edit"))
                {
                    LineStart = LineStart == 0 ? 1 : LineStart;
                    var list_add = new List<ChiSoGiaTdHh>();
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
                            for (int row = LineStart; row <= LineStop; row++)
                            {
                                list_add.Add(new ChiSoGiaTdHh
                                {
                                    Created_at = DateTime.Now,
                                    Updated_at = DateTime.Now,
                                    Nam = Nam,
                                    Matt=Matt,
                                    Masohanghoa = worksheet.Cells[row, Int16.Parse(Mahanghoa)].Value.ToString() != null ?
                                                worksheet.Cells[row, Int16.Parse(Mahanghoa)].Value.ToString().Trim() : "",
                                    Tenhanghoa = worksheet.Cells[row, Int16.Parse(Tennhomhang)].Value.ToString() != null ?
                                                worksheet.Cells[row, Int16.Parse(Tennhomhang)].Value.ToString().Trim() : "",

                                    Dvt = worksheet.Cells[row, Int16.Parse(Dvt)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Dvt)].Value.ToString().Trim() : "",

                                    Masogoc = worksheet.Cells[row, Int16.Parse(Magoc)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Magoc)].Value.ToString().Trim() : "",

                                    Gia = Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Gia.ToString())].Value) != 0 ?
                                                Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Gia.ToString())].Value) : 0,

                                }); ;
                            }

                        }
                    }
                    _db.ChiSoGiaTdHh.AddRange(list_add);
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
        [Route("Danhmuchanghoa/Export")]
        [HttpGet]
        public IActionResult Export()
        {
            var model = _db.ChiSoGiaTdHh;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Danh sách hàng hóa");
            workSheet.Cells.Style.Font.Name = "Times New Roman";
            workSheet.Cells.Style.Font.Size = 12;

            workSheet.Cells[1, 1].Value = "Tên hàng hoá";
            workSheet.Cells[1, 2].Value = "Mã số gốc";
            workSheet.Cells[1, 3].Value = "Đơn vị tính";
            workSheet.Cells[1, 4].Value = "Mã số hàng hoá";
            workSheet.Cells[1, 5].Value = "Giá";
            workSheet.Cells[1, 1, 1, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[1, 1, 1, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[1, 1, 1, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[1, 1, 1, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            int record_index = 2;
            foreach (var item in model)
            {
                workSheet.Cells[record_index, 1].Value = item.Tenhanghoa;
                workSheet.Cells[record_index, 2].Value = item.Masogoc;
                workSheet.Cells[record_index, 3].Value = item.Dvt;
                workSheet.Cells[record_index, 4].Value = item.Masohanghoa;
                workSheet.Cells[record_index, 5].Value = 0;
                workSheet.Cells[record_index, 1, record_index, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[record_index, 1, record_index, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[record_index, 1, record_index, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[record_index, 1, record_index, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                record_index++;
            }
            workSheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(2).Style.Font.Bold = true;
            workSheet.Cells.AutoFitColumns();
            workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            string excelName = "mauHangHoa" + ".xlsx";
            using (var stream = new MemoryStream())
            {
                excel.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }

        }
    }
}
