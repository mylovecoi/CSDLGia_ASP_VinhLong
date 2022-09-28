using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaoDucDaoTao
{
    public class GiaoDucDaoTaoCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaoDucDaoTaoCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        [Route("DinhGiaGdDtCt/Store")]
        [HttpPost]
        public JsonResult Store(string Mahs, string Maspdv, string Namapdung1, double Thanhthi1, double Nongthon1, double Miennui1, string Namapdung2,
            double Thanhthi2, double Nongthon2, double Miennui2, string Namapdung3, double Thanhthi3, double Nongthon3, double Miennui3)
        {
            var model = _db.GiaDvGdDtCt.Where(t => t.Maspdv == Maspdv && t.Mahs == Mahs).FirstOrDefault();

            if (model != null)
            {
                model.Mahs = Mahs;
                model.Namapdung1 = Namapdung1;
                model.Giathanhthi1 = Thanhthi1;
                model.Gianongthon1 = Nongthon1;
                model.Giamiennui1 = Miennui1;

                model.Namapdung2 = Namapdung2;
                model.Giathanhthi2 = Thanhthi2;
                model.Gianongthon2 = Nongthon2;
                model.Giamiennui2 = Miennui2;

                model.Namapdung3 = Namapdung3;
                model.Giathanhthi3 = Thanhthi3;
                model.Gianongthon3 = Nongthon3;
                model.Giamiennui3 = Miennui3;
                _db.GiaDvGdDtCt.Update(model);
                _db.SaveChanges();
            }
            else
            {
                string result2 = GetData(Mahs);
                var data2 = new { status = "success2", message = result2 };
                return Json(data2);
            }
            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }



        [Route("DinhGiaGdDtCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaDvGdDtCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaDvGdDtCt.Remove(model);
            _db.SaveChanges();
            var result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }


        [Route("DinhGiaGdDtCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id, string tunam, string dennam)
        {
            var model = _db.GiaDvGdDtCt.FirstOrDefault(p => p.Id == Id);

            var GiaDvGdDtDm = _db.GiaDvGdDtDm.ToList();
            string Namapdung1 = "";
            string Namapdung2 = "";
            string Namapdung3 = "";
            if (model.Namapdung1 != null || model.Namapdung1 != "")
            {
                var i = 1;

                for (int j = int.Parse(tunam); j <= int.Parse(dennam); j++)
                {
                    int k = j + 1;
                    int n = k - 1;
                    switch (i)
                    {
                        case (1):
                            {
                                Namapdung1 = tunam + '-' + k;
                                break;
                            }
                        case (2):
                            {
                                Namapdung2 = "" + n + '-' + k;
                                break;
                            }
                        case (3):
                            {
                                Namapdung3 = "" + n + '-' + k;
                                break;
                            }
                    }
                    i++;
                }
            }
            else
            {
                Namapdung1 = model.Namapdung1;
                Namapdung2 = model.Namapdung2;
                Namapdung3 = model.Namapdung3;
            }


            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label> Tên dịch vụ </label>";
                result += "<select id = 'Maspdv_edit' name = 'Maspdv_edit' class='form-control'>";
                foreach (var dm in GiaDvGdDtDm)
                {
                    result += "<option value='" + dm.Maspdv + "' " + (model.Maspdv == dm.Maspdv ? "selected" : "") + ">" + dm.Tenspdv + "</option>";
                }
                result += "</select></div></div></div>";
                result += "<div class='row'>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Năm áp dụng 1</b></label>";
                result += "<input type = 'text' id='nam1_edit' name='nam1_edit' value='" + Namapdung1 + "' class='form-control' />";
                result += "</div></div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Khu vực thành thị 1</b></label>";
                result += "<input type='text' id='tt1_edit' name='tt1_edit'  value='" + model.Giathanhthi1 + "' class='form-control money text-right' style='font-weight: bold' />";
                result += "</div></div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Khu vực nông thôn 1</b></label>";
                result += "<input type = 'text' id='nt1_edit' name='nt1_edit'  value='" + model.Gianongthon1 + "' class='form-control  money text-right' style='font-weight: bold' />";
                result += "</div> </div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Khu vực miền núi 1</b></label>";
                result += "<input type = 'text' id='mn1_edit' name='mn1_edit'  value='" + model.Giamiennui1 + "' class='form-control money text-right' style='font-weight: bold' />";
                result += "</div></div></div>";

                result += "<div class='row'>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Năm áp dụng 2</b></label>";
                result += "<input type = 'text' id='nam2_edit' name='nam2_edit'  value='" + Namapdung2 + "' class='form-control' />";
                result += "</div></div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Khu vực thành thị 2</b></label>";
                result += "<input type='text' id='tt2_edit' name='tt2_edit'  value='" + model.Giathanhthi2 + "' class='form-control money text-right' style='font-weight: bold' />";
                result += "</div></div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Khu vực nông thôn 2</b></label>";
                result += "<input type = 'text' id='nt2_edit' name='nt2_edit'  value='" + model.Gianongthon2 + "' class='form-control  money text-right' style='font-weight: bold' />";
                result += "</div> </div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Khu vực miền núi 2</b></label>";
                result += "<input type = 'text' id='mn2_edit' name='mn2_edit'  value='" + model.Giamiennui2 + "' class='form-control money text-right' style='font-weight: bold' />";
                result += "</div></div></div>";

                result += "<div class='row'>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Năm áp dụng 3</b></label>";
                result += "<input type = 'text' id='nam3_edit' name='nam3_edit'  value='" + Namapdung3 + "' class='form-control' />";
                result += "</div></div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Khu vực thành thị 3</b></label>";
                result += "<input type='text' id='tt3_edit' name='tt3_edit'  value='" + model.Giathanhthi3 + "' class='form-control money text-right' style='font-weight: bold' />";
                result += "</div></div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Khu vực nông thôn 3</b></label>";
                result += "<input type = 'text' id='nt3_edit' name='nt3_edit'  value='" + model.Gianongthon3 + "' class='form-control  money text-right' style='font-weight: bold' />";
                result += "</div> </div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Khu vực miền núi 3</b></label>";
                result += "<input type='text' id='mn3_edit' name='mn3_edit'  value='" + model.Giamiennui3 + "' class='form-control money text-right' style='font-weight: bold' />";

                result += "</div></div></div>";
                result += "<input type='hidden' id='Mahs_edit' name='Mahs_edit' value='" + model.Mahs + "'/>";
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
        public JsonResult Update(string Mahs, string Maspdv, string Namapdung1, double Thanhthi1, double Nongthon1, double Miennui1,
        string Namapdung2, double Thanhthi2, double Nongthon2, double Miennui2, string Namapdung3, double Thanhthi3, double Nongthon3, double Miennui3)
        {

            var model = _db.GiaDvGdDtCt.Where(t => t.Mahs == Mahs && t.Maspdv == Maspdv).FirstOrDefault();

            model.Namapdung1 = Namapdung1;
            model.Giathanhthi1 = Thanhthi1;
            model.Gianongthon1 = Nongthon1;
            model.Giamiennui1 = Miennui1;

            model.Namapdung2 = Namapdung2;
            model.Giathanhthi2 = Thanhthi2;
            model.Gianongthon2 = Nongthon2;
            model.Giamiennui2 = Miennui2;

            model.Namapdung3 = Namapdung3;
            model.Giathanhthi3 = Thanhthi3;
            model.Gianongthon3 = Nongthon3;
            model.Giamiennui3 = Miennui3;

            model.Updated_at = DateTime.Now;
            _db.GiaDvGdDtCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetData(string Mahs)
        {
            var Model = _db.GiaDvGdDtCt.Where(t => t.Mahs == Mahs).ToList();
            var GiaDvGdDtDm = _db.GiaDvGdDtDm.ToList();
            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";

            result += "<table class='table table-striped table-bordered table-hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style = 'text-align:center' >";
            result += "<th rowspan='2'> STT </ th >";
            result += "<th rowspan='2'> Tên sản phẩm <br />dịch vụ</th>";
            result += "<td colspan='4'>Mức thu học phí 1</td>";
            result += "<td colspan='4'>Mức thu học phí 2</td>";
            result += "<td colspan='4'>Mức thu học phí 3</td>";
            result += "<th rowspan='2'>Thao tác</th>";
            result += "</tr>";

            result += "<tr style = 'text-align:center' >";
            result += "<th>Năm học</th>";
            result += "<th>Thành thị</th>";
            result += "<th>Nông thôn</th>";
            result += "<th>Miền núi</th>";
            result += "<th>Năm học</th>";
            result += "<th>Thành thị</th>";
            result += "<th>Nông thôn</th>";
            result += "<th>Miền núi</th>";
            result += "<th>Năm học</th>";
            result += "<th>Thành thị</th>";
            result += "<th>Nông thôn</th>";
            result += "<th>Miền núi</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";
            if (Model != null)
            {
                foreach (var item in Model)
                {
                    result += "<tr>";
                    result += "<td style='text-align:center'>" + (record++) + "</td>";
                    if (GiaDvGdDtDm != null)
                    {
                        foreach (var dm in GiaDvGdDtDm)
                        {
                            if (item.Maspdv == dm.Maspdv)
                            {
                                result += "<td>" + dm.Tenspdv + "</td>";
                            }
                        }
                    }


                    result += "<td>" + item.Namapdung1 + "</td>";
                    result += "<td>" + item.Giathanhthi1 + "</td>";
                    result += "<td>" + item.Gianongthon1 + "</td>";
                    result += "<td>" + item.Giamiennui1 + "</td>";

                    result += "<td>" + item.Namapdung2 + "</td>";
                    result += "<td>" + item.Giathanhthi2 + "</td>";
                    result += "<td>" + item.Giathanhthi2 + "</td>";
                    result += "<td>" + item.Gianongthon2 + "</td>";
                    result += "<td>" + item.Giamiennui2 + "</td>";

                    result += "<td>" + item.Namapdung3 + "</td>";
                    result += "<td>" + item.Giathanhthi3 + "</td>";
                    result += "<td>" + item.Gianongthon3 + "</td>";
                    result += "<td>" + item.Giamiennui3 + "</td>";



                    result += "<td>";
                    result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                    result += "data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                    result += "<i class='icon-lg la la-edit text-primary'></i>";
                    result += "</button>";
                    result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Xóa'";
                    result += "data-target='#Delete_Modal' data-toggle='modal' onclick='GetDelete(`" + item.Id + "`)'>";
                    result += "<i class='icon-lg la la-trash text-danger'></i>";
                    result += "</button></td></tr>";
                }
            }

            result += "</ tbody >";
            result += "</ table >";
            result += "</div>";

            return result;
        }
    }
}
