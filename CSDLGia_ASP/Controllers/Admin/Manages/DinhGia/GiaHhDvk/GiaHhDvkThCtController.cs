using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.Models.Manages.KeKhaiDkg;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiDkg;
using Microsoft.Extensions.Hosting;
using CSDLGia_ASP.Models.Systems;
using System.Net.WebSockets;
using System.Collections;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhDvk
{
    public class GiaHhDvkThCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaHhDvkThCtController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaHhDvk/TongHopCt/Edit")]
        [HttpGet]
        public JsonResult Edit(int id)
        {
            var record = _db.GiaHhDvkCtTh.FirstOrDefault(x => x.Id == id);
            string result = "<div class='modal-body' id='tttsedit'>";
            result += "<input type='hidden' id='Id' name='Id' value='" + id + "'/>";
            result += "<div class='row'>";
            result += "<div class='col-xl-12'>";
            result += "<div class='form-group'>";
            result += "<label>Mã hàng hóa</label>";
            result += "<input class='form-control' id='Mahh' name='Mahh' value='" + record.Tenhhdv + "' disabled/>";
            result += "</div></div>";
            result += "<div class='col-xl-12'>";
            result += "<div class='form-group'>";
            result += "<label>Loại giá</label>";
            result += "<select class='form-control' id='Loaigia' name='Loaigia'>";
            foreach(var lg in Helpers.Loaigia())
            {
                result += "<option value='"+lg+"'" + (record.Loaigia == lg ? "selected" : "") + ">"+lg+"</option>";
            }
            result += "</select></div></div>";
            result += "<div class='col-xl-6'>";
            result += "<div class='form-group'>";
            result += "<label>Giá kỳ trước</label>";
            result += "<input data-mask='fdecimal' class='form-control' id='Gia' name='Gia' value='" + record.Gia+"'/>";
            result += "</div></div>";
            result += "<div class='col-xl-6'>";
            result += "<div class='form-group'>";
            result += "<label>Giá kỳ này</label>";
            result += "<input data-mask='fdecimal' class='form-control' id='Gialk' name='Gialk' value='" + record.Gialk + "'/>";
            result += "</div></div>";
            result += "<div class='col-xl-12'>";
            result += "<div class='form-group'>";
            result += "<label>Nguồn thông tin</label>";
            result += "<select class='form-control' id='Nguontt' name='Nguontt'>";
            foreach (var ntt in Helpers.Nguonthongtin())
            {
                result += "<option value='" + ntt + "'" + (record.Nguontt == ntt ? "selected" : "") + ">" + ntt + "</option>";
            }
            result += "</select></div></div></div></div>";

            var data = new { status = "success", message = result };
            return Json(data);
        }
        [Route("GiaHhDvk/TongHopCt/Update")]
        [HttpGet]
        public JsonResult Update(int Id,double Gia,double Gialk,string Loaigia,string Nguontt)
        {
            var record = _db.GiaHhDvkCtTh.FirstOrDefault(x => x.Id == Id);
            record.Gia = Gia;
            record.Gialk = Gialk;
            record.Loaigia = Loaigia;
            record.Nguontt = Nguontt;
            _db.GiaHhDvkCtTh.Update(record);
            _db.SaveChanges();
            var modelCt = _db.GiaHhDvkCtTh.Where(x => x.Mahs == record.Mahs);
            string result = "<table class='table table-striped table-bordered table-hover' id='tableCt'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th>STT</th>";
            result += "<th>Tên hàng hóa dịch vụ</th>";
            result += "<th>Đơn vị tính</th>";
            result += "<th>Đơn giá</th>";
            result += "<th>Thao tác</th>";
            result += "</tr></thead>";
            result += "<tbody>";
            int record_id = 0;
            foreach (var ct in modelCt)
            {
                result += "<tr>";
                result += "<td>"+(++record_id)+"</td>";
                result += "<td>"+ct.Tenhhdv+"</td>";
                result += "<td>"+ct.Gia+"</td>";
                result += "<td>"+ct.Gialk+"</td>";
                result += "<td><button type='button' data-target='#modal-edit' data-toggle='modal' class='btn btn-default btn-xs mbs' onclick='editItem(`" + ct.Id + "`)'>";
                result+= "<i class='fa fa-edit'></i>&nbsp;Kê khai</button></td></tr>";
            }
            result += "</table>";
            var data = new { status = "success", message = result };
            return Json(data);
        }
    }
}
