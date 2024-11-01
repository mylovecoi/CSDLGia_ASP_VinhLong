using CSDLGia_ASP.Database;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.ViewModels.Systems.KetNoiGiaDichVu;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CSDLGia_ASP.Models.Systems.KetNoiGiaDichVu;
using System.Collections.Generic;

namespace CSDLGia_ASP.Controllers.Admin.Systems.API
{
    public class KetNoiGiaDichVuController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KetNoiGiaDichVuController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("KetNoiGiaDichVu/DanhSachDoanhNghiep")]
        public IActionResult DanhSachDoanhNghiep()
        {
            var doanhNghiepDVLTs = _db.DoanhNghiepDVLT.ToList();
            //Kiểm tra doanh nghiệp trên hệ thống
            var sDMadv= _db.Company.Select(Company=>Company.Madv).ToList();
            ViewData["Title"] = "Danh sách doanhnghiep";
            
            ViewData["MenuLv1"] = "menu_qlketnoi";
            ViewData["MenuLv2"] = "menu_dsdoanhnghiepdvlt";
            foreach(var item in doanhNghiepDVLTs)
            {
                if (sDMadv.Contains(item.masothue))
                {
                    item.trangthai = "Đã có trên hệ thống";
                }
                else {
                    item.trangthai = "Chưa có trên hệ thống";
                }
            }
            return View("Views/Admin/Systems/KetNoiGiaDichVu/Index_DoanhNghiep.cshtml", doanhNghiepDVLTs);
        }

        [Route("KetNoiGiaDichVu/LayDanhSachDoanhNghiepDVLT")]
        [HttpPost]
        public async Task<IActionResult> LayDanhSachDoanhNghiepDVLT(VMAPIDoanhNghiepDVLT vMAPIDoanhNghiepDVLT)
        {
            //string url = "http://giadichvu.life/";

            //var parameters_data = new Dictionary<string, string>
            //  {
            //      { "sadmin", vMAPIDoanhNghiepDVLT.sadmin},
            //      { "cqcq", vMAPIDoanhNghiepDVLT.cqcq },

            //  };

            //string data_client = await Helpers.GetDataClient(url, "api/doanhnghiep", parameters_data);
            ////JavaScriptSerializer j = new JavaScriptSerializer();
            ////object a = j.Deserialize(str, typeof(object));
            ////string json = JsonConvert.SerializeObject(data_client);
            //return Ok(data_client);
            HttpClient client = new HttpClient();
            // Thêm một header tùy chỉnh vào yêu cầu HTTP
            client.DefaultRequestHeaders.Add("sadmin", vMAPIDoanhNghiepDVLT.sadmin);
            client.DefaultRequestHeaders.Add("cqcq", vMAPIDoanhNghiepDVLT.cqcq);

            // Tạo một đối tượng HttpRequestMessage với phương thức POST
            var request = new HttpRequestMessage(HttpMethod.Post, vMAPIDoanhNghiepDVLT.linkAPI);

            // Thêm dữ liệu cần gửi lên vào nội dung của yêu cầu
            //request.Content = new StringContent("{\"name\":\"John\",\"age\":25}");

            // Gửi yêu cầu và nhận phản hồi
            var response = await client.SendAsync(request);
            string ketQua = "";
            // Kiểm tra trạng thái của phản hồi
            if (response.IsSuccessStatusCode)
            {
                ketQua = await response.Content.ReadAsStringAsync();
            }
            //Kiêm tra kết quả trả về
            try
            {
                VMAPIDVLT vMAPIDVLT = JsonConvert.DeserializeObject<VMAPIDVLT>(ketQua);
                return Ok(vMAPIDVLT.thongbao);
                // Do something with the deserialized object
            }
            catch{  }
            //catch (JsonException ex)
          
            //Chuyển đổi dữ liệu
            var model = JsonConvert.DeserializeObject<List<VMDoanhNghiepCsKd>>(ketQua);
            var dn = new List<DoanhNghiepDVLT>();
            var csdn = new List<CoSoKinhDoanhDVLT>();
            List<string> list_dn = _db.DoanhNghiepDVLT.Select(t => t.masothue).ToList();
            List<string> list_cskd = _db.CoSoKinhDoanhDVLT.Select(t => t.macskd).ToList();
            //return Ok(model);
            foreach (var item in model)
            {
                if (!list_dn.Contains(item.masothue))
                {
                    dn.Add(new DoanhNghiepDVLT
                    {
                        tendn = item.tendn,
                        masothue = item.masothue,
                        diachidn = item.diachidn,
                        teldn = item.teldn,
                        faxdn = item.faxdn,
                        email = item.email,
                        noidknopthue = item.noidknopthue,
                        giayphepkd = item.giayphepkd,
                        chucdanhky = item.chucdanhky,
                        nguoiky = item.nguoiky,
                        diadanh = item.diadanh,
                        tailieu = item.tailieu,
                        cqcq = item.cqcq,
                    });

                    if (item.ds_cskd.Any())
                    {
                        foreach (var cs in item.ds_cskd)
                        {
                            if (!list_cskd.Contains(cs.macskd))
                            {
                                csdn.Add(new CoSoKinhDoanhDVLT
                                {
                                    macskd = cs.macskd,
                                    masothue = cs.masothue,
                                    tencskd = cs.tencskd,
                                    diachikd = cs.diachikd,
                                    telkd = cs.telkd,
                                    toado = cs.toado,
                                    link = cs.link,
                                    cqcq = cs.cqcq,
                                    ghichu = cs.ghichu,
                                });
                            }
                        }
                    }
                }
            }
            _db.DoanhNghiepDVLT.AddRange(dn);
            _db.CoSoKinhDoanhDVLT.AddRange(csdn);
            _db.SaveChanges();
            return RedirectToAction("DanhSachDoanhNghiep", "KetNoiGiaDichVu");
        }

        [Route("KetNoiGiaDichVu/DanhSachHoSoKeKhaiDVLT")]
        public IActionResult DanhSachHoSoKeKhaiDVLT()
        {
            var hoSoKeKhai = _db.HoSoKeKhaiGia.ToList();
            //Kiểm tra doanh nghiệp trên hệ thống

            ViewData["Title"] = "Danh sách doanhnghiep";
            
            ViewData["MenuLv1"] = "menu_qlketnoi";
            ViewData["MenuLv2"] = "menu_dsdoanhnghiepdvlt";
            return View("Views/Admin/Systems/KetNoiGiaDichVu/Index_HoSo.cshtml", hoSoKeKhai);
        }



        [Route("KetNoiGiaDichVu/LayDanhSachHoSoDVLT")]
        [HttpPost]
        public async Task<IActionResult> LayDanhSachHoSoDVLT(VMAPIHoSoKeKhaiDVLT vMAPIHoSoKeKhaiDVLT)
        {
            HttpClient client = new HttpClient();
            // Thêm một header tùy chỉnh vào yêu cầu HTTP
            client.DefaultRequestHeaders.Add("sadmin", vMAPIHoSoKeKhaiDVLT.sadmin);
            client.DefaultRequestHeaders.Add("cqcq", vMAPIHoSoKeKhaiDVLT.cqcq);
            client.DefaultRequestHeaders.Add("tungay", vMAPIHoSoKeKhaiDVLT.tungay.ToString("yyyy-MM-dd"));
            client.DefaultRequestHeaders.Add("denngay", vMAPIHoSoKeKhaiDVLT.denngay.ToString("yyyy-MM-dd"));

            // Tạo một đối tượng HttpRequestMessage với phương thức POST
            var request = new HttpRequestMessage(HttpMethod.Post, vMAPIHoSoKeKhaiDVLT.linkAPI);

            // Gửi yêu cầu và nhận phản hồi
            var response = await client.SendAsync(request);
            string ketQua = "";
            // Kiểm tra trạng thái của phản hồi
            if (response.IsSuccessStatusCode)
            {
                ketQua = await response.Content.ReadAsStringAsync();
            }
            //Kiểm tra xem có lấy được dữ liệu ko            
            try
            {
                VMAPIDVLT vMAPIDVLT = JsonConvert.DeserializeObject<VMAPIDVLT>(ketQua);
                return Ok(vMAPIDVLT.thongbao);
                // Do something with the deserialized object
            }
            //catch (JsonException ex)
            //{}

            catch {  }


            var model = JsonConvert.DeserializeObject<List<VMHoSoKeKhaiGia>>(ketQua);
            var hoSo = new List<HoSoKeKhaiGia>();
            var hoSoChiTiet = new List<HoSoKeKhaiGia_ChiTiet>();
            List<string> listHoSo = _db.HoSoKeKhaiGia.Select(t => t.mahs).ToList();

            //return Ok(model);
            foreach (var item in model)
            {
                if (!listHoSo.Contains(item.mahs))
                {
                    hoSo.Add(new HoSoKeKhaiGia
                    {
                        mahs = item.mahs,
                        macskd = item.macskd,
                        masothue = item.masothue,
                        ngaynhap = item.ngaynhap,
                        socv = item.socv,
                        socvlk = item.socvlk,
                        ngaycvlk = item.ngaycvlk,
                        ngayhieuluc = item.ngayhieuluc,
                        ttnguoinop = item.ttnguoinop,
                        ngaynhan = item.ngaynhan,
                        sohsnhan = item.sohsnhan,
                        ghichu = item.ghichu,
                        ngaychuyen = item.ngaychuyen,
                        lydo = item.lydo,
                        //trangthai = item.trangthai,
                        cqcq = item.cqcq,
                        dvt = item.dvt,
                        phanloai = item.phanloai,
                        plhs = item.plhs,
                        tendn = item.tendn,
                        tencskd = item.tencskd,
                        loaihang = item.loaihang,
                        thoihan = item.thoihan,
                        giaycnhangcs = item.giaycnhangcs,
                        filedk1 = item.filedk1,
                        filedk2 = item.filedk2,
                        filedk3 = item.filedk3,
                        soqdgiaycnhangcs = item.soqdgiaycnhangcs,
                        giaycnhangcstungay = item.giaycnhangcstungay,
                        giaycnhangcsdenngaypublic = item.giaycnhangcsdenngaypublic,
                    });

                    if (item.ds_cths.Any())
                    {
                        foreach (var chiTiet in item.ds_cths)
                        {

                            hoSoChiTiet.Add(new HoSoKeKhaiGia_ChiTiet
                            {
                                macskd = chiTiet.macskd,
                                mahs = chiTiet.mahs,
                                maloaip = chiTiet.mahs,
                                loaip = chiTiet.loaip,
                                qccl = chiTiet.qccl,
                                sohieu = chiTiet.sohieu,
                                ghichu = chiTiet.ghichu,
                                mucgialk = chiTiet.mucgialk,
                                mucgiakk = chiTiet.mucgiakk,
                                tendoituong = chiTiet.tendoituong,
                                apdungpublic = chiTiet.apdungpublic,
                            });

                        }
                    }
                }
            }
            _db.HoSoKeKhaiGia.AddRange(hoSo);
            _db.HoSoKeKhaiGia_ChiTiet.AddRange(hoSoChiTiet);
            _db.SaveChanges();
            return RedirectToAction("DanhSachHoSoKeKhaiDVLT", "KetNoiGiaDichVu");
        }

    }
}
