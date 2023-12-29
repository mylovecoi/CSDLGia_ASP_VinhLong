using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTaiSanTths
{
    public class GiaTaiSanTthsCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaTaiSanTthsCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        [Route("GiaTaiSanTthsCt/Store")]
        [HttpPost]
        public JsonResult Store(string Tents, string Taisantd, string Dacdiemkt, string Tinhtrang,
            string Dactinh, double Giabanbuon, double Giabanle, DateTime Thoidiem, double Phantram, string Ghichu, string Mahs)
        {
            var model = new GiaTaiSanTthsCt
            {
                Mahs = Mahs,
                Mataisan = DateTime.Now.ToString("yyMMddssmmHH"),
                Tentaisan = Tents,
                TaisanTd = Taisantd,
                DacdiemKt = Dacdiemkt,
                Tinhtrang = Tinhtrang,
                Dactinh = Dactinh,
                Giabanbuon = Giabanbuon,
                Giabanle = Giabanle,
                Ghichu = Ghichu,
                Thoidiem = Thoidiem,
                Phantram = Phantram,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaTaiSanTthsCt.Add(model);
            _db.SaveChanges();
            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        public string GetData(string Mahs)
        {
            var model = _db.GiaTaiSanTthsCt.Where(t => t.Mahs == Mahs).ToList();

            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th rowspan='2' width='2%'>STT</th>";
            result += "<th rowspan='2' width='17%'>Tên tài khoản</th>";
            result += "<th rowspan='2' width='17%'>Tài sản cùng loại tương đương</th>";
            result += "<th rowspan='2' width='17%'>Các thông số kĩ thuật tài sản</th>";
            result += "<th colspan='2'>Giá tài sản</th>";
            result += "<th rowspan='2' width='15%'>Thời điểm</th>";
            result += "<th rowspan='2'>Ghi chú</th>";
            result += "<th rowspan='2'>Thao tác</th>";
            result += "</tr>";
            result += "<tr>";
            result += "<th>Giá bán buôn</th>";
            result += "<th>Giá bán lẻ</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + record++ + "</td>";
                result += "<td class='active'>" + item.Tentaisan + "</td>";
                result += "<td>" + item.TaisanTd + "</td>";
                result += "<td style='text-align:center'>" + item.DacdiemKt + "</td>";
                result += "<td style='text-align:right'>" + item.Giabanbuon + "</td>";
                result += "<td style='text-align:right'>" + item.Giabanle + "</td>";
                result += "<td>" + Helpers.ConvertDateTimeToStr(item.Thoidiem) + "</td>";
                result += "<td>" + item.Ghichu + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Xóa'";
                result += " data-target='#Delete_Modal' data-toggle='modal' onclick='SetDelete(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-trash text-danger'></i>";
                result += "</button>";
                result += "</td>";
                result += "</tr>";
            }
            result += "</tbody>";
            result += "</table>";
            result += "</div>";

            return result;

        }
        [Route("GiaTaiSanTthsCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaTaiSanTthsCt.FirstOrDefault(p => p.Id == Id);
            var list = _db.GiaTaiSanTthsCt;
            var tstd = (from t in list group t by t.TaisanTd into grp select new { ts = grp.Key });
            //var ttrang = (from t in list group t by t.Tinhtrang into grp select new { tt=grp.Key});
            var ttrang = Helpers.TinhtrangTs();
            var dtinh = (from t in list group t by t.Dactinh into grp select new { dt = grp.Key });
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên tài sản</b></label>";
                result += "<input type='text' id='tents_edit' name='tents_edit' value='" + model.Tentaisan + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-11'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tài sản cùng loại hoặc tương đương</b></label>";
                result += "<select id='tstd_edit' name='tstd_edit' class='form-control'>";
                result += "<option>--Chọn tài sản tương đương--</option>";
                foreach (var ts in tstd)
                {
                    result += "<option value='" + ts.ts + "'" + (ts.ts == model.TaisanTd ? "selected" : "") + ">" + ts.ts + "</option>";
                }
                result += "</select></div>";
                result += "</div>";
                result += "<div class='col-xl-1'>";
                result += "<label><b></b></label>";
                result += "<button type='button' class='btn btn-default' style='border: rgba(0, 0, 0, 0.1) solid 0.05px' data-target='#TstdEdit_Modal' data-toggle='modal'>";
                result += "<i class='fa fa-plus'></i></button></div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Các thông số kĩ thuật của tài sản</b></label>";
                result += "<input type='text' id='dacdiemkt_edit' name='dacdiemkt_edit' value='" + model.DacdiemKt + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tình trạng</b></label>";
                result += "<select id='tinhtrang_edit' name='tinhtrang_edit' class='form-control'>";
                result += "<option>--Chọn tình trạng--</option>";
                foreach (var tt in ttrang)
                {
                    result += "<option value='" + tt + "'" + (tt == model.Tinhtrang ? "selected" : "") + ">" + tt + "</option>";
                }
                result += "</select></div>";
                result += "</div>";

                result += "<div class='col-xl-11'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đặc tính</b></label>";
                result += "<select id='dactinh_edit' name='dactinh_edit' class='form-control'>";
                result += "<option>--Chọn đặc tính--</option>";
                foreach (var dt in dtinh)
                {
                    result += "<option value='" + dt.dt + "'" + (dt.dt == model.Dactinh ? "selected" : "") + ">" + dt.dt + "</option>";
                }
                result += "</select></div>";
                result += "</div>";
                result += "<div class='col-xl-1'>";
                result += "<label><b></b></label>";
                result += "<button type='button' class='btn btn-default' style='border: rgba(0, 0, 0, 0.1) solid 0.05px' data-target='#DactinhEdit_Modal' data-toggle='modal'>";
                result += "<i class='fa fa-plus'></i></button></div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá bán buôn</b></label>";
                result += "<input type='text' id='banbuon_edit' name='banbuon_edit' value='" + model.Giabanbuon + "' class='form-control money text-right'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá bán lẻ</b></label>";
                result += "<input type='text' id='banle_edit' name='banle_edit' value='" + model.Giabanle + "' class='form-control money text-right'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Phần trăm định giá</b></label>";
                result += "<input type='text' id='phantram_edit' name='phantram_edit' value='" + model.Phantram + "' class='form-control percent text-right'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Thời điểm</b></label>";
                result += "<input type='date' id='thoidiem_edit' name='thoidiem_edit' value='" + Helpers.ConvertDateToStr(model.Thoidiem) + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Ghi chú</b></label>";
                result += "<textarea type='text' id='ghichu_edit' name='ghichu_edit' cols='12' rows='2' class='form-control'>" + model.Ghichu + "</textarea>";
                result += "</div>";
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
        [Route("GiaTaiSanTthsCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Madv, string Tendvcu, string Qccl, string Dvt, double Gialk, double Giakk, string Ghichu)
        {
            var model = _db.KkGiaXmTxdCt.FirstOrDefault(t => t.Id == Id);
            model.Tendvcu = Tendvcu;
            model.Qccl = Qccl;
            model.Dvt = Dvt;
            model.Gialk = Gialk;
            model.Giakk = Giakk;
            model.Ghichu = Ghichu;
            model.Updated_at = DateTime.Now;
            _db.KkGiaXmTxdCt.Update(model);
            _db.SaveChanges();
            string result = GetData(Madv);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        [Route("GiaTaiSanTthsCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaTaiSanTthsCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaTaiSanTthsCt.Remove(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
    }
}
