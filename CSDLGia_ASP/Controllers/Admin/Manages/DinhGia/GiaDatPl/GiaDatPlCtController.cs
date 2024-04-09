using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatPl
{
    public class GiaDatPlCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatPlCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaDatCuTheCt/Store")]
        [HttpPost]
        public JsonResult Store(string MaDv, string Mahs, string Maloaidat, int Vitri, double Banggiadat, double Giacuthe, double Hesodc, string Khuvuc, string Diagioitu, string Diagioiden)
        {
            var model = new GiaDatPhanLoaiCt
            {
                Mahs = Mahs,
                Khuvuc = Khuvuc,
                Maloaidat = Maloaidat,
                Vitri = Vitri,
                Banggiadat = Banggiadat,
                Giacuthe = Giacuthe,
                Diagioitu = Diagioitu,
                Diagioiden = Diagioiden,
                Hesodc = Hesodc,
                Madv = MaDv,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
                Trangthai = "CXD",
            };
            _db.GiaDatPhanLoaiCt.Add(model);
            _db.SaveChanges();
            ViewData["MenuLv1"] = "menu_dg";
            ViewData["MenuLv2"] = "menu_dg_giadatpl";
            ViewData["MenuLv3"] = "menu_dg_giadatpl_tt";
            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("GiaDatCuTheCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaDatPhanLoaiCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";
                result += "<input hidden type='text' id='mahs_edit' name='mahs_edit' value='" + model.Mahs + "' class='form-control'/>";

                result += "<div class='row'>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên đường, giới hạn, khu vực</b></label>";
                result += "<input type='text' id='tenduong_edit' name='tenduong_edit' value='" + model.Khuvuc + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá đất cụ thể</b></label>";
                result += "<input type='text' id='giact_edit' name='giact_edit' class='form-control money text-right' style='font-weight: bold' value='" + model.Giacuthe + "'/>";
                result += "</div>";
                result += "</div>";

                result += "</div>";//end row

                result += "</div>"; // end body


                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }

        [Route("GiaDatCuTheCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Mahs, string Maloaidat, int Vitri, double Banggiadat, double Giacuthe, double Hesodc, string Khuvuc, string Diagioitu, string Diagioiden)
        {
            var model = _db.GiaDatPhanLoaiCt.FirstOrDefault(t => t.Id == Id);
            model.Khuvuc = Khuvuc;
            model.Maloaidat = Maloaidat;
            model.Vitri = Vitri;
            model.Banggiadat = Banggiadat;
            model.Diagioitu = Diagioitu;
            model.Diagioiden = Diagioiden;
            model.Giacuthe = Giacuthe;
            model.Hesodc = Hesodc;
            model.Updated_at = DateTime.Now;
            _db.GiaDatPhanLoaiCt.Update(model);
            _db.SaveChanges();
            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("GiaDatCuTheCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaDatPhanLoaiCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaDatPhanLoaiCt.Remove(model);
            _db.SaveChanges();
            var result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetData(string Mahs)
        {
            var model = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == Mahs).ToList();
            var dmloaidat = _db.DmLoaiDat.ToList();

            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th>#</th>";
            result += "<th>STT</th>";
            result += "<th>Tên đường, giới hạn, khu vực</th>";
            result += "<th>Giá đất cụ thể</th>";
            result += "<th>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";
            foreach (var item in model.OrderBy(t => t.STTSapXep))
            {
                result += "<tr style='text-align:center'>";
                result += "<td style='text-align:center'>" + item.STTSapXep + "</td>";
                result += "<td style='text-align:center'>" + item.STTHienThi + "</td>";
                result += "<td>" + item.Khuvuc + "</td>";
                result += "<td style='text-align:center; font-weight: bold'>" + Helpers.ConvertDbToStr(item.Giacuthe) + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Xóa'";
                result += " data-target='#Delete_Modal' data-toggle='modal' onclick='GetDelete(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-trash text-danger'></i>";
                result += "</button></td></tr>";
            }
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";
            return result;
        }
    }
}
