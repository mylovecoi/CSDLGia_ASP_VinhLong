using CSDLGia_ASP.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ThongTinGiayTo
{
    public class ThongTinGiayToController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _env;

        public ThongTinGiayToController(CSDLGiaDBContext db, IWebHostEnvironment hostingEnv)
        {
            _db = db;
            _env = hostingEnv;
        }

        [HttpPost("ThongTinGiayTo/Store")]
        public async Task<JsonResult> Store(IFormFile FileUpLoad, string MoTa, string Mahs, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                string filename = "";
                if (FileUpLoad != null)
                {
                    string wwwRootPath = _env.WebRootPath;
                    string extension = Path.GetExtension(FileUpLoad.FileName);
                    if (Helper.Helpers.CheckFileType(extension))
                    {
                        string name = FileUpLoad.FileName;
                        name = name.Replace(extension, "");
                        name = Regex.Replace(name.Normalize(NormalizationForm.FormD), @"[^\p{L}\p{N}]", "");
                        filename = Mahs + "_" + DateTime.Now.ToString("yymmssfff") + "_" + name + extension;
                        string path = Path.Combine(wwwRootPath + "/UpLoad/File/ThongTinGiayTo", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await FileUpLoad.CopyToAsync(FileStream);
                            FileStream.Close();
                        }
                        var model = new Models.Manages.DinhGia.ThongTinGiayTo
                        {
                            MoTa = MoTa,
                            FileName = filename,
                            Mahs = Mahs,
                            Status = "CXD",
                            Madv = Madv
                        };
                        _db.ThongTinGiayTo.Add(model);
                        _db.SaveChanges();
                        string result = this.GetDataThongTinGiayTo(Mahs);
                        var data = new { status = "success", message = result };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "File tải lên không đúng định dạng (.jpg, jpeg, png, doc, docx, xls, xlsx, pdf)!" };
                        return Json(data);
                    }
                }
                else
                {
                    var model = new Models.Manages.DinhGia.ThongTinGiayTo
                    {
                        MoTa = MoTa,
                        FileName = filename,
                        Mahs = Mahs,
                        Status = "CXD",
                        Madv = Madv
                    };
                    _db.ThongTinGiayTo.Add(model);
                    _db.SaveChanges();
                    string result = this.GetDataThongTinGiayTo(Mahs);
                    var data = new { status = "success", message = result };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("ThongTinGiayTo/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.ThongTinGiayTo.FirstOrDefault(t => t.Id == Id);

                string result = "<div class='modal-body' id='frm_giayto_edit'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Mô tả</label>";
                result += "<input type='text' class='form-control' id='mota_giayto_edit' name='mota_giayto_edit'value='" + model.MoTa + "'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form -group fv-plugins-icon-container'>";
                result += "<label>File đính kèm</label>";
                if (!string.IsNullOrEmpty(model.FileName))
                {
                    result += "<br>";
                    result += "<a href='/UpLoad/File/ThongTinGiayTo/" + model.FileName + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/ThongTinGiayTo/" + model.FileName + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += "<i class='icon-lg la la-eye text-success''></i>File đính kèm hiện tại</a>";
                    result += "<br>";
                }
                result += "<input type='file' class='form-control' id='file_giayto_edit' name='file_giayto_edit'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>";
                result += "<input hidden class='form-control' id='id_giayto_edit' name='id_giayto_edit' value='" + model.Id + "'/>";
                result += "</div>";

                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [HttpPost("ThongTinGiayTo/Update")]
        [DisableRequestSizeLimit]
        public async Task<JsonResult> Update(IFormFile FileUpLoad, string MoTa, string Mahs, int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.ThongTinGiayTo.FirstOrDefault(t => t.Id == Id);

                string filename = model.FileName;
                string wwwRootPath = _env.WebRootPath;
                if (FileUpLoad != null)
                {
                    string extension = Path.GetExtension(FileUpLoad.FileName);
                    if (Helper.Helpers.CheckFileType(extension))
                    {
                        //File != null => delete file old
                        if (!string.IsNullOrEmpty(filename))
                        {
                            string path_del = Path.Combine(wwwRootPath + "/UpLoad/File/ThongTinGiayTo/", filename);
                            FileInfo fi = new FileInfo(path_del);
                            if (fi != null)
                            {
                                System.IO.File.Delete(path_del);
                                fi.Delete();
                            }
                        }
                        // add new file
                        string name = FileUpLoad.FileName;
                        name = name.Replace(extension, "");
                        name = Regex.Replace(name.Normalize(NormalizationForm.FormD), @"[^\p{L}\p{N}]", "");
                        filename = Mahs + "_" + DateTime.Now.ToString("yymmssfff") + "_" + name + extension;

                        string path_up = Path.Combine(wwwRootPath + "/UpLoad/File/ThongTinGiayTo/", filename);
                        using (var FileStream = new FileStream(path_up, FileMode.Create))
                        {
                            await FileUpLoad.CopyToAsync(FileStream);
                            FileStream.Close();
                        }
                        model.MoTa = MoTa;
                        model.FileName = filename;
                        _db.ThongTinGiayTo.Update(model);
                        _db.SaveChanges();
                        string result = this.GetDataThongTinGiayTo(Mahs);
                        var data = new { status = "success", message = result };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "File tải lên không đúng định dạng (.jpg, jpeg, png, doc, docx, xls, xlsx, pdf)!" };
                        return Json(data);
                    }
                }
                else
                {
                    model.MoTa = MoTa;
                    model.FileName = filename;
                    _db.ThongTinGiayTo.Update(model);
                    _db.SaveChanges();
                    string result = this.GetDataThongTinGiayTo(Mahs);
                    var data = new { status = "success", message = result };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("ThongTinGiayTo/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.ThongTinGiayTo.FirstOrDefault(t => t.Id == Id);
                string wwwRootPath = _env.WebRootPath;
                if (!string.IsNullOrEmpty(model.FileName))
                {
                    string path = Path.Combine(wwwRootPath + "/UpLoad/File/ThongTinGiayTo/", model.FileName);
                    FileInfo fi = new FileInfo(path);
                    if (fi != null)
                    {
                        System.IO.File.Delete(path);
                        fi.Delete();
                    }
                }
                _db.ThongTinGiayTo.Remove(model);
                _db.SaveChanges();
                string result = this.GetDataThongTinGiayTo(Mahs);
                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        private string GetDataThongTinGiayTo(string Mahs)
        {
            var model = _db.ThongTinGiayTo.Where(t => t.Mahs == Mahs);

            int record_id = 1;
            string result = "<div class='card-body' id='giayto_data'>";
            result += "<table class='table table-striped table-bordered table-hover'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='5%'>#</th>";
            result += "<th> Mô tả</th>";
            result += "<th width='15%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";
            if (model.Any())
            {
                foreach (var item in model)
                {
                    string path = _env.WebRootPath + "/UpLoad/File/ThongTinGiayTo/" + item.FileName;
                    result += "<tr>";
                    result += "<td style='text-align:center'>" + (record_id++) + "</td>";
                    result += "<td style='font-weight:bold'>" + item.MoTa + "</td>";
                    result += "<td>";
                    if (!string.IsNullOrEmpty(item.FileName) && System.IO.File.Exists(path))
                    {
                        result += "<a href='/UpLoad/File/ThongTinGiayTo/" + item.FileName + "' target='_blank' class='btn btn-sm btn-clean btn-icon' title='Xem chi tiết'";
                        result += " onclick='window.open(`/UpLoad/File/ThongTinGiayTo/" + item.FileName + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                        result += "<i class='icon-lg la la-eye text-success''></i></a>";
                    }
                    result += "<button type='button' data-target='#Edit_GiayTo_Modal' data-toggle='modal' class='btn btn-sm btn-clean btn-icon'";
                    result += " onclick='SetEditGiayTo(`" + item.Id + "`)' title='Chỉnh sửa'>";
                    result += " <i class='icon-lg la la-edit text-primary'></i>";
                    result += " </button>";
                    result += "<button type='button' data-toggle='modal' data-target='#Delete_GiayTo_Modal' class='btn btn-sm btn-clean btn-icon' title='Xóa'";
                    result += " onclick='SetDelGiayTo(`" + item.Id + "`,`" + @item.MoTa + "`)'>";
                    result += "<i class='icon-lg la la-trash text-danger'></i>";
                    result += "</button>";
                    result += "</td>";
                    result += "</tr>";
                }
            }
            else
            {
                result += "<tr>";
                result += "<td colspan='3'>Không tìm thấy thông tin</td>";
                result += "</tr>";
            }
            result += "</tbody>";
            result += "</table>";
            result += "</div>";

            return result;
        }

        [Route("ThongTinGiayTo/GetList")]
        [HttpPost]
        public JsonResult GetList(string Mahs)
        {
            var model = _db.ThongTinGiayTo.Where(t => t.Mahs == Mahs);
            string result = "<div class='modal-body' id='giayto_data'>";
            int record_id = 1;
            if (model.Any())
            {
                foreach (var item in model)
                {
                    string path = _env.WebRootPath + "/UpLoad/File/ThongTinGiayTo/" + item.FileName;
                    result += "<p>";
                    result += (record_id++) + "." + item.MoTa + " ";
                    if (!string.IsNullOrEmpty(item.FileName) && System.IO.File.Exists(path))
                    {
                        result += "<a href='/UpLoad/File/ThongTinGiayTo/" + item.FileName + "' target='_blank' class='btn btn-link'";
                        result += " onclick='window.open(`/UpLoad/File/ThongTinGiayTo/" + item.FileName + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                        result += "<i class='icon-lg la la-eye text-success''></i>File đính kèm hiện tại</a>";
                    }                        
                    result += "</p>";
                }
            }
            else
            {
                result += "<p>Không tìm thấy thông tin giấy tờ theo hồ sơ</p>";
            }
            result += "</div>";

            var data = new { status = "success", message = result };
            return Json(data);
        }
    }
}
