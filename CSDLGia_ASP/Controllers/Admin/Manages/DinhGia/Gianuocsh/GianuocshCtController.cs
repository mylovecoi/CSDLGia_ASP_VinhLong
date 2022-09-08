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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Gianuocsh
{
    public class GianuocshCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GianuocshCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        public string GetData(string Mahs)
        {
            var model = _db.GiaNuocShCt.Where(t => t.Mahs == Mahs).ToList();
            //var dm=_db.GiaNuocShDm.ToList();
            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table - bordered table - hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th rowspan='2' colspan='1'>STT</th>";
            result += "<th rowspan='2' colspan='1'>Mục đích sử dụng</th>";
            result += "<th rowspan='1' colspan='2'>Đơn giá</th>";
            result += "<th rowspan='1' colspan='2'>Đơn giá</th>";
            result += "<th rowspan='1' colspan='2'>Đơn giá</th>";
            result += "<th rowspan='1' colspan='2'>Đơn giá</th>";
            result += "<th rowspan='1' colspan='2'>Đơn giá</th>";
            result += "<th rowspan='2' colspan='1'>Thao tác</th>";
            result += "</tr>";
            result += "<tr style='text-align:center'>";
            result += "<th rowspan='1' colspan='1'>Năm áp dụng</th>";
            result += "<th rowspan='1' colspan='1'>Giá tiền</th>";
            result += "<th rowspan='1' colspan='1'>Năm áp dụng</th>";
            result += "<th rowspan='1' colspan='1'>Giá tiền</th>";
            result += "<th rowspan='1' colspan='1'>Năm áp dụng</th>";
            result += "<th rowspan='1' colspan='1'>Giá tiền</th>";
            result += "<th rowspan='1' colspan='1'>Năm áp dụng</th>";
            result += "<th rowspan='1' colspan='1'>Giá tiền</th>";
            result += "<th rowspan='1' colspan='1'>Năm áp dụng</th>";
            result += "<th rowspan='1' colspan='1'>Giá tiền</th>";
            result += "</tr></thead><tbody>";

                foreach(var item in model)
                {
                    result += "<tr>";
                    result += "<td style='text-align:center'>" + (record++) + "</td>";
                    result += "<td>" + item.Doituongsd + "</td>";
                    result += "<td>" + item.Namchuathue + "</td>";
                    result += "<td>";
                    result+=item.Giachuathue!=0 ? item.Giachuathue : null;
                    result += "</td>";
                    result += "<td>" + item.Namchuathue1 + "</td>";
                    result += "<td>";
                    result += item.Giachuathue1 != 0 ? item.Giachuathue1 : null;
                    result += "</td>";
                    result += "<td>" + item.Namchuathue2 + "</td>";
                    result += "<td>";
                    result += item.Giachuathue2 != 0 ? item.Giachuathue2 : null;
                    result += "</td>";
                    result += "<td>" + item.Namchuathue3 + "</td>";
                    result += "<td>";
                    result += item.Giachuathue3 != 0 ? item.Giachuathue3 : null;
                    result += "</td>";
                    result += "<td>" + item.Namchuathue4 + "</td>";
                    result += "<td>";
                    result += item.Giachuathue4 != 0 ? item.Giachuathue4 : null;
                    result += "</td>";
                    result += "<td>";
                    result += "<button type='button' data-target='#Edit_Modal' data-toggle='modal'";
                    result += " class='btn btn-primary font-weight-bolder' title='Sửa'";
                    result += " onclick='SetEditAd(`"+ item.Id + "`)'>";
                    result += "<i class='la la-plus'></i>Sửa";
                    result += "</button></td></tr>";
                }
            result += "</tbody></table></div>";
            return result;
        }
        
        [Route("DinhGiaNuocShCt/Update")]
        [HttpPost]
        public JsonResult Update(string Mahs,int Id,string Madoituong,string Namchuathue, string Namchuathue1,string Namchuathue2,string Namchuathue3,string Namchuathue4,double Giachuathue,double Giachuathue1,double Giachuathue2,double Giachuathue3,double Giachuathue4)
        {
            var model = _db.GiaNuocShCt.FirstOrDefault(t => t.Id == Id);
            model.Namchuathue = Namchuathue;
            model.Namchuathue1 = Namchuathue1;
            model.Namchuathue2 = Namchuathue2;
            model.Namchuathue3 = Namchuathue3;
            model.Namchuathue4 = Namchuathue4;
            model.Giachuathue = Giachuathue;
            model.Giachuathue1 = Giachuathue1;
            model.Giachuathue2 = Giachuathue2;
            model.Giachuathue3 = Giachuathue3;
            model.Giachuathue4 = Giachuathue4;
            model.Updated_at = DateTime.Now;
            _db.GiaNuocShCt.Update(model);
            _db.SaveChanges();
            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
    }
}
