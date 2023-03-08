using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.ChiSoGiaTd;
using CSDLGia_ASP.ViewModels.Systems;
//using CSDLGia_ASP.ViewModels.Manages.ChiSoGiaTd;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CSDLGia_ASP.ViewModels.Manages.ChiSoGiaTd;
using OfficeOpenXml;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ChiSoGiaTd
{
    public class ChiSoGiaTdDdController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ChiSoGiaTdDdController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        [Route("DuDoanChiSoGiaTieuDung")]
        [HttpGet]
        public IActionResult Index( string Madv,string Type)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.dudoan", "Index") ||
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

                        List<CSDLGia_ASP.Models.Manages.ChiSoGiaTd.ChiSoGiaTdDd> model = new List<CSDLGia_ASP.Models.Manages.ChiSoGiaTd.ChiSoGiaTdDd>();

                        //var getNam = _db.ChiSoGiaTdDm.OrderBy(x => x.Id).LastOrDefault().Nam;
                        var lastRecord = _db.ChiSoGiaTd.OrderBy(x => x.Id).LastOrDefault();
                        model = _db.ChiSoGiaTdDd.Where(x => x.Nam == lastRecord.Nam && x.Madv == Madv).ToList();
                        ViewData["Type"] = Type;
                        ViewData["Nam"] = lastRecord.Nam;
                        ViewData["Thang"] = lastRecord.Thang;

                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                        }

                        ViewData["listTt"] = _db.ChiSoGiaTdDm.Where(x => x.Matt != "1");
                        ViewData["Title"] = " Thông tin chi tiết hồ sơ";

                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["MenuLv1"] = "menu_csg";
                        ViewData["MenuLv2"] = "menu_csgHs";
                        ViewData["MenuLv3"] = "menu_csgHs_ddlastmonth";
                        return View("Views/Admin/Manages/ChiSoGiaTd/Dudoan/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = " Thông tin chi tiết hồ sơ";
                        ViewData["Messages"] = "Thông tin chi tiết hồ sơ.";
                        ViewData["MenuLv1"] = "menu_csg";
                        ViewData["MenuLv2"] = "menu_csgHs";
                        ViewData["MenuLv3"] = "menu_csgHs_ddlastmonth";
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
        [Route("DuDoanChiSoGiaTieuDung/Create")]
        [HttpGet]
        public IActionResult Create(string Madv,string Type)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.dudoan", "Create"))
                {
                    //lấy time của sample lấy ra dự đoán=>dựa theo time mẫu lấy time sau để dự đoán
                    var lastRecord = _db.ChiSoGiaTd.OrderBy(x=>x.Id).LastOrDefault();
                    
                    var thangDd = (int.Parse(lastRecord.Thang) + 1).ToString();
                    var lastyear = (int.Parse(lastRecord.Nam) - 1).ToString();
                    
                    ViewData["allModel"] = this.CheckType(Type);
                    
                    var model = new ChiSoGiaTdDd
                    {
                        Mahs = DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = lastRecord.Madv,
                        Thang = thangDd,
                        Nam = lastRecord.Nam,
                        Congbo = lastRecord.Congbo,
                        Diaphuong = lastRecord.Diaphuong,
                        Macqcq = lastRecord.Macqcq,
                        Trangthai = lastRecord.Trangthai,
                    };
                    _db.ChiSoGiaTdDd.Add(model);
                    _db.SaveChanges();
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Madv"] = model.Madv;
                    ViewData["Type"] = Type;
                    ViewData["ThangDd"] = thangDd;
                    ViewData["Thang"] = lastRecord.Thang;
                    ViewData["nam"] = lastRecord.Nam;
                    ViewData["Donvitinh"] = _db.DmDvt.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    
                    ViewData["Title"] = "Thêm mới hồ sơ dự đoán chỉ số giá";
                    ViewData["MenuLv1"] = "menu_csg";
                    ViewData["MenuLv2"] = "menu_csgHs";
                    ViewData["MenuLv3"] = "menu_csgHs_ddlastmonth";

                    return View("Views/Admin/Manages/ChiSoGiaTd/Dudoan/Create.cshtml");
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
        [Route("DuDoanChiSoGiaTieuDung/SetUpdate")]
        [HttpPost]
        public JsonResult SetUpdate(string[] List1, string[] List2, string[] List3,
            string Mahs, string Thang,string ThangDd, string Nam, string Type)
        {
            //var thangDd = (int.Parse(Thang) - 1).ToString();
            var getDm = _db.ChiSoGiaTdDmCt.AsQueryable();
            var namDd = (int.Parse(Nam) - 1).ToString();
            
            getDm = this.CheckType(Type);
            
            var getDmDd = new List<ChiSoGiaTdDmCtDd>();
            string[] List;
            //dựa vào list nhóm nào sẽ xoá luôn các nhóm sau nó khi lấy sample dự đoán
            if (List3.Count() > 0)
            {
                List = List3;
                getDm = getDm.Where(x => x.Masonhomhanghoa != "4");
            }
            else if (List2.Count() > 0)
            {
                List = List2;
                getDm = getDm.Where(x => x.Masonhomhanghoa == "1" || x.Masonhomhanghoa == "2");
            }
            else
            {
                List = List1;
                getDm = getDm.Where(x => x.Masonhomhanghoa == "1");
            }


            foreach (var lastDm in getDm)
            {
                getDmDd.Add(new ChiSoGiaTdDmCtDd
                {
                    Mahs = Mahs,
                    Masogoc = lastDm.Masogoc,
                    Masonhomhanghoa = lastDm.Masonhomhanghoa,
                    Tenhanghoa = lastDm.Tenhanghoa,
                    Masohanghoa = lastDm.Masohanghoa,
                    Dvt = lastDm.Dvt,
                    Baocao = lastDm.Baocao,
                    QuyensoTt = lastDm.QuyensoTt,
                    QuyensoNt = lastDm.QuyensoNt,
                    Thang = ThangDd,
                    Giakychon = lastDm.Giakychon,
                    Nam = Nam,
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now,
                });
            }
            _db.ChiSoGiaTdDmCtDd.AddRange(getDmDd);
            _db.SaveChanges();
            string result = GetData(List, Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("DuDoanChiSoGiaTieuDung/UpdateElement")]
        [HttpPost]
        public JsonResult UpdateElement(int Id, string Xuhuong, string Tile, string[] List,string Type)
        {
            var info = _db.ChiSoGiaTdDmCtDd.FirstOrDefault(x => x.Id == Id);
            var record = _db.ChiSoGiaTdDd.FirstOrDefault(x => x.Mahs == info.Mahs);
            //var record = _db.ChiSoGiaTdDmCtDd.FirstOrDefault(x => x.Mahs == Mahs);
            if(Type== "Lastmonth") { 
                record.Noidung = "Dự đoán CPI so với tháng " + (int.Parse(record.Thang)-1) + " năm " + record.Nam; 
            }
            if (Type == "Lastyear")
            {
                record.Noidung = "Dự đoán CPI so với tháng " + (int.Parse(record.Thang) - 1) + " năm " + (int.Parse(record.Nam) - 1);
            }
            if (Type == "Kygoc")
            {
                record.Noidung = "Dự đoán CPI so kỳ gốc ";
            }
            if (Type == "Last12")
            {
                record.Noidung = "Dự đoán CPI so với tháng 12 năm " + record.Nam;
            }
            if (Type == "Average")
            {
                record.Noidung = "Dự đoán CPI so bình quân năm " + record.Nam;
            }

            if (Xuhuong == "tang")
            {
                record.Ghichu = record.Ghichu + "Tăng nhóm " + info.Tenhanghoa + " " + Tile + "%|";
                info.Giakychon = info.Giakychon * (100 + float.Parse(Tile)) / 100; ;
            }
            if (Xuhuong == "giam")
            {
                record.Ghichu = record.Ghichu + "Giảm nhóm " + info.Tenhanghoa + " " + Tile + "%|";
                info.Giakychon = info.Giakychon * (100 - float.Parse(Tile)) / 100; ;
            }
            _db.ChiSoGiaTdDd.Update(record);

            var getDmDd = _db.ChiSoGiaTdDmCtDd.Where(x => x.Mahs == info.Mahs);
            _db.SaveChanges();

            /*string[] newList = { };
            string maso = info.Masohanghoa;
            for(int i = 0; i < List.Length; i++)
            {
                if (List[i] == maso)
                {
                    newList[i] = List[i];
                }
            }*/
            string result = GetData(List, info.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        public string GetData(string[] List, string Mahs)
        {
            string result = "<div class='card-body' id='frm_update'>";
            result += "<table class='table table-striped table-bordered table-hover' id='dudoanTb'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th>Tên nhóm</th>";
            result += "<th>Giá</th>";
            result += "<th>Cập nhật</th>";
            result += "</tr></thead><tbody>";
            result += "</div>";
            //List.ToList().Remove(Maso);

            for (int i = 0; i < List.Length; i++)
            {
                result += "<tr>";
                var value = _db.ChiSoGiaTdDmCtDd.FirstOrDefault(x => x.Masohanghoa == List[i] && x.Mahs == Mahs);
                result += "<td>" + value.Tenhanghoa + "</td>";
                result += "<td>" + value.Giakychon + "</td>";
                result += "<input value='" + List[i] + "' id='nhom' name='nhom[]' type='hidden'/>";
                int id = value.Id;
                result += "<td><button type='button' class='btn btn-primary font-weight-bold' data-toggle='modal' data-target='#Predict_Modal' onclick='GetUpdate(`" + id + "`)'>";
                result += "Cập nhật</button></td></tr>";
            }
            result += "</tbody></table></div>";
            result += "</div>";
            return result;
        }
        [Route("DuDoanChiSoGiaTieuDung/Calculate")]
        [HttpGet]
        public IActionResult Calculate(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.dudoan", "Index"))
                {
                    //take masonhomhanghoa
                    //var takeMasonhom = _db.ChiSoGiaTdDmCtDd.FirstOrDefault(x => x.Mahs == Mahs).Masonhomhanghoa;
                    var dp = _db.ChiSoGiaTdDd.FirstOrDefault(x => x.Mahs == Mahs).Diaphuong;
                    //average all nhom 3
                    //lay tat ca cac nhom 3
                    //var allnhom3 = _db.ChiSoGiaTdDmCt.Where(x =>x.Masonhomhanghoa == "3");
                    var allnhom3 = _db.ChiSoGiaTdDmCtDd.Where(x => x.Mahs == Mahs && x.Masonhomhanghoa == "3").ToList();
                    //lay tat ca cac nhom 3 con nhom 2
                    if (allnhom3.Count() > 0)
                    {
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
                            var nhom2 = _db.ChiSoGiaTdDmCtDd.FirstOrDefault(x =>
                                                                x.Masonhomhanghoa == "2"
                                                            && x.Masohanghoa == getMasogocnhom2
                                                            && x.Mahs == Mahs);
                            //nhom2.Giagoc = giaGocRsN3;
                            nhom2.Giakychon = giaKychonRsN3;
                            _db.ChiSoGiaTdDmCtDd.Update(nhom2);
                        }
                    }

                    //average all nhom 2
                    //lay tat ca cac nhom 2
                    //var allnhom2 = _db.ChiSoGiaTdDmCt.Where(x =>x.Masonhomhanghoa == "2");
                    var allnhom2 = _db.ChiSoGiaTdDmCtDd.Where(x =>
                                                x.Masonhomhanghoa == "2"
                                                && x.Mahs == Mahs).ToList();
                    //lay tat ca cac nhom 2 con nhom 1
                    if (allnhom2.Count() > 0)
                    {
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
                            var nhom1 = _db.ChiSoGiaTdDmCtDd.FirstOrDefault(x =>
                                                        x.Masonhomhanghoa == "1"
                                                    && x.Masohanghoa == getMasogocnhom1
                                                    && x.Mahs == Mahs);
                            nhom1.Giakychon = giaKychonRsN2;
                            _db.ChiSoGiaTdDmCtDd.Update(nhom1);
                        }
                    }

                    //average all nhom 1
                    //lay tat ca cac nhom 1
                    //var nhom1 = _db.ChiSoGiaTdDmCt.Where(x => x.Mahs == Mahs && x.Masonhomhanghoa == "1");
                    var getNhom1 = _db.ChiSoGiaTdDmCtDd.Where(x =>
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

                    var getHs = _db.ChiSoGiaTdDd.FirstOrDefault(x => x.Mahs == Mahs);
                    getHs.Giakychon = giaKychonRsN1;
                    _db.ChiSoGiaTdDd.Update(getHs);
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
                    int compareThang = int.Parse(getHs.Thang) - 1;
                    var priceRoot = _db.ChiSoGiaTd.FirstOrDefault(x => x.Nam == getHs.Nam
                      && x.Thang == compareThang.ToString()).Giakychon;
                    ViewData["DsDonVi"] = dsdonvi;
                    ViewData["cpiKychon"] = System.Math.Round(giaKychonRsN1, 5);
                    ViewData["listTt"] = _db.ChiSoGiaTdDm.Where(x => x.Matt != "1");
                    ViewData["thang"] = getHs.Thang;
                    ViewData["nam"] = getHs.Nam;
                    ViewData["gia"] = giaKychonRsN1;
                    if (giaKychonRsN1 - priceRoot > 0)
                    {
                        ViewData["compare"] = giaKychonRsN1 - priceRoot;
                        ViewData["state"] = "tang";
                    }
                    else
                    {
                        ViewData["compare"] = priceRoot - giaKychonRsN1;
                        ViewData["state"] = "giam";
                    }
                    ViewData["tenhs"] = _db.ChiSoGiaTdDd.FirstOrDefault(x => x.Mahs == Mahs).Noidung;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["MenuLv1"] = "menu_csg";
                    ViewData["MenuLv2"] = "menu_csgHs";
                    ViewData["MenuLv3"] = "menu_csgHs_ddlastmonth";
                    return View("Views/Admin/Manages/ChiSoGiaTd/Dudoan/Result.cshtml");
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
        [Route("DuDoanChiSoGiaTieuDung/Detail")]
        [HttpGet]
        public IActionResult Detail(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.dudoan", "Index") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    ViewData["model"] = _db.ChiSoGiaTdDmCtDd.Where(x => x.Mahs == Mahs);
                    ViewData["listTt"] = _db.ChiSoGiaTdDm.Where(x => x.Matt != "1");
                    ViewData["Title"] = " Thông tin chi tiết hồ sơ";
                    ViewData["MenuLv1"] = "menu_csg";
                    ViewData["MenuLv2"] = "menu_csgHs";
                    ViewData["MenuLv3"] = "menu_csgHs_ddlastmonth";
                    return View("Views/Admin/Manages/ChiSoGiaTd/Dudoan/Detail.cshtml");
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
        private IQueryable<ChiSoGiaTdDmCt> CheckType(string Type)
        {
            var lastRecord = _db.ChiSoGiaTd.OrderBy(x => x.Id).LastOrDefault();

            var thangDd = (int.Parse(lastRecord.Thang) + 1).ToString();
            var lastyear = (int.Parse(lastRecord.Nam) - 1).ToString();
            var getDm = _db.ChiSoGiaTdDmCt.AsQueryable();
            if(Type== "Lastmonth")
            {
                getDm = getDm.Where(x => x.Nam == lastRecord.Nam && x.Thang == lastRecord.Thang && x.Trangthai!="tb");
            }
            if (Type == "Lastyear")
            {
                getDm = getDm.Where(x => x.Nam == lastyear && x.Thang == lastRecord.Thang);
            }
            if (Type == "Last12")
            {
                getDm = getDm.Where(x => x.Nam == lastyear && x.Thang == "12");
            }
            if (Type == "Average")
            {
                var Average = (from t in _db.ChiSoGiaTdDmCt.Where(x => x.Nam == lastRecord.Nam && x.Trangthai!="tb")
                               group t by new
                               {
                                   t.Masohanghoa,
                                   t.Masonhomhanghoa,
                                   t.QuyensoNt,
                                   t.QuyensoTt,
                                   t.Masogoc,
                                   t.Tenhanghoa
                               }
                                      into grp
                               select new
                               {
                                   grp.Key.Masohanghoa,
                                   grp.Key.Masonhomhanghoa,
                                   grp.Key.QuyensoNt,
                                   grp.Key.QuyensoTt,
                                   grp.Key.Masogoc,
                                   grp.Key.Tenhanghoa,
                                   Giakychon = grp.Sum(t => t.Giakychon) / (Convert.ToDouble(grp.Count())),
                               });
                var checkNull = _db.ChiSoGiaTdDmCt.Where(x => x.Thang == lastRecord.Thang && x.Nam == lastRecord.Nam && x.Trangthai=="tb");
                if (checkNull.Count() <=0)
                {
                    foreach (var item in Average)
                    {
                        var newa = new ChiSoGiaTdDmCt
                        {
                            Masonhomhanghoa = item.Masonhomhanghoa,
                            Masohanghoa = item.Masohanghoa,
                            Giakychon = item.Giakychon,
                            QuyensoNt = item.QuyensoNt,
                            QuyensoTt = item.QuyensoTt,
                            Thang = lastRecord.Thang,
                            Nam = lastRecord.Nam,
                            Tenhanghoa = item.Tenhanghoa,
                            Masogoc = item.Masogoc,
                            Baocao = "Bình quân tháng 1 đến tháng " + lastRecord.Thang,
                            Trangthai = "tb",
                        };
                        _db.ChiSoGiaTdDmCt.Add(newa);

                    }
                }
                
                getDm = getDm.Where(x => x.Nam == lastRecord.Nam && x.Thang == lastRecord.Thang && x.Trangthai == "tb");
                _db.SaveChanges();
            }
            if (Type == "Kygoc")
            {
                var checkNull = _db.ChiSoGiaTdDmCt.Where(x => x.Trangthai == "root");
                if (checkNull.Count()<=0)
                {
                    foreach (var item in _db.ChiSoGiaTdDm.Where(x => x.Matt == "1"))
                    {
                        var newa = new ChiSoGiaTdDmCt
                        {
                            Masonhomhanghoa = item.Masonhomhanghoa,
                            Masohanghoa = item.Masohanghoa,
                            Giakychon = item.Gia,
                            QuyensoNt = item.QuyensoNt,
                            QuyensoTt = item.QuyensoTt,
                            Thang = lastRecord.Thang,
                            Nam = lastRecord.Nam,
                            Tenhanghoa = item.Tenhanghoa,
                            Masogoc = item.Masogoc,
                            Baocao = "Kỳ gốc",
                            Trangthai = "root",
                        };
                        _db.ChiSoGiaTdDmCt.Add(newa);

                    }
                }
                
                getDm = getDm.Where(x => x.Trangthai == "root");
                _db.SaveChanges();
            }
            return (IQueryable<ChiSoGiaTdDmCt>)getDm;
        }
        
        [Route("DuDoanChiSoGiaTieuDung/DropList")]
        [HttpPost]
        public JsonResult DropList(string[] id,string nhom,string Nam,string Type)
        {
            var model = new List<ChiSoGiaTdDmCt>();
            //var modelAverage = new List<ChiSoGiaTdAverage>();
            string result = "";
            for (int i = 0; i < id.Length; i++)
            {
                var list = this.CheckType(Type).Where(x => x.Masogoc == id[i]);
                var t = this.CheckType(Type).Where(x => x.Masogoc == id[i]).Count();
                foreach (var item in list)
                {
                    model.Add(item);
                }
                foreach (var item in model)
                {
                    result += "<option value ='" + item.Masohanghoa + "'>" + item.Tenhanghoa + "</ option >";
                }

            }
            var data = new { status = "success", message = result };
            return Json(data);
        }
    }
}
