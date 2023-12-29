using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.ChiSoGiaTd;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using CSDLGia_ASP.ViewModels.Manages.ChiSoGiaTd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ChiSoGiaTd
{
    public class ChiSoGiaTdController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ChiSoGiaTdController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("ChiSoGiaTieuDung")]
        [HttpGet]
        public IActionResult Index(string Matt, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.hoso", "Index") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {

                    //List<CSDLGia_ASP.Models.Manages.ChiSoGiaTd.ChiSoGiaTd> model = new List<CSDLGia_ASP.Models.Manages.ChiSoGiaTd.ChiSoGiaTd>();
                    var dsdonvi = (from db in _db.DsDiaBan.Where(t => t.Level != "H")
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       MaDiaBan = dv.MaDiaBan,
                                       TenDv = dv.TenDv,
                                       MaDv = dv.MaDv,
                                   }).ToList();
                    if (dsdonvi.Count > 0)
                    {
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                        {
                            Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(Madv))
                            {
                                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.MaDv).First();
                            }
                        }

                        List<CSDLGia_ASP.Models.Manages.ChiSoGiaTd.ChiSoGiaTd> model = new List<CSDLGia_ASP.Models.Manages.ChiSoGiaTd.ChiSoGiaTd>();
                        if (string.IsNullOrEmpty(Matt))
                        {
                            model = _db.ChiSoGiaTd.Where(x => x.Madv == Madv).ToList();
                            ViewData["matt"] = _db.ChiSoGiaTdDm.FirstOrDefault(x => x.Matt != "1").Matt;
                            ViewData["nam"] = _db.ChiSoGiaTdDm.FirstOrDefault(x => x.Matt != "1").Nam;
                        }
                        else
                        {
                            var getNam = _db.ChiSoGiaTdDm.FirstOrDefault(x => x.Matt == Matt).Nam;
                            model = _db.ChiSoGiaTd.Where(x => x.Nam == getNam && x.Madv == Madv).ToList();
                            ViewData["matt"] = Matt;
                            ViewData["nam"] = getNam;
                        }

                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["hanghoa"] = _db.ChiSoGiaTdHh;
                        ViewData["allModel"] = _db.ChiSoGiaTdDm;
                        ViewData["Title"] = " Thông tin chi tiết hồ sơ";
                        ViewData["listTt"] = _db.ChiSoGiaTdDm.Where(x => x.Matt != "1");
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["MenuLv1"] = "menu_csg";
                        ViewData["MenuLv2"] = "menu_csgHs";
                        ViewData["MenuLv3"] = "menu_csgHs_ds";
                        return View("Views/Admin/Manages/ChiSoGiaTd/Hoso/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = " Thông tin chi tiết hồ sơ";
                        ViewData["Messages"] = "Thông tin chi tiết hồ sơ.";
                        ViewData["MenuLv1"] = "menu_csg";
                        ViewData["MenuLv2"] = "menu_csgHs";
                        ViewData["MenuLv3"] = "menu_csgHs_ds";
                        return View("Views/Admin/Error/ThongBaoLoi.cshtml");
                    }


                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("ChiSoGiaTieuDung/Store")]
        [HttpPost]
        public JsonResult Store(string Thongtinbc, string Ghichu, DateTime Ngaybc, string Tinhtrang, string Matt, string Thang, string Diaphuong, string Donvi)
        {
            var getInfo = _db.ChiSoGiaTdDm.FirstOrDefault(x => x.Matt == Matt);
            var model = new CSDLGia_ASP.Models.Manages.ChiSoGiaTd.ChiSoGiaTd
            {
                Mahs = DateTime.Now.ToString("yyMMddssmmHH"),
                Madv = Donvi,
                Thongtinbc = Thongtinbc,
                Ghichu = Ghichu,
                Thang = Thang,
                Nam = getInfo.Nam,
                Trangthai = "CHT",
                Congbo = "CHUACONGBO",
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
                Diaphuong = Diaphuong,
            };
            _db.ChiSoGiaTd.Add(model);

            //add danhmuc by thongtu
            var danhmucnhom = _db.ChiSoGiaTdDm.Where(x => x.Matt == Matt);

            foreach (var dm in danhmucnhom)
            {
                var modelDm = new ChiSoGiaTdDmCt();
                modelDm.Mahs = model.Mahs;
                modelDm.Masonhomhanghoa = dm.Masonhomhanghoa;
                modelDm.Masohanghoa = dm.Masohanghoa;
                modelDm.Tenhanghoa = dm.Tenhanghoa;
                modelDm.Masogoc = dm.Masogoc;
                modelDm.Thang = model.Thang;
                modelDm.Nam = dm.Nam;
                if (Diaphuong == "nt")
                {
                    modelDm.QuyensoNt = dm.QuyensoNt;
                }
                else
                {
                    modelDm.QuyensoTt = dm.QuyensoTt;
                }
                _db.ChiSoGiaTdDmCt.Add(modelDm);
            }

            //add hanghoa by thongtu(if have)
            var hanghoa = _db.ChiSoGiaTdHh.Where(x => x.Matt == Matt);
            if (hanghoa != null)
            {
                foreach (var hh in hanghoa)
                {
                    var modelHh = new ChiSoGiaTdHhCt();
                    modelHh.Mahs = model.Mahs;
                    modelHh.Masonhomhanghoa = hh.Masonhomhanghoa;
                    modelHh.Masohanghoa = hh.Masohanghoa;
                    modelHh.Tenhanghoa = hh.Tenhanghoa;
                    modelHh.Masogoc = hh.Masogoc;
                    modelHh.Thang = model.Thang;
                    modelHh.Dvt = hh.Dvt;
                    modelHh.Nam = hh.Nam;
                    modelHh.Giakychon = hh.Gia;
                    _db.ChiSoGiaTdHhCt.Add(modelHh);
                }
            }
            _db.SaveChanges();
            string result = GetData();
            var data = new { status = "success", message = result };
            return Json(data);
        }
        public string GetData()
        {
            var model = _db.ChiSoGiaTd.ToList();

            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover' id='sample_3'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th>STT</th>";
            result += "<th>Tên hồ sơ</th>";
            result += "<th>Nội dung</th>";
            result += "<th>Kỳ chọn</th>";
            result += "<th>Chỉ số giá</th>";
            result += "<th>Thao tác</th>";
            result += "</tr></thead><tbody>";
            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + (record++) + "</td>";
                result += "<td class='active'>" + item.Thongtinbc + "</td>";
                result += "<td class='active'>" + item.Ghichu + "</td>";
                result += "<td class='active'> Tháng " + item.Thang + " năm " + item.Nam + "</td>";
                result += "<td>" + item.Giakychon + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-edit btn-icon' title='Sửa'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Xóa'";
                result += " data-target='#Delete_Modal' data-toggle='modal' onclick='SetDelete(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-trash text-danger'></i>";
                result += "</button>";
                result += "<a class='btn btn-sm btn-clean btn-icon' title='Xem nhóm hàng hoá' href='/DanhSachNhomChiSoGiaTieuDungCt?Mahs=" + item.Mahs + "'>";
                result += "<i class='icon-lg la la la-flag-o text-info'></i>";
                result += "</a>";
                result += "<a class='btn btn-sm btn-clean btn-icon' title='Xem chi tiết hàng hoá' href='/DanhmuchanghoaChitiet?Mahs=" + item.Mahs + "'>";
                result += "<i class='icon-lg la la-eye text-success'></i>";
                result += "</a>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Tính chí số giá CPI'";
                result += " data-target='#Calculate_Modal' data-toggle='modal' onclick='SetDelete(`" + item.Mahs + "`,`" + item.Thongtinbc + "`)'>";
                result += "<i class='icon-lg la la-calculator text-warning'></i>";
                result += "</button>";
                result += "</td></tr>";
            }
            result += "<tbody>";
            return result;
        }
        [Route("ChiSoGiaTieuDung/Edit")]
        [HttpPost]
        public JsonResult Edit(int id)
        {
            var model = _db.ChiSoGiaTd.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                var result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<input type='hidden' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
                result += "<div class='row'>";
                result += "<div class='col-xl-9'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên hồ sơ</b></label>";
                result += "<input type='text' id='thongtinbc_edit' name='thongtinbc_edit' value='" + model.Thongtinbc + "' class='form-control' />";
                result += "</div></div>";
                result += "<div class='col-xl-9'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Nội dung</b></label>";
                result += "<input type='text' id='ghichu_edit' name='ghichu_edit' value='" + model.Ghichu + "' class='form-control' />";
                result += "</div></div>";
                result += "<div class='col-xl-9'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Theo dõi</b></label>";
                result += "<select class='form-control' id='trangthai_edit' name='trangthai_edit'>";
                if (model.Trangthai == "Đang theo dõi")
                {
                    result += "<option value='Đang theo dõi' selected>Đang theo dõi</option>";
                    result += "<option value='Không theo dõi'>Không theo dõi</option>";
                }
                else
                {
                    result += "<option value='Đang theo dõi'>Đang theo dõi</option>";
                    result += "<option value='Không theo dõi' selected>Không theo dõi</option>";
                }

                result += "</select></div></div></div>";
                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa" };
                return Json(data);
            }
        }
        [Route("ChiSoGiaTieuDung/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Thongtinbc, string Ghichu, DateTime Ngaybc, string Trangthai)
        {
            var model = _db.ChiSoGiaTd.FirstOrDefault(x => x.Id == Id);
            model.Thongtinbc = Thongtinbc;
            model.Ghichu = Ghichu;
            model.Trangthai = Trangthai;
            _db.ChiSoGiaTd.Update(model);
            _db.SaveChanges();
            string result = GetData();
            var data = new { status = "success", message = result };
            return Json(data);
        }


        [Route("ChiSoGiaTieuDung/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.ChiSoGiaTd.FirstOrDefault(t => t.Id == Id);
            _db.ChiSoGiaTd.Remove(model);
            _db.SaveChanges();
            var result = GetData();
            var data = new { status = "success", message = result };
            return Json(data);
        }
        [Route("ChiSoGiaTieuDung/Calculate")]
        [HttpGet]
        public IActionResult Calculate(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.hoso", "Index"))
                {
                    var dp = _db.ChiSoGiaTd.FirstOrDefault(x => x.Mahs == Mahs).Diaphuong;
                    //average all hanghoa
                    //take all hanghoa in mahs

                    var hanghoa = _db.ChiSoGiaTdHhCt.Where(x => x.Mahs == Mahs);
                    //lay tat ca cac hang hoa theo nhom 4
                    var getNhom = from t in hanghoa
                                  group t by t.Masogoc into grp
                                  select new
                                  {
                                      maso = grp.Key,
                                      cnt = grp.Count()

                                  };
                    //loc qua tung nhom xong loc qua tung hang hoa trong nhom day
                    foreach (var item in getNhom)
                    {
                        double giaKychonHh = 0;
                        double giaKychonRs = 0;
                        double countHh = 0;
                        //var getGiagocHh = hanghoa.Where(x => x.MasohanghoaInDm == item.maso);
                        var getGiagocHh = hanghoa.Where(x => x.Masogoc == item.maso && x.Mahs == Mahs);
                        //loc qua tung hang hoa
                        foreach (var getItem in getGiagocHh)
                        {
                            var gocHh2019 = _db.ChiSoGiaTdHh.FirstOrDefault(x => x.Matt == "1" && x.Masohanghoa == getItem.Masohanghoa).Gia;
                            giaKychonHh = (getItem.Giakychon / gocHh2019) * 100;
                            giaKychonHh *= giaKychonHh;
                            countHh++;
                        }
                        giaKychonRs = Math.Pow(giaKychonHh, 1 / countHh);

                        //average all nhom 4
                        //var getMasogocnhom4 = getGiagocHh.FirstOrDefault().Masogoc.Substring(0, 7);
                        //var nhom4 = _db.ChiSoGiaTdDmCt.FirstOrDefault(x => x.Masonhomhanghoa ==  "4" && x.Masogoc.Substring(0, 7) == getMasogocnhom4 && x.Mahs == Mahs);

                        var getMasogocnhom4 = getGiagocHh.FirstOrDefault().Masogoc;

                        var nhom4 = _db.ChiSoGiaTdDmCt.FirstOrDefault(x => x.Masonhomhanghoa == "4" && x.Masohanghoa == getMasogocnhom4 && x.Mahs == Mahs);
                        nhom4.Giakychon = giaKychonRs;
                        _db.ChiSoGiaTdDmCt.Update(nhom4);
                    }

                    //average all nhom 4
                    //lay tat ca cac nhom 4
                    //var allnhom4 = _db.ChiSoGiaTdDmCt.Where(x =>x.Masonhomhanghoa == "4");
                    var allnhom4 = _db.ChiSoGiaTdDmCt.Where(x => x.Masonhomhanghoa == "4" && x.Mahs == Mahs).ToList();
                    //lay tat ca cac nhom 4 con nhom 3
                    var getNhom4 = from t in allnhom4
                                   group t by t.Masogoc into grp
                                   select new
                                   {
                                       masogoc = grp.Key,
                                       cnt = grp.Count()
                                   };

                    //loc qua tung nhom xong loc qua tung nhom 4 trong masogoc
                    foreach (var item in getNhom4)
                    {
                        double giaKychonNhom4 = 0;
                        double giaKychonRsN4 = 0;
                        /*double count = 0;*/
                        var getGiagoc4 = allnhom4.Where(x => x.Masogoc == item.masogoc);
                        //loc qua tung nhom 4
                        foreach (var getItem in getGiagoc4)
                        {
                            if (dp == "nt")
                            {
                                giaKychonNhom4 = Math.Pow(getItem.Giakychon, getItem.QuyensoNt);

                            }
                            if (dp == "tt")
                            {
                                giaKychonNhom4 = Math.Pow(getItem.Giakychon, getItem.QuyensoTt);
                            }
                            giaKychonNhom4 *= giaKychonNhom4;
                            //count++;
                        }
                        giaKychonRsN4 = giaKychonNhom4;
                        //giaKychonRsN4 = Math.Pow(giaKychonNhom4, 1 / count);

                        //average all nhom 3
                        var getMasogocnhom3 = getGiagoc4.FirstOrDefault().Masogoc;
                        //var nhom3 = _db.ChiSoGiaTdDmCt.FirstOrDefault(x => x.Masonhomhanghoa == "3" && x.Masogoc.Substring(0,5) == getMasogocnhom3);
                        var nhom3 = _db.ChiSoGiaTdDmCt.FirstOrDefault(x => x.Masonhomhanghoa == "3" && x.Masohanghoa == getMasogocnhom3 && x.Mahs == Mahs);
                        nhom3.Giakychon = giaKychonRsN4;
                        _db.ChiSoGiaTdDmCt.Update(nhom3);
                    }


                    //average all nhom 3
                    //lay tat ca cac nhom 3
                    //var allnhom3 = _db.ChiSoGiaTdDmCt.Where(x =>x.Masonhomhanghoa == "3");
                    var allnhom3 = _db.ChiSoGiaTdDmCt.Where(x => x.Masonhomhanghoa == "3" && x.Mahs == Mahs).ToList();
                    //lay tat ca cac nhom 3 con nhom 2
                    var getNhom3 = from t in allnhom3
                                   group t by t.Masogoc into grp
                                   select new
                                   {
                                       masogoc = grp.Key,
                                       cnt = grp.Count()
                                   };

                    //loc qua tung nhom xong loc qua tung nhom 3 trong masogoc
                    foreach (var item in getNhom3)
                    {
                        double giaKychonNhom3 = 0;
                        double giaKychonRsN3 = 0;
                        double count = 0;
                        var getGiagoc3 = allnhom3.Where(x => x.Masogoc == item.masogoc);
                        //loc qua tung nhom 3

                        foreach (var getItem in getGiagoc3)
                        {
                            if (dp == "nt")
                            {
                                giaKychonNhom3 = Math.Pow(getItem.Giakychon, getItem.QuyensoNt);
                            }
                            if (dp == "tt")
                            {
                                giaKychonNhom3 = Math.Pow(getItem.Giakychon, getItem.QuyensoTt);
                            }
                            giaKychonNhom3 *= giaKychonNhom3;
                            count++;

                        }
                        giaKychonRsN3 = giaKychonNhom3;
                        //giaKychonRsN3 = Math.Pow(giaKychonNhom3, 1 / count);

                        //average all nhom 2
                        var getMasogocnhom2 = getGiagoc3.FirstOrDefault().Masogoc;
                        //var nhom2 = _db.ChiSoGiaTdDmCt.FirstOrDefault(x => x.Masonhomhanghoa == "2" && x.Masogoc.Substring(0, 3) == getMasogocnhom2);
                        var nhom2 = _db.ChiSoGiaTdDmCt.FirstOrDefault(x =>
                                                            x.Masonhomhanghoa == "2"
                                                        && x.Masohanghoa == getMasogocnhom2
                                                        && x.Mahs == Mahs);
                        //nhom2.Giagoc = giaGocRsN3;
                        nhom2.Giakychon = giaKychonRsN3;
                        _db.ChiSoGiaTdDmCt.Update(nhom2);
                    }

                    //average all nhom 2
                    //lay tat ca cac nhom 2
                    //var allnhom2 = _db.ChiSoGiaTdDmCt.Where(x =>x.Masonhomhanghoa == "2");
                    var allnhom2 = _db.ChiSoGiaTdDmCt.Where(x =>
                                                x.Masonhomhanghoa == "2"
                                                && x.Mahs == Mahs).ToList();
                    //lay tat ca cac nhom 2 con nhom 1
                    var getNhom2 = from t in allnhom2
                                   group t by t.Masogoc into grp
                                   select new
                                   {
                                       masogoc = grp.Key,
                                       cnt = grp.Count()
                                   };

                    //loc qua tung nhom xong loc qua tung nhom 3 trong masogoc
                    foreach (var item in getNhom2)
                    {
                        double giaKychonNhom2 = 0;
                        double giaKychonRsN2 = 0;
                        double count = 0;
                        var getGiagoc2 = allnhom2.Where(x => x.Masogoc == item.masogoc);
                        //loc qua tung nhom 2
                        foreach (var getItem in getGiagoc2)
                        {
                            if (dp == "nt")
                            {
                                giaKychonNhom2 = Math.Pow(getItem.Giakychon, getItem.QuyensoNt);
                            }
                            if (dp == "tt")
                            {
                                giaKychonNhom2 = Math.Pow(getItem.Giakychon, getItem.QuyensoTt);
                            }
                            giaKychonNhom2 *= giaKychonNhom2;
                            count++;
                        }
                        giaKychonRsN2 = giaKychonNhom2;
                        //giaKychonRsN2 = Math.Pow(giaKychonNhom2, 1 / count);

                        //average all nhom 2
                        var getMasogocnhom1 = getGiagoc2.FirstOrDefault().Masogoc;
                        //var nhom1 = _db.ChiSoGiaTdDmCt.FirstOrDefault(x => x.Masonhomhanghoa == "1" && x.Masogoc.Substring(0, 2) == getMasogocnhom1);
                        var nhom1 = _db.ChiSoGiaTdDmCt.FirstOrDefault(x =>
                                                    x.Masonhomhanghoa == "1"
                                                && x.Masohanghoa == getMasogocnhom1
                                                && x.Mahs == Mahs);
                        nhom1.Giakychon = giaKychonRsN2;
                        _db.ChiSoGiaTdDmCt.Update(nhom1);
                    }
                    //average all nhom 1
                    //lay tat ca cac nhom 1
                    //var nhom1 = _db.ChiSoGiaTdDmCt.Where(x => x.Mahs == Mahs && x.Masonhomhanghoa == "1");
                    var getNhom1 = _db.ChiSoGiaTdDmCt.Where(x =>
                                            x.Masonhomhanghoa == "1"
                                            && x.Mahs == Mahs).ToList();

                    double giaKychonNhom1 = 0;
                    double giaKychonRsN1 = 0;
                    double countCPI = 0;
                    foreach (var getItem in getNhom1)
                    {

                        //loc qua tung nhom 1
                        if (dp == "nt")
                        {
                            giaKychonNhom1 = Math.Pow(getItem.Giakychon, getItem.QuyensoNt);
                        }
                        if (dp == "tt")
                        {
                            giaKychonNhom1 = Math.Pow(getItem.Giakychon, getItem.QuyensoTt);
                        }
                        giaKychonNhom1 *= giaKychonNhom1;
                        countCPI++;

                    }
                    giaKychonRsN1 = giaKychonNhom1;
                    //giaKychonRsN1 = Math.Pow(giaKychonNhom1, 1 / countCPI)*100;

                    var getHs = _db.ChiSoGiaTd.FirstOrDefault(x => x.Mahs == Mahs);
                    getHs.Giakychon = giaKychonRsN1;
                    _db.ChiSoGiaTd.Update(getHs);
                    _db.SaveChanges();

                    var dsdonvi = (from db in _db.DsDiaBan.Where(t => t.Level != "H")
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       MaDiaBan = dv.MaDiaBan,
                                       TenDv = dv.TenDv,
                                       MaDv = dv.MaDv,
                                   }).ToList();
                    ViewData["DsDonVi"] = dsdonvi;
                    ViewData["cpiKychon"] = System.Math.Round(giaKychonRsN1, 5);
                    ViewData["listTt"] = _db.ChiSoGiaTdDm.Where(x => x.Matt != "1");
                    ViewData["thang"] = getHs.Thang;
                    ViewData["nam"] = getHs.Nam;
                    ViewData["tenhs"] = _db.ChiSoGiaTd.FirstOrDefault(x => x.Mahs == Mahs).Thongtinbc;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["MenuLv1"] = "menu_csg";
                    ViewData["MenuLv2"] = "menu_csgHs";
                    ViewData["MenuLv3"] = "menu_csgHs_ds";

                    var takeInfoModel = _db.ChiSoGiaTd.FirstOrDefault(x => x.Mahs == Mahs);
                    var model = _db.ChiSoGiaTd.Where(x => x.Madv == takeInfoModel.Madv && x.Nam == takeInfoModel.Nam);
                    return View("Views/Admin/Manages/ChiSoGiaTd/Hoso/Index.cshtml", model);
                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

        }

        [Route("ChiSoGiaTieuDung/Average")]
        [HttpPost]
        public IActionResult Average(string nam_average)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.hoso", "Index"))
                {
                    var model = _db.ChiSoGiaTd.Where(x => x.Nam == nam_average);
                    double average = 0;
                    double count = 0;
                    foreach (var item in model)
                    {
                        average += item.Giakychon;
                        ++count;
                    }
                    average = average / count;
                    ViewData["thang"] = count;
                    ViewData["nam"] = nam_average;
                    ViewData["average"] = average;
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["MenuLv1"] = "menu_csg";
                    ViewData["MenuLv2"] = "menu_csgHs";
                    ViewData["MenuLv3"] = "menu_csgHs_ds";
                    return View("Views/Admin/Manages/ChiSoGiaTd/Hoso/Average.cshtml");
                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
        [Route("ChiSoGiaTieuDung/Chuyen")]
        [HttpPost]
        public IActionResult Chuyen(string mahs_chuyen, string macqcq_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.hoso.xetduyet", "Index"))
                {
                    var model = _db.ChiSoGiaTd.FirstOrDefault(t => t.Mahs == mahs_chuyen);

                    var dvcq_join = from dvcq in _db.DsDonVi
                                    join db in _db.DsDiaBan on dvcq.MaDiaBan equals db.MaDiaBan
                                    select new VMDsDonVi
                                    {
                                        Id = dvcq.Id,
                                        MaDiaBan = dvcq.MaDiaBan,
                                        MaDv = dvcq.MaDv,
                                        TenDv = dvcq.TenDv,
                                        Level = db.Level,
                                    };
                    var chk_dvcq = dvcq_join.FirstOrDefault(t => t.MaDv == macqcq_chuyen);
                    model.Macqcq = macqcq_chuyen;
                    model.Trangthai = "HT";
                    if (chk_dvcq != null && chk_dvcq.Level == "T")
                    {
                        model.Madv_t = macqcq_chuyen;
                        model.Thoidiem_t = DateTime.Now;
                        model.Trangthai_t = "CHT";
                    }
                    else if (chk_dvcq != null && chk_dvcq.Level == "ADMIN")
                    {
                        model.Madv_ad = macqcq_chuyen;
                        model.Thoidiem_ad = DateTime.Now;
                        model.Trangthai_ad = "CHT";
                    }
                    else
                    {
                        model.Madv_h = macqcq_chuyen;
                        model.Thoidiem_h = DateTime.Now;
                        model.Trangthai_h = "CHT";
                    }
                    _db.ChiSoGiaTd.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "ChiSoGiaTd", new { model.Madv });

                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}
