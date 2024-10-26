using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.IdentityModel.Tokens;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatCuThe
{
    public class GiaDatCuTheVlCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatCuTheVlCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaDatCuTheVlCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaDatCuTheVlCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                List<string> list_style = !string.IsNullOrEmpty(model.Style) ? new List<string>(model.Style.Split(',')) : new List<string>();
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row'>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Chi phí nhân công:</label>";
                result += "<input type='text' id='chiphinhancong_edit' name='chiphinhancong_edit' class='form-control money-decimal-mask' style='font-weight: bold' value='" + Helpers.ConvertDbToStr(model.ChiPhiNhanCong) + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Chi phí dụng cụ:</label>";
                result += "<input type='text' id='chiphidungcu_edit' name='chiphidungcu_edit' class='form-control money-decimal-mask' style='font-weight: bold' value='" + Helpers.ConvertDbToStr(model.ChiPhiDungCu) + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<label style='text-decoration-line: underline; font-weight: bold'>Chi phí thiết bị:</label>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Chi phí năng lượng:</label>";
                result += "<input type='text' id='chiphinangluong_edit' name='chiphinangluong_edit' class='form-control money-decimal-mask' style='font-weight: bold' value='" + Helpers.ConvertDbToStr(model.ChiPhiNangLuong) + "'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Chi phí khấu hao:</label>";
                result += "<input type='text' id='chiphikhauhao_edit' name='chiphikhauhao_edit' class='form-control money-decimal-mask' style='font-weight: bold' value='" + Helpers.ConvertDbToStr(model.ChiPhiKhauHao) + "'/>";
                result += "</div>";
                result += "</div>";

                result += "</div>";

                result += "<div class='row'>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Chi phí vật liệu:</label>";
                result += "<input type='text' id='chiphivatlieu_edit' name='chiphivatlieu_edit' class='form-control money-decimal-mask' style='font-weight: bold' value='" + Helpers.ConvertDbToStr(model.ChiPhiVatLieu) + "'/>";
                result += "</div>";
                result += "</div>";

                result += "</div>";

                result += "<div class='row'>";

                result += "<div class='col-xl-12'>";
                result += "<label style='text-decoration-line: underline; font-weight: bold'>Chi phí trực tiếp:</label>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Không có chi phí khấu hao:</label>";
                result += "<input type='text' id='chiphitructiepkkh_edit' name='chiphitructiepkkh_edit' class='form-control money-decimal-mask' style='font-weight: bold' value='" + Helpers.ConvertDbToStr(model.ChiPhiTrucTiepKkh) + "'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Có chi phí khấu hao:</label>";
                result += "<input type='text' id='chiphitructiepckh_edit' name='chiphitructiepckh_edit' class='form-control money-decimal-mask' style='font-weight: bold' value='" + Helpers.ConvertDbToStr(model.ChiPhiTrucTiepCkh) + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<label style='text-decoration-line: underline; font-weight: bold'>Chi phí quản lý chung (Nội nghiệp 15%; Ngoại nghiệp 20%) :</label>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Không có chi phí khấu hao:</label>";
                result += "<input type='text' id='chiphiqlchungkkh_edit' name='chiphiqlchungkkh_edit' class='form-control money-decimal-mask' style='font-weight: bold' value='" + Helpers.ConvertDbToStr(model.ChiPhiQlChungKkh) + "'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Có chi phí khấu hao:</label>";
                result += "<input type='text' id='chiphiqlchungckh_edit' name='chiphiqlchungckh_edit' class='form-control money-decimal-mask' style='font-weight: bold' value='" + Helpers.ConvertDbToStr(model.ChiPhiQlChungCkh) + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<label style='text-decoration-line: underline; font-weight: bold'>Đơn giá:</label>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Không có chi phí khấu hao:</label>";
                result += "<input type='text' id='dongiakkh_edit' name='dongiakkh_edit' class='form-control money-decimal-mask' style='font-weight: bold' value='" + Helpers.ConvertDbToStr(model.DonGiaKkh) + "'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Có chi phí khấu hao:</label>";
                result += "<input type='text' id='dongiackh_edit' name='dongiackh_edit' class='form-control money-decimal-mask' style='font-weight: bold' value='" + Helpers.ConvertDbToStr(model.DonGiaCkh) + "'/>";
                result += "</div>";
                result += "</div>";

                result += "</div>";

                result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";
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

        [Route("GiaDatCuTheVlCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, double Chiphinhancong, double Chiphidungcu, double Chiphinangluong, double Chiphikhauhao, double Chiphivatlieu, 
            double Chiphitructiepkkh, double Chiphitructiepckh, double Chiphiqlchungkkh, double Chiphiqlchungckh, double Dongiakkh, double Dongiackh)
        {
            var model = _db.GiaDatCuTheVlCt.FirstOrDefault(t => t.Id == Id);

            model.ChiPhiNhanCong = !string.IsNullOrEmpty(Chiphinhancong.ToString()) ? Chiphinhancong : 0;
            model.ChiPhiDungCu = !string.IsNullOrEmpty(Chiphidungcu.ToString()) ? Chiphidungcu : 0;
            model.ChiPhiNangLuong = !string.IsNullOrEmpty(Chiphinangluong.ToString()) ? Chiphinangluong : 0;
            model.ChiPhiKhauHao = !string.IsNullOrEmpty(Chiphikhauhao.ToString()) ? Chiphikhauhao : 0;
            model.ChiPhiVatLieu = !string.IsNullOrEmpty(Chiphivatlieu.ToString()) ? Chiphivatlieu : 0;
            model.ChiPhiTrucTiepKkh = !string.IsNullOrEmpty(Chiphitructiepkkh.ToString()) ? Chiphitructiepkkh : 0;
            model.ChiPhiTrucTiepCkh = !string.IsNullOrEmpty(Chiphitructiepckh.ToString()) ? Chiphitructiepckh : 0;
            model.ChiPhiQlChungKkh = !string.IsNullOrEmpty(Chiphiqlchungkkh.ToString()) ? Chiphiqlchungkkh : 0;
            model.ChiPhiQlChungCkh = !string.IsNullOrEmpty(Chiphiqlchungckh.ToString()) ? Chiphiqlchungckh : 0;
            model.DonGiaKkh = !string.IsNullOrEmpty(Dongiakkh.ToString()) ? Dongiakkh : 0;
            model.DonGiaCkh = !string.IsNullOrEmpty(Dongiackh.ToString()) ? Dongiackh : 0;
            model.Updated_at = DateTime.Now;
            _db.GiaDatCuTheVlCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("GiaDatCuTheVlCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaDatCuTheVlCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaDatCuTheVlCt.Remove(model);
            _db.SaveChanges();
            var result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetData(string Mahs)
        {
            var model = _db.GiaDatCuTheVlCt.Where(t => t.Mahs == Mahs).ToList();
            var model_dm = _db.GiaDatCuTheVlDmPPDGDat;
            string result = "<div class='card-body' id='frm_data'>";
            foreach (var dm in model_dm)
            {
                var data = model.Where(t => t.Mapp == dm.Mapp);
                if (data.Any())
                {
                    result += "<p style='text-align:center; font-size:16px; text-transform:uppercase; font-weight:bold'>ĐƠN GIÁ DỊCH VỤ ĐỊNH GIÁ ĐẤT CỤ THỂ THEO PHƢƠNG PHÁP " + dm.Tenpp + "</p>";

                    result += "<table class='table table-striped table-bordered table-hover class-nosort'>";
                    result += "<thead>";
                    result += "<tr>";
                    result += "<th rowspan='2' style='text-align:center' width='2%'>STT</th>";
                    result += "<th rowspan='2' style='text-align:center'>Nội dung công việc</th>";
                    result += "<th rowspan='2' style='text-align:center'>Chi phí<br />nhân<br />công</th>";
                    result += "<th rowspan='2' style='text-align:center'>Chi phí<br />dụng cụ</th>";
                    result += "<th colspan='2' style='text-align:center'>Chi phí thiết bị</th>";
                    result += "<th rowspan='2' style='text-align:center'>Chi phí<br />vật liệu</th>";
                    result += "<th colspan='2' style='text-align:center'>Chi phí trực tiếp</th>";
                    result += "<th colspan='2' style='text-align:center'>Chi phí quản<br />lí chung(Nội nghiệp<br />15%;Ngoại nghiệp 20%)</th>";
                    result += "<th colspan='2' style='text-align:center'>Đơn giá</th>";
                    result += "<th rowspan='2' width='5%' style='text-align:center'>Thao tác</th>";
                    result += "</tr>";
                    result += "<tr>";
                    result += "<th style='text-align:center'>Chi phí<br />năng<br />lượng</th>";
                    result += "<th style='text-align:center'>Chi phí<br />khấu hao</th>";
                    result += "<th style='text-align:center'>Không có<br />chi phí<br />khấu hao</th>";
                    result += "<th style='text-align:center'>Có chi<br />phí khấu<br />hao</th>";
                    result += "<th style='text-align:center'>Không có<br />chi phí<br />khấu hao</th>";
                    result += "<th style='text-align:center'>Có chi<br />phí khấu<br />hao</th>";
                    result += "<th style='text-align:center'>Không có<br />chi phí<br />khấu hao</th>";
                    result += "<th style='text-align:center'>Có chi<br />phí khấu<br />hao</th>";
                    result += "</tr>";
                    result += "</thead>";
                    result += "<tbody>";

                    foreach (var item in data.OrderBy(x => x.STTSapXep))
                    {
                        string HtmlStyle = Helpers.ConvertStrToStyle(item.Style);
                        result += "<tr>";
                        result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.STTHienThi + "</td>";
                        result += "<td style='text-align:left;" + HtmlStyle + "'>" + item.Noidungcv + "</td>";
                        result += "<td style='text-align:right;" + HtmlStyle + "'>" + Helpers.ConvertDbToStr(item.ChiPhiNhanCong) + "</td>";
                        result += "<td style='text-align:right;" + HtmlStyle + "'>" + Helpers.ConvertDbToStr(item.ChiPhiDungCu) + "</td>";
                        result += "<td style='text-align:right;" + HtmlStyle + "'>" + Helpers.ConvertDbToStr(item.ChiPhiNangLuong) + "</td>";
                        result += "<td style='text-align:right;" + HtmlStyle + "'>" + Helpers.ConvertDbToStr(item.ChiPhiKhauHao) + "</td>";
                        result += "<td style='text-align:right;" + HtmlStyle + "'>" + Helpers.ConvertDbToStr(item.ChiPhiVatLieu) + "</td>";
                        result += "<td style='text-align:right;" + HtmlStyle + "'>" + Helpers.ConvertDbToStr(item.ChiPhiTrucTiepKkh) + "</td>";
                        result += "<td style='text-align:right;" + HtmlStyle + "'>" + Helpers.ConvertDbToStr(item.ChiPhiTrucTiepCkh) + "</td>";
                        result += "<td style='text-align:right;" + HtmlStyle + "'>" + Helpers.ConvertDbToStr(item.ChiPhiQlChungKkh) + "</td>";
                        result += "<td style='text-align:right;" + HtmlStyle + "'>" + Helpers.ConvertDbToStr(item.ChiPhiQlChungCkh) + "</td>";
                        result += "<td style='text-align:right;" + HtmlStyle + "'>" + Helpers.ConvertDbToStr(item.DonGiaKkh) + "</td>";
                        result += "<td style='text-align:right;" + HtmlStyle + "'>" + Helpers.ConvertDbToStr(item.DonGiaCkh) + "</td>";
                        result += "<td>";
                        result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                        result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                        result += "<i class='icon-lg la la-edit text-primary'></i>";
                        result += "</button>";
                        result += "</td></tr>";
                    }
                    result += "</tbody>";
                    result += "</table>";
                    result += "<hr width='70%' align='center' />";
                }
            }
            result += "</div>";
            return result;
        }
    }
}
