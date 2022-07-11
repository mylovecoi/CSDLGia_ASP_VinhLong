using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class TtDnTdController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TtDnTdController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("DoanhNghiep/DanhSach")]
        [HttpGet]
        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                {
                    Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");

                }
                else
                {
                    if (string.IsNullOrEmpty(Madv))
                    {
                        Madv = _db.Company.OrderBy(t => t.Id).Select(t => t.Madv).First();
                    }
                }

                var com_join = (from com in _db.Company
                                join db in _db.DsDiaBan on com.Madiaban equals db.MaDiaBan
                                select new VMCompany
                                {
                                    Id = com.Id,
                                    Madv = com.Madv,
                                    Madiaban = com.Madiaban,
                                    Tendiaban = db.TenDiaBan,
                                    Tendn = com.Tendn,
                                    Diachi = com.Diachi,
                                    Tel = com.Tel,
                                    Fax = com.Fax,
                                    Email = com.Email,
                                    Chucdanh = com.Chucdanh,
                                    Nguoiky = com.Nguoiky,
                                    Diadanh = com.Diadanh,
                                }).FirstOrDefault(t => t.Madv == Madv);

                var comct_join = (from comct in _db.CompanyLvCc
                                  join nghe in _db.DmNgheKd on comct.Manghe equals nghe.Manghe
                                  select new VMCompanyLvCc
                                  {
                                      Id = comct.Id,
                                      Mahs = comct.Mahs,
                                      Madv = comct.Madv,
                                      Manghe = comct.Manghe,
                                      Tennghe = nghe.Tennghe,
                                      Manganh = comct.Manganh,
                                      Macqcq = comct.Macqcq,
                                      Trangthai = comct.Trangthai,
                                  }).Where(t => t.Madv == Madv).ToList();

                var dn = _db.TtDnTd.FirstOrDefault(t => t.Madv == Madv);
                var dnct = _db.TtDnTdCt.Where(t => t.Madv == Madv).ToList();

                if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                {
                    ViewData["DsDoanhNghiep"] = _db.Company;
                }
                else
                {
                    ViewData["DsDoanhNghiep"] = _db.Company.Where(t => t.Madv == Madv);
                }

                ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
                ViewData["CompanyLvCc"] = comct_join;
                ViewData["TtDnTd"] = dn;
                ViewData["TtDnTdCt"] = dnct;
                ViewData["Madv"] = Madv;
                ViewData["Title"] = "Thông tin doanh nghiệp";
                ViewData["MenuLv1"] = "menu_kknygia";
                ViewData["MenuLv2"] = "menu_ttdn";
                return View("Views/Admin/Systems/TtDnTd/Index.cshtml", com_join);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("DoanhNghiep/Edit")]
        [HttpGet]
        public IActionResult Edit(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.Company.FirstOrDefault(t => t.Madv == Madv);
                var model_new = new VMCompany
                {
                    Id = model.Id,
                    Madv = model.Madv,
                    Mahs = model.Mahs,
                    Macqcq = model.Macqcq,
                    Tendn = model.Tendn,
                    Tel = model.Tel,
                    Fax = model.Fax,
                    Email = model.Email,
                    Diachi = model.Diachi,
                    Chucdanh = model.Chucdanh,
                    Nguoiky = model.Nguoiky,
                    Diadanh = model.Diadanh,
                    Giayphepkd = model.Giayphepkd,
                };

                var model_ct = from com_ct in _db.CompanyLvCc.Where(t => t.Mahs == model_new.Mahs && t.Madv == model_new.Madv)
                               join nghe in _db.DmNgheKd on com_ct.Manghe equals nghe.Manghe
                               select new VMCompanyLvCc
                               {
                                   Id = com_ct.Id,
                                   Mahs = com_ct.Mahs,
                                   Manghe = com_ct.Manghe,
                                   Macqcq = com_ct.Macqcq,
                                   Madv = com_ct.Madv,
                                   Manganh = com_ct.Manganh,
                                   Tennghe = nghe.Tennghe,
                                   Trangthai = com_ct.Trangthai
                               };

                model_new.VMCompanyLvCc = model_ct.ToList();

                ViewData["Madv"] = Madv;
                ViewData["DmNganhKd"] = _db.DmNganhKd;
                ViewData["DmNgheKd"] = _db.DmNgheKd;
                ViewData["Title"] = "Thông tin doanh nghiệp chỉnh sửa";
                ViewData["MenuLv1"] = "menu_kknygia";
                ViewData["MenuLv2"] = "menu_ttdn";
                return View("Views/Admin/Systems/TtDnTd/Edit.cshtml", model_new);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("DoanhNghiep/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(VMCompany request, IFormFile Giayphepkdupload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Giayphepkdupload != null && Giayphepkdupload.Length > 0)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string filename = Path.GetFileNameWithoutExtension(Giayphepkdupload.FileName);
                    string extension = Path.GetExtension(Giayphepkdupload.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Upload/File/", filename);
                    using (var FileStream = new FileStream(path, FileMode.Create))
                    {
                        await Giayphepkdupload.CopyToAsync(FileStream);
                    }
                    request.Giayphepkd = filename;
                }

                var ttdntd = new TtDnTd
                {

                };
                _db.TtDnTd.Add(ttdntd);
                _db.SaveChanges();

                return RedirectToAction("Index", "TtDnTd");
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }



        [Route("DoanhNghiepCt/Store")]
        [HttpPost]
        public JsonResult Store(string Madv, string Manghe, string Madiaban)
        {
            var model = new CompanyLvCc
            {
                Madv = Madv,
                Manghe = Manghe,
                Macqcq = Madiaban,
                Trangthai = "CXD",
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.CompanyLvCc.Add(model);
            _db.SaveChanges();
            string result = this.GetData(Madv);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("DoanhNghiepCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.CompanyLvCc.FirstOrDefault(t => t.Id == Id);
            _db.CompanyLvCc.Remove(model);
            _db.SaveChanges();
            string result = this.GetData(model.Madv);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        //Get
        public string GetData(string Madv)
        {
            var model = _db.CompanyLvCc.Where(t => t.Madv == Madv).ToList();
            var model_join = (from cty in model
                              join dmnghe in _db.DmNgheKd on cty.Manghe equals dmnghe.Manghe
                              select new VMCompanyLvCc
                              {
                                  Id = cty.Id,
                                  Mahs = cty.Mahs,
                                  Madv = cty.Madv,
                                  Manganh = cty.Manganh,
                                  Manghe = cty.Manghe,
                                  Tennghe = dmnghe.Tennghe,
                                  Macqcq = cty.Macqcq,
                                  Trangthai = cty.Trangthai,

                              });

            int record = 1;
            string result = "<div class='card-body' id='frm_ct_data'>";
            result += "<table class='table table-striped table-bordered table-hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>#</th>";
            result += "<th>Tên nghề kinh doanh</th>";
            result += "<th width='9%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model_join)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + (record++) + "</td>";
                result += "<td style='font-weight:bold'>" + item.Tennghe + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Xóa' data-toggle='modal'";
                result += " data-target='#Delete_Modal' onclick='GetDelete(`" + item.Id + "`, `" + item.Tennghe + "`)'>";
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
    }
}
