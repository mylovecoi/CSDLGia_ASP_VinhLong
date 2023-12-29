using CSDLGia_ASP.Database;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaLePhi
{
    public class GiaLePhiCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaLePhiCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DinhGiaLePhiCt/Store")]
        [HttpPost]
        public JsonResult Store(GiaPhiLePhiCt request)
        {
            var model = new GiaPhiLePhiCt
            {
                Mahs = request.Mahs,
                Ptcp = request.Ptcp,
                Mucthuden = request.Mucthuden,
                Mucthutu = request.Mucthutu,
                Phanloai = request.Phanloai,
                Phantram = request.Phantram,
                Ghichu = "CXD",
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaPhiLePhiCt.Add(model);
            _db.SaveChanges();

            string result = GetData(request.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        [Route("DinhGiaLePhiCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaPhiLePhiCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaPhiLePhiCt.Remove(model);
            _db.SaveChanges();
            var result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        public string GetData(string Mahs)
        {
            var model = _db.GiaPhiLePhiCt.Where(t => t.Mahs == Mahs).ToList();
            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr class='text-center'>";
            result += "<th rowspan='2' width='5%'>STT</th>";
            result += "<th rowspan='2'>Phân loại</th>";
            result += "<th rowspan='2'>Tên phí, lệ phí</th>";
            result += "<th rowspan='2'>Phần<br>trăm</th>";
            result += "<th colspan='2'>Mức thu</th>";
            result += "<th rowspan='2' width='18%'>Thao tác</th>";
            result += "</tr>";
            result += "<tr class='text-center'>";
            result += "<th>Từ</th>";
            result += "<th>Đến</th>";
            result += "</tr>";
            result += "</thead><tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + (record++) + "</td>";
                result += "<td style='text-align:center'>" + item.Phanloai + "</td>";
                result += "<td style='text-align:center'>" + item.Ptcp + "</td>";
                result += "<td style='text-align:center'>" + item.Phantram + "</td>";
                result += "<td style='text-align:center'>" + item.Mucthutu + "</td>";
                result += "<td style='text-align:center'>" + item.Mucthuden + "</td>";
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
            result += "</tbody>";
            return result;
        }
        [Route("DinhGiaLePhiCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var Phanloai = _db.GiaPhiLePhiDm.ToList();
            var model = _db.GiaPhiLePhiCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<input type='Hidden' id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";
                result += "<div class='row'>";
                result += "<div class='col-xl-11'>";
                result += "<label class='form-control-label'><b>Tên phân loại nhóm phí, lệ phí*</b></label>";
                result += "<select type='text' id='Phanloai_edit' name='Phanloai_edit' class='form-control'>";

                foreach (var item in Phanloai)
                {
                    result += " <option value = '" + item.Phanloai + "' >" + item.Phanloai + "</ option >";
                }
                result += "</select>";
                result += "</div>";

                result += " <div class='col-md-1' style='padding-left: 0px'>";
                result += " <label class='control-label'>&nbsp;&nbsp;&nbsp;</label>";
                result += " <button type ='button' class='btn btn-default' style='border:rgba(0, 0, 0, 0.1) solid 0.05px' data-target='#Phanloai_edit_Modal' data-toggle='modal'>";
                result += " <i class='fa fa-plus'></i>";
                result += " </button>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên phí lệ phí</b></label>";
                result += "<input type='text' id='Ptcp_edit' name='Ptcp_edit' value='" + @model.Ptcp + "' class='form-control' />";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Phần trăm</b></label>";
                result += "<input type='text' id='Phantram_edit' name='Phantram_edit' value='" + @model.Phantram + "' class='form-control text-right' style = 'font-weight: bold'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mức thu từ</b></label>";
                result += "<input type='number' id='Mucthutu_edit' name='Mucthutu_edit' value='" + @model.Mucthutu + "' class='form-control money text-right' style = 'font-weight: bold'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mức thu đến</b></label>";
                result += "<input type='number' id='Mucthuden_edit' name='Mucthuden_edit' value='" + @model.Mucthuden + "' class='form-control money text-right' style = 'font-weight: bold'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>";
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
        [Route("DinhGiaLePhiCt/Update")]
        [HttpPost]
        public JsonResult Update(GiaPhiLePhiCt request)
        {
            var model = _db.GiaPhiLePhiCt.FirstOrDefault(t => t.Id == request.Id);
            model.Phanloai = request.Phanloai;
            model.Ptcp = request.Ptcp;
            model.Phantram = request.Phantram;
            model.Mucthutu = request.Mucthutu;
            model.Mucthuden = request.Mucthuden;
            model.Updated_at = DateTime.Now;
            _db.GiaPhiLePhiCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

    }
}
