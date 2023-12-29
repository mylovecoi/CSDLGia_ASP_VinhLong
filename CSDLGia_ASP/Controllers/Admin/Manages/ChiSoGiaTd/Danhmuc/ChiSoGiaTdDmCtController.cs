using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Manages.ChiSoGiaTd;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ChiSoGiaTd
{
    public class ChiSoGiaTdDmCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ChiSoGiaTdDmCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhSachNhomChiSoGiaTieuDungCt")]
        [HttpGet]
        public IActionResult Index(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.danhmuc.chitiet", "Index") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    //var model = _db.ChiSoGiaTdDmCt.Where(x => x.Mahs == Mahs);
                    /*var model = (from cpiDmCt in _db.ChiSoGiaTdDmCt.Where(x=>x.Mahs==Mahs)
                                 join cpiDm in _db.ChiSoGiaTdDm on cpiDmCt.Masohanghoa equals cpiDm.Masohanghoa
                                 select new VMChiSoGiaTdDm { 
                                    Id=cpiDmCt.Id,
                                    Masogoc=cpiDm.Masogoc,
                                    Tenhanghoa=cpiDm.Tenhanghoa,
                                    Giagoc=cpiDmCt.Giagoc,
                                    Giakychon=cpiDmCt.Giakychon,
                                    Masonhomhanghoa=cpiDm.Masonhomhanghoa,
                                    Baocao=cpiDm.Baocao,
                                    QuyensoNt=cpiDmCt.QuyensoNt,
                                    QuyensoTt=cpiDmCt.QuyensoTt,
                                 });*/
                    var model = _db.ChiSoGiaTdDmCt.Where(x => x.Mahs == Mahs);
                    var getInfo = _db.ChiSoGiaTd.FirstOrDefault(x => x.Mahs == Mahs);
                    ViewData["dvtinh"] = _db.DmDvt.ToList();
                    ViewData["model"] = model;
                    ViewData["danhsach"] = _db.ChiSoGiaTdHh;
                    ViewData["qs"] = getInfo.Diaphuong;
                    ViewData["Mahs"] = Mahs;
                    ViewData["Title"] = " Thông tin chi tiết danh mục";
                    ViewData["MenuLv1"] = "menu_csg";
                    ViewData["MenuLv2"] = "menu_csgHs";
                    ViewData["MenuLv3"] = "menu_csgHs_ds";
                    return View("Views/Admin/Manages/ChiSoGiaTd/DanhmucCt/Index.cshtml");

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
        [Route("DanhSachNhomChiSoGiaTieuDungCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int id)
        {
            var viewmodel = (from cpi in _db.ChiSoGiaTd
                             join cpiDmCt in _db.ChiSoGiaTdDmCt on cpi.Mahs equals cpiDmCt.Mahs
                             join cpiDm in _db.ChiSoGiaTdDm on cpiDmCt.Masohanghoa equals cpiDm.Masohanghoa
                             select new VMChiSoGiaTdDm
                             {
                                 Id = cpiDmCt.Id,
                                 Masogoc = cpiDm.Masogoc,
                                 Tenhanghoa = cpiDm.Tenhanghoa,
                                 Giagoc = cpiDmCt.Giagoc,
                                 Giakychon = cpiDmCt.Giakychon,
                                 Masonhomhanghoa = cpiDm.Masonhomhanghoa,
                                 Baocao = cpiDm.Baocao,
                                 QuyensoNt = cpiDmCt.QuyensoNt,
                                 QuyensoTt = cpiDmCt.QuyensoTt,
                             });
            var model = viewmodel.FirstOrDefault(x => x.Id == id);
            var getMasodanhmuc = model.Mahs;
            var donvitinh = _db.DmDvt;
            if (model != null)
            {
                var result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<input type='hidden' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
                result += "<input type='hidden' id='mahs_edit' name='mahs_edit' value='" + model.Mahs + "'/>";
                result += "<div class='row'>";
                result += "<div class='col-xl-9'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên hàng hoá</b></label>";
                result += "<input type='text' value='" + model.Tenhanghoa + "' class='form-control' readonly/>";
                result += "</div></div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn vị tính</b></label>";
                result += "<input type='text' value='" + model.Dvt + "' class='form-control' readonly/>";
                result += "</div></div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá gốc</b></label>";
                result += "<input type='text' id='giaGoc_edit' name='giaGoc_edit' value='" + model.Giagoc + "' class='form-control' />";
                result += "</div></div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá kỳ chọn</b></label>";
                result += "<input type='text' id='giaKychon_edit' name='giaKychon_edit' value='" + model.Giakychon + "' class='form-control' />";
                result += "</div></div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Quyền số thành thị</b></label>";
                result += "<input type='number' id='quyensoTt_edit' name='quyensoTt_edit' value='" + model.QuyensoTt + "' class='form-control' />";
                result += "</div></div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Quyền số nông thôn</b></label>";
                result += "<input type='number' id='quyensoNt_edit' name='quyensoNt_edit' value='" + model.QuyensoNt + "' class='form-control' />";
                result += "</div></div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tháng kỳ chọn</b></label>";
                result += "<input type='number' id='thang_edit' name='thang_edit' value='" + model.Thang + "' class='form-control' />";
                result += "</div></div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Năm kỳ chọn</b></label>";
                result += "<input type='number' id='nam_edit' name='nam_edit' value='" + model.Nam + "' class='form-control' />";
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
        [Route("DanhSachNhomChiSoGiaTieuDungCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Mahs, double Giagoc, double GiaKychon, double QuyensoTt, double QuyensoNt, string Thang, string Nam)
        {
            /*var viewmodel = (from cpi in _db.ChiSoGiaTd
                             join cpiDmCt in _db.ChiSoGiaTdDmCt on cpi.Mahs equals cpiDmCt.Mahs
                             join cpiDm in _db.ChiSoGiaTdDm on cpiDmCt.Masohanghoa equals cpiDm.Masohanghoa
                             select new VMChiSoGiaTdDm
                             {
                                 Id = cpiDmCt.Id,
                                 Masogoc = cpiDm.Masogoc,
                                 Tenhanghoa = cpiDm.Tenhanghoa,
                                 Giagoc = cpiDmCt.Giagoc,
                                 Giakychon = cpiDmCt.Giakychon,
                                 Masonhomhanghoa = cpiDm.Masonhomhanghoa,
                                 Baocao = cpiDm.Baocao,
                                 QuyensoNt = cpiDmCt.QuyensoNt,
                                 QuyensoTt = cpiDmCt.QuyensoTt,
                             });*/
            var model = _db.ChiSoGiaTdDmCt.FirstOrDefault(x => x.Id == Id);
            model.Giagoc = Giagoc;
            model.Mahs = Mahs;
            model.Giakychon = GiaKychon;
            model.QuyensoTt = QuyensoTt;
            model.QuyensoNt = QuyensoNt;
            model.Thang = Thang;
            model.Nam = Nam;
            model.Created_at = DateTime.Now;
            model.Updated_at = DateTime.Now;
            _db.SaveChanges();
            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        public string GetData(string Mahs)
        {
            var viewmodel = (from cpi in _db.ChiSoGiaTd
                             join cpiDmCt in _db.ChiSoGiaTdDmCt on cpi.Mahs equals cpiDmCt.Mahs
                             join cpiDm in _db.ChiSoGiaTdDm on cpiDmCt.Masohanghoa equals cpiDm.Masohanghoa
                             select new VMChiSoGiaTdDm
                             {
                                 Id = cpiDmCt.Id,
                                 Masogoc = cpiDm.Masogoc,
                                 Tenhanghoa = cpiDm.Tenhanghoa,
                                 Giagoc = cpiDmCt.Giagoc,
                                 Giakychon = cpiDmCt.Giakychon,
                                 Masonhomhanghoa = cpiDm.Masonhomhanghoa,
                                 Baocao = cpiDm.Baocao,
                                 QuyensoNt = cpiDmCt.QuyensoNt,
                                 QuyensoTt = cpiDmCt.QuyensoTt,
                             });
            //var model = viewmodel.FirstOrDefault(x => x.Mahs == Mahs);
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

            foreach (var item in viewmodel)
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
                result += "<td>" + item.Giagoc + "</td>";
                result += "<td>" + item.QuyensoTt + "</td>";
                result += "<td>" + item.QuyensoNt + "</td>";
                result += "<td>" + item.Baocao + "</td>";
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
        /*[Route("DanhSachNhomChiSoGiaTieuDungCt/Import")]
        [HttpPost]
        public async Task<JsonResult> Import(string Mahanghoa, string Tennhomhang, string Masonhom, string Magoc, double QuyensoNt,
             string QuyensoTt, int Sheet, int LineStart, int LineStop, IFormFile FormFile,string Thang,string Nam,string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.danhmuc", "Edit"))
                {
                    LineStart = LineStart == 0 ? 1 : LineStart;
                    var list_add = new List<ChiSoGiaTdDmCt>();
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
                            int i = 0;
                            for (int row = LineStart; row <= LineStop; row++)
                            {
                                double result = 0;
                                list_add.Add(new ChiSoGiaTdDmCt
                                {
                                    Created_at = DateTime.Now,
                                    Updated_at = DateTime.Now,
                                    Mahs = Mahs,
                                    Thang = Thang,
                                    Nam = Nam,
                                    Masohanghoa = worksheet.Cells[row, Int16.Parse(Mahanghoa)].Value.ToString() != null ?
                                                worksheet.Cells[row, Int16.Parse(Mahanghoa)].Value.ToString().Trim() : "",
                                    Tenhanghoa = worksheet.Cells[row, Int16.Parse(Tennhomhang)].Value.ToString() != null ?
                                                worksheet.Cells[row, Int16.Parse(Tennhomhang)].Value.ToString().Trim() : "",

                                    Masonhomhanghoa = worksheet.Cells[row, Int16.Parse(Masonhom)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Masonhom)].Value.ToString().Trim() : "",

                                    Masogoc = worksheet.Cells[row, Int16.Parse(Magoc)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Magoc)].Value.ToString().Trim() : "",

                                    *//*QuyensoNtString = worksheet.Cells[row, Int16.Parse(QuyensoNt.ToString())].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(QuyensoNt.ToString())].Value.ToString().Trim() : "",*//*
                                    QuyensoNt = Convert.ToDouble(worksheet.Cells[row, Int16.Parse(QuyensoNt.ToString())].Value) != 0 ?
                                                System.Math.Round(Convert.ToDouble(worksheet.Cells[row, Int16.Parse(QuyensoNt.ToString())].Value),5) : 0,

                                    QuyensoTt = Convert.ToDouble(worksheet.Cells[row, Int16.Parse(QuyensoTt.ToString())].Value) != 0 ?
                                                System.Math.Round(Convert.ToDouble(worksheet.Cells[row, Int16.Parse(QuyensoTt.ToString())].Value),5) : 0,
                                }); ; 
                            }

                        }
                    }
                    _db.ChiSoGiaTdDmCt.AddRange(list_add);
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
        }*/
    }
}
