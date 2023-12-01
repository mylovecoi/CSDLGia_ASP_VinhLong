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
        public JsonResult Store(string Mahs, string Maloaidat, int Vitri, double Banggiadat, double Giacuthe, double Hesodc, string Khuvuc)
        {
            var model = new GiaDatPhanLoaiCt
            {
                Mahs = Mahs,
                Khuvuc = Khuvuc,
                Maloaidat = Maloaidat,
                Vitri = Vitri,
                Banggiadat = Banggiadat,
                Giacuthe = Giacuthe,
                Hesodc = Hesodc,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
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
                result += "<input type='text' id='tenduong_edit' name='tenduong_edit' value='" + @model.Khuvuc + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Loại đất</b></label>";
                result += "<select class='form-control' id='mld_edit' name='mld_edit'>";
                var dmloaidat = _db.DmLoaiDat.ToList();
                foreach (var item in dmloaidat)
                {
                    /*result+="<option value ='"+@item.Maloaidat+"'>"+@item.Loaidat+"</ option >";*/
                    result += "<option value='" + item.Maloaidat + "' " + ((string)model.Maloaidat == item.Maloaidat ? "selected" : "") + ">" + item.Loaidat + "</option>";
                }
                result += "</select>";
                result += "</div>";
                result += "</div>";

                result += "</div>";//end row

                result += "<div class='row'>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Vị trí</b></label>";
                result += "<select id='vt_edit' name='vt_edit' class='form-control'>";
                result += "<option value='1' " + (model.Vitri == 1 ? "selected" : "") + ">1</option>";
                result += "<option value='2' " + (model.Vitri == 2 ? "selected" : "") + ">2</option>";
                result += "<option value='3' " + (model.Vitri == 3 ? "selected" : "") + ">3</option>";
                result += "<option value='4' " + (model.Vitri == 4 ? "selected" : "") + ">4</option>";
                result += "<option value='5' " + (model.Vitri == 5 ? "selected" : "") + ">5</option>";
                result += "</select>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá tại bảng giá</b></label>";
                result += "<input type='text' id='giabg_edit' name='giabg_edit' class='form-control money text-right' style='font-weight: bold' value='" + @model.Banggiadat + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá đất cụ thể</b></label>";
                result += "<input type='text' id='giact_edit' name='giact_edit' class='form-control money text-right' style='font-weight: bold' value='" + @model.Giacuthe + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Hệ số điều chỉnh</b></label>";
                result += "<input type='number' step='any' id='hs_edit' name='hs_edit' class='form-control money text-right' style='font-weight: bold' value='" + @model.Hesodc + "'/>";
                result += "</div>";
                result += "</div>";

                result += "</div>"; //end row

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
        public JsonResult Update(int Id, string Mahs, string Maloaidat, int Vitri, double Banggiadat, double Giacuthe, double Hesodc, string Khuvuc)
        {
            var model = _db.GiaDatPhanLoaiCt.FirstOrDefault(t => t.Id == Id);
            model.Khuvuc = Khuvuc;
            model.Maloaidat = Maloaidat;
            model.Vitri = Vitri;
            model.Banggiadat = Banggiadat;
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
            int record_id = 1;

            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th>STT</th>";
            result += "<th>Tên đường, giới hạn, khu vực</th>";
            result += "<th>Loại đất</th>";
            result += "<th>Vị trí</th>";
            result += "<th>Giá đất tại bảng giá</th>";
            result += "<th>Giá đất cụ thể</th>";
            result += "<th>Hệ số điều chỉnh</th>";
            result += "<th>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";
            foreach (var item in model)
            {
                result += "<tr tyle='text-align:center'> >";
                result += "<td style='text-align:center'>" + record_id++ + "</td>";
                result += "<td style='text-align:center'>" + item.Khuvuc + "</td>";
                result += "<td style='text-align:center'>";
                foreach (var ten in dmloaidat)
                {
                    if (ten.Maloaidat == item.Maloaidat)
                    {
                        result += "<span>" + ten.Loaidat + "</span>";
                    }
                }
                result += "</td>";
                result += "<td style='text-align:center' >" + item.Vitri + "</td>";
                result += "<td style='text-align:center; font-weight: bold'>" + Helpers.ConvertDbToStr(item.Banggiadat) + "</td>";
                result += "<td style='text-align:center; font-weight: bold'>" + Helpers.ConvertDbToStr(item.Giacuthe) + "</td>";
                result += "<td style='text-align:center; font-weight: bold'>" + Helpers.ConvertDbToStr(item.Hesodc) + "</td>";
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
