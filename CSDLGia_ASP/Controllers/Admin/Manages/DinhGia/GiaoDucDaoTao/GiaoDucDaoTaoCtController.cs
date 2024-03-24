using CSDLGia_ASP.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaoDucDaoTao
{
    public class GiaoDucDaoTaoCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaoDucDaoTaoCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        [Route("DinhGiaGdDtCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaDvGdDtCt.FirstOrDefault(p => p.Id == Id);

            var GiaDvGdDtDm = _db.GiaDvGdDtDm.ToList();
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Tên dịch vụ </label>";
                result += "<label class='form-control'>" + model.Mota + "</label>";
                result += "</div></div></div>";
                result += "<div class='row'>";
                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Khu vực thành thị 1</b></label>";
                result += "<input type='text' id='tt1_edit' name='tt1_edit'  value='" + model.Giathanhthi1 + "' class='form-control money text-right' style='font-weight: bold' />";
                result += "</div></div>";
                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Khu vực nông thôn 1</b></label>";
                result += "<input type='text' id='nt1_edit' name='nt1_edit'  value='" + model.Gianongthon1 + "' class='form-control  money text-right' style='font-weight: bold' />";
                result += "</div> </div>";
                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Khu vực miền núi 1</b></label>";
                result += "<input type = 'text' id='mn1_edit' name='mn1_edit'  value='" + model.Giamiennui1 + "' class='form-control money text-right' style='font-weight: bold' />";
                result += "</div></div></div>";
                result += "<input type='hidden' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
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
        [Route("DinhGiaGdDtCt/Update")]
        [HttpPost]
        public JsonResult Update(double Thanhthi1, double Nongthon1, double Miennui1, int Id)
        {

            var model = _db.GiaDvGdDtCt.FirstOrDefault(t => t.Id == Id);

            model.Giathanhthi1 = Thanhthi1;
            model.Gianongthon1 = Nongthon1;
            model.Giamiennui1 = Miennui1;

            model.Updated_at = DateTime.Now;
            _db.GiaDvGdDtCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetData(string Mahs)
        {
            var model = _db.GiaDvGdDtCt.Where(t => t.Mahs == Mahs).ToList();
            var model_nhom = _db.GiaDvGdDtNhom;
            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            foreach (var nhom in model_nhom)
            {
                var data = model.Where(t => t.MaNhom == nhom.MaNhom);
                if (data.Any())
                {
                    result += "<p style='text-align:center; font-size:16px; text-transform:uppercase; font-weight:bold'>" + nhom.TenNhom + "</p>";
                    result += "<table class='table table-striped table-bordered table-hover class-nosort'>";
                    result += "<thead>";
                    result += "<tr style = 'text-align:center' >";
                    result += "<th>STT</th >";
                    result += "<th>Tên sản phẩm dịch vụ</th>";
                    result += "<td>Thành thị</td>";
                    result += "<td>Nông thôn</td>";
                    result += "<td>Miền núi</td>";
                    result += "<th>Thao tác</th>";
                    result += "</tr>";
                    result += "</thead>";
                    result += "<tbody>";
                    if (data.Any())
                    {
                        foreach (var item in data.OrderBy(t => t.Id))
                        {
                            result += "<tr>";
                            result += "<td style='text-align:center'>" + (record++) + "</td>";
                            result += "<td style='text-align:left'>" + item.Mota + "</td>";
                            result += "<td style='text-align:right'>" + item.Giathanhthi1 + "</td>";
                            result += "<td style='text-align:right'>" + item.Gianongthon1 + "</td>";
                            result += "<td style='text-align:right'>" + item.Giamiennui1 + "</td>";
                            result += "<td>";
                            result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                            result += "data-target='#Edit_Modal' data-toggle='modal' onclick='GetEdit(`" + item.Id + "`)'>";
                            result += "<i class='icon-lg la la-edit text-primary'></i>";
                            result += "</button>";
                            result += "</td></tr>";
                        }
                    }
                    result += "</tbody>";
                    result += "</table>";
                }
            }
            result += "</div>";

            return result;
        }
    }
}
