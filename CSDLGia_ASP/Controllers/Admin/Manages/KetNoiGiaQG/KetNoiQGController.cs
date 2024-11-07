using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems.API;
using CSDLGia_ASP.ViewModels.Systems.CSDLQuocGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Hosting;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KetNoiGiaQG
{
    public class KetNoiQGController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _env;

        public KetNoiQGController(CSDLGiaDBContext db, IWebHostEnvironment hostingEnv)
        {
            _db = db;
            _env = hostingEnv;
        }


        [Route("KetNoiQG/ThietLap")]
        [HttpGet]
        public IActionResult ThietLap()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.KetNoiAPI_DanhSach.ToList();
                ViewData["Title"] = "Thiết lập kết nối";
                ViewData["MenuLv1"] = "menu_giaqg";
                ViewData["MenuLv2"] = "menu_giaqg_thietlap";

                return View("Views/Admin/Manages/KetNoiGiaQG/ThietLap/ThietLap.cshtml", model);

            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("KetNoiQG/LuuThietLap")]
        [HttpPost]
        public IActionResult Update(KetNoiAPI_DanhSach request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                var model = _db.KetNoiAPI_DanhSach.FirstOrDefault(t => t.Maso == request.Maso);
                if (model == null)
                {
                    var ketnoi = new KetNoiAPI_DanhSach
                    {
                        Maso = request.Maso,
                        NguoiDuyet = request.NguoiDuyet,
                        NguoiTao = request.NguoiTao,
                        MaDonVi = request.MaDonVi,
                        MaDiaBan = request.MaDiaBan,
                        MaBM = request.MaBM,
                    };
                    _db.KetNoiAPI_DanhSach.Add(ketnoi);
                    _db.SaveChanges();
                }
                else
                {
                    model.LinkTruyenPost = request.LinkTruyenPost;
                    model.NguoiDuyet = request.NguoiDuyet;
                    model.NguoiTao = request.NguoiTao;
                    model.MaDonVi = request.MaDonVi;
                    model.MaDiaBan = request.MaDiaBan;
                    model.MaBM = request.MaBM;
                    _db.KetNoiAPI_DanhSach.Update(model);
                    _db.SaveChanges();
                }

                return RedirectToAction("ThietLap", "KetNoiQG");
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        public IActionResult XemDuLieuCSDLQG(VMHoSoTruyenCSDLQG request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                return Ok(getDuLieu(request, _db, _env));
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> TruyenDuLieuCSDLQG(VMHoSoTruyenCSDLQG request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                string maBearer = "Bearer ";
                //Lấy mã kết nối

                HttpClient client_layma = new HttpClient();
                // Thêm các header cần thiết
                client_layma.DefaultRequestHeaders.Add("lgspaccesstoken", request.TokenLGSP);
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, request.LinkAPIXacthuc);
                // Dữ liệu gửi đi
                var requestData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                });
                // Thêm Content-Type vào header của nội dung
                requestData.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                httpRequest.Content = requestData;
                var response = await client_layma.SendAsync(httpRequest);
                // Đọc nội dung phản hồi
                string ketQuaLayMa = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    JObject jsonObject = JObject.Parse(ketQuaLayMa);
                    maBearer += (string)jsonObject["access_token"];
                }
                else
                {
                    return Ok("Không thể kết nối đến LGSP để lấy mã kết nối. Mã lỗi:" + response.StatusCode);
                }

                //Tạo hồ sơ truyền dữ liệu
                string jsonKetQua = getDuLieu(request, _db, _env);

                /* 25.04

                //Truyền dữ liệu
                // Khởi tạo HttpClient
                var client = new HttpClient();
                // Đặt các header cần thiết
                client.DefaultRequestHeaders.Add("lgspaccesstoken", request.TokenLGSP);
                client.DefaultRequestHeaders.Add("Authorization", maBearer);
                var httpTruyen = new HttpRequestMessage(HttpMethod.Post, request.LinkAPIKetNoi);
                // Dữ liệu gửi đi
                var requestTruyen = new StringContent(jsonKetQua, Encoding.UTF8, "application/json");
                // Thêm Content-Type vào header của nội dung
                requestTruyen.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpTruyen.Content = requestData;
                var responseTruyen = await client.SendAsync(httpTruyen);
                */
                var client = new HttpClient();
                var requestTruyen = new HttpRequestMessage(HttpMethod.Post, request.LinkAPIKetNoi);
                requestTruyen.Headers.Add("lgspaccesstoken", request.TokenLGSP);
                requestTruyen.Headers.Add("SENDER_CODE", "");
                requestTruyen.Headers.Add("TRAN_CODE", "");
                requestTruyen.Headers.Add("RECEIVER_CODE", "");
                requestTruyen.Headers.Add("Authorization", maBearer);
                requestTruyen.Content = new StringContent(jsonKetQua, null, "application/json");
                var responseTruyen = await client.SendAsync(requestTruyen);
                // Đọc phản hồi
                string responseBody = await responseTruyen.Content.ReadAsStringAsync();
                if (responseTruyen.IsSuccessStatusCode)
                {
                    /*2024.07.09: chưa kiểm tra xem thông điệp trả về có thành công ko
                     * Nếu thành công thì lưu lại trạng thái (DAKETNOI) và thời gian kết nối vào bảng thông tin
                     */
                    //VMKetQua ketQua = JsonConvert.DeserializeObject<VMKetQua>(responseBody);

                    VMKetQua ketQua = null;
                    try
                    {
                        ketQua = JsonConvert.DeserializeObject<VMKetQua>(responseBody);
                        if (ketQua != null)
                        {
                            // Việc chuyển đổi thành công - Nhận đc thông báo từ CSDLQG

                            if (ketQua.error_code == "0" || ketQua.error_code == "1")
                            {
                                //Lưu thông tin vào dữ liệu
                                switch (request.MaKetNoiAPI)
                                {
                                    case "giahhdvk":
                                        {
                                            var hoSo = _db.GiaHhDvkTh.FirstOrDefault(x => x.Mahs == request.Mahs);
                                            hoSo.TrangThaiCSDLQG = "DAKETNOI";
                                            hoSo.NgayKetNoi = DateTime.Now;
                                            _db.GiaHhDvkTh.Add(hoSo);
                                            _db.SaveChanges();
                                            break;
                                        }

                                    case "giahhdvkdm":
                                        {
                                            var hoSo = _db.GiaHhDvkNhom.FirstOrDefault(x => x.Matt == request.Mahs);
                                            hoSo.TrangThaiCSDLQG = "DAKETNOI";
                                            hoSo.NgayKetNoi = DateTime.Now;
                                            _db.GiaHhDvkNhom.Add(hoSo);
                                            _db.SaveChanges();
                                            break;
                                        }

                                    case "giathuetainguyen":
                                        {
                                            var hoSo = _db.GiaThueTaiNguyen.FirstOrDefault(x => x.Mahs == request.Mahs);
                                            hoSo.TrangThaiCSDLQG = "DAKETNOI";
                                            hoSo.NgayKetNoi = DateTime.Now;
                                            _db.GiaThueTaiNguyen.Add(hoSo);
                                            _db.SaveChanges();
                                            break;
                                        }

                                    case "giathuetainguyendm":
                                        {
                                            var hoSo = _db.GiaThueTaiNguyenNhom.FirstOrDefault(x => x.Manhom == request.Mahs);
                                            hoSo.TrangThaiCSDLQG = "DAKETNOI";
                                            hoSo.NgayKetNoi = DateTime.Now;
                                            _db.GiaThueTaiNguyenNhom.Add(hoSo);
                                            _db.SaveChanges();
                                            break;
                                        }

                                    case "giaspdvcongichdm":
                                        {
                                            //2024.05.14 Do khánh hoà nhập thu gom rác thải vào GiaSpDvToiDa                                            
                                            var hoSo = _db.GiaSpDvToiDaNhom.FirstOrDefault(x => x.Manhom == request.Mahs);
                                            hoSo.TrangThaiCSDLQG = "DAKETNOI";
                                            hoSo.NgayKetNoi = DateTime.Now;
                                            _db.GiaSpDvToiDaNhom.Add(hoSo);
                                            _db.SaveChanges();
                                            break;
                                        }

                                    case "giaspdvcongich":
                                        {
                                            /*2024.05.15: File đính kèm chỉ nhận file .xls
                                             * MA_BM của khánh hoà là 183
                                             */
                                            var hoSo = _db.GiaSpDvToiDa.FirstOrDefault(x => x.Mahs == request.Mahs);
                                            hoSo.TrangThaiCSDLQG = "DAKETNOI";
                                            hoSo.NgayKetNoi = DateTime.Now;
                                            _db.GiaSpDvToiDa.Add(hoSo);
                                            _db.SaveChanges();
                                            break;
                                        }

                                    case "thamdinhgia":
                                        {
                                            var hoSo = _db.ThamDinhGia.FirstOrDefault(x => x.Mahs == request.Mahs);
                                            hoSo.TrangThaiCSDLQG = "DAKETNOI";
                                            hoSo.NgayKetNoi = DateTime.Now;
                                            _db.ThamDinhGia.Add(hoSo);
                                            _db.SaveChanges();
                                            break;
                                        }
                                }
                                //
                                ViewData["Messages"] = ketQua.message;
                                return View("Views/Admin/Error/Success.cshtml");
                            }
                            else
                            {
                                ViewData["Messages"] = ketQua.message;
                                return View("Views/Admin/Error/Error.cshtml");
                            }

                        }
                        else
                        {
                            // Việc chuyển đổi thất bại - Nhận đc thông báo nhưng ko giống định dạng
                            ViewData["Messages"] = responseBody;
                            return View("Views/Admin/Error/Error.cshtml");
                        }
                    }
                    catch (JsonException ex)
                    {
                        // Việc chuyển đổi thất bại và ném ngoại lệ
                        return Ok("Không thể truyền dữ liệu lên CSDL quốc giá. Thông báo lỗi:" + ex.Message);
                    }

                }
                else
                {
                    return Ok("Không thể truyền dữ liệu lên CSDL quốc giá. Mã lỗi:" + responseTruyen.StatusCode);
                }

            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        public static bool TryGetKey(Dictionary<string, int> dictionary, string value, out int ketQua)
        {
            // Lặp qua từng cặp key-value trong từ điển
            foreach (var pair in dictionary)
            {
                // Nếu value của cặp key-value đúng với giá trị tìm kiếm
                if (pair.Key == value)
                {
                    // Trả về value và đánh dấu là thành công
                    ketQua = pair.Value;
                    return true;
                }
            }

            // Không tìm thấy key tương ứng với giá trị
            ketQua = -1;
            return false;
        }

        public static string LaySoQD(string value)
        {
            string ketQua = "0";
            if (!string.IsNullOrEmpty(value))
            {
                string[] parts = value.Split("/");
                string soQD = parts[0];
                ketQua = new string(soQD.Where(char.IsDigit).ToArray());
            }
            return ketQua;
        }

        public static string getDuLieu(VMHoSoTruyenCSDLQG request, CSDLGiaDBContext _db, IWebHostEnvironment _env)
        {
            string jsonKetQua = "";
            switch (request.MaKetNoiAPI)
            {
                case "giadaotaodm":
                    {
                        var model_dm = _db.GiaDvGdDtDm.Where(x => x.MaNhom == request.Mahs);
                        var dmDVT = _db.DmDvt;
                        var dm = new List<VMGiaDaoTaoDM>();
                        foreach (var item in model_dm)
                        {

                            dm.Add(new VMGiaDaoTaoDM
                            {
                                DIA_BAN = request.DIA_BAN,
                                MA_HHDV = item.Maspdv,
                                TEN_HHDV = item.Tenspdv,
                                DOI_TUONG = item.DoiTuong,
                                NGUOI_TAO = request.NGUOI_TAO,
                            });
                        }
                        jsonKetQua = @"{""data"":" + JsonConvert.SerializeObject(dm) + @"}";
                        break;

                    }

                case "giadaotao":
                    {
                        var model_hoso = _db.GiaDvGdDt.FirstOrDefault(x => x.Mahs == request.Mahs);
                        var model_chitiet = _db.GiaDvGdDtCt.Where(x => x.Mahs == request.Mahs);

                        var dm = new List<VMGiaDaoTao>();
                        dm.Add(new VMGiaDaoTao
                        {
                            DIA_BAN = request.DIA_BAN,
                            DONVI_TTSL = request.NGUON_SO_LIEU,
                            NGUON_SO_LIEU = request.NGUON_SO_LIEU,
                            SO_VAN_BAN = model_hoso.Soqd,
                            NGAY_THUC_HIEN = model_hoso.Thoidiem.ToString("yyyyMMdd"),
                            NGAY_BD_HIEU_LUC = model_hoso.Thoidiem.ToString("yyyyMMdd"),
                            NGAY_KT_HIEU_LUC = null,
                            NGUOI_TAO = request.NGUOI_TAO,
                            NGUOI_DUYET = request.NGUOI_DUYET,
                            MA_BM = request.MaBM,
                            FILE_SO_LIEU = "",
                            TEN_FILE = "",
                        });

                        jsonKetQua = @"{""data"":" + JsonConvert.SerializeObject(dm) + @"}";
                        break;

                    }

                case "giahhdvk":
                    {
                        var hoSo = _db.GiaHhDvkTh.FirstOrDefault(x => x.Mahs == request.Mahs);
                        var dmDVT = _db.DmDvt;
                        var hoSoChiTiet = from chitiet in _db.GiaHhDvkThCt.Where(x => x.Mahs == request.Mahs)
                                          join danhmuc in _db.GiaHhDvkDm.Where(x => x.Matt == hoSo.Matt) on chitiet.Mahhdv equals danhmuc.Mahhdv
                                          select new GiaHhDvkThCt
                                          {
                                              Mahhdv = danhmuc.Mahhdv,
                                              Tenhhdv = danhmuc.Tenhhdv,
                                              Manhom = danhmuc.Manhom,
                                              Dvt = danhmuc.Dvt,
                                              Dacdiemkt = danhmuc.Dacdiemkt,
                                              Nguontt = chitiet.Nguontt,
                                              Loaigia = chitiet.Loaigia,
                                              Gia = chitiet.Gia,
                                              Gialk = chitiet.Gialk,
                                              Ghichu = chitiet.Ghichu,
                                          };
                        var giaHHDVK_DSHH = new List<VMGiaHHDVK_DSHH>();
                        Dictionary<string, int> loaiGia = new Dictionary<string, int>();
                        loaiGia["Giá bán buôn"] = 10;
                        loaiGia["Giá bán lẻ"] = 5;

                        Dictionary<string, int> nguonThongTin = new Dictionary<string, int>();
                        nguonThongTin["Do trực tiếp điều tra, thu thập"] = 1;
                        nguonThongTin["Do cơ quan/đơn vị quản lý nhà nước có liên quan cung cấp/báo cáo theo quy định"] = 2;
                        nguonThongTin["Từ thống kê đăng ký giá, kê khai giá, thông báo giá của doanh nghiệp"] = 3;
                        nguonThongTin["Hợp đồng mua tin"] = 4;
                        nguonThongTin["Các nguồn thông tin khác"] = 5;

                        foreach (var item in hoSoChiTiet)
                        {
                            var dvt = dmDVT.FirstOrDefault(x => x.Dvt.ToLower() == item.Dvt.ToLower());
                            int lOAI_GIA;
                            int nGUON_THONG_TIN;
                            TryGetKey(loaiGia, item.Loaigia, out lOAI_GIA);
                            TryGetKey(nguonThongTin, item.Nguontt, out nGUON_THONG_TIN);
                            giaHHDVK_DSHH.Add(new VMGiaHHDVK_DSHH
                            {
                                LOAI_GIA = lOAI_GIA == -1 ? 5 : lOAI_GIA,//Viết hàm lấy loại giá
                                MA_HHDV = item.Mahhdv,
                                DON_VI_TINH = dvt.Madvt,
                                GIA_KY_TRUOC = item.Gialk,
                                GIA_KY_NAY = item.Gia,
                                NGUON_THONG_TIN = nGUON_THONG_TIN == -1 ? 2 : nGUON_THONG_TIN,//Viết hàm lấy nguồn thông tin
                                GHI_CHU = item.Ghichu,
                            });
                        }
                        var giaHHDVK = new List<VMGiaHHDVK>();
                        giaHHDVK.Add(new VMGiaHHDVK
                        {
                            DIA_BAN = request.DIA_BAN,
                            NGUON_SO_LIEU = request.NGUON_SO_LIEU,
                            DONVI_TTSL = request.NGUON_SO_LIEU,
                            DINH_KY = 24,
                            THOI_GIAN_BC_1 = Int16.Parse(hoSo.Thang),
                            THOI_GIAN_BC_2 = Int16.Parse(hoSo.Thang),
                            THOI_GIAN_BC_NAM = Int16.Parse(hoSo.Nam),
                            FILE_DINH_KEM_WORD = hoSo.ipf_word_base64,
                            FILE_DINH_KEM_PDF = hoSo.ipf_pdf_base64,
                            NGUOI_TAO = request.NGUOI_TAO,
                            NGUOI_DUYET = request.NGUOI_DUYET,
                            DS_HHDV_TT = giaHHDVK_DSHH
                        });

                        jsonKetQua = @"{""data"":" + JsonConvert.SerializeObject(giaHHDVK) + @"}";
                        break;
                    }

                case "giahhdvkdm":
                    {
                        var model_giahhdvkdm = _db.GiaHhDvkDm.Where(x => x.Matt == request.Mahs && x.Theodoi == "TD").OrderBy(x => x.Mahhdv);
                        var dmDVT = _db.DmDvt;
                        var giaHHDVKDM = new List<VMGiaHHDVKDM>();
                        foreach (var item in model_giahhdvkdm)
                        {
                            var dvt = dmDVT.FirstOrDefault(x => x.Dvt.ToLower() == item.Dvt.ToLower());
                            giaHHDVKDM.Add(new VMGiaHHDVKDM
                            {
                                DIA_BAN = request.DIA_BAN,
                                NHOM_HHDV = item.Manhom,
                                MA_HHDV = item.Mahhdv,
                                MA_HHDV_TINH_THANH = item.Mahhdv,
                                TEN_HHDV_TINH_THANH = item.Tenhhdv,
                                DAC_DIEM_KY_THUAT = item.Dacdiemkt,
                                DON_VI_TINH = dvt.Madvt,
                                NGUOI_TAO = request.NGUOI_TAO,
                            });
                        }
                        jsonKetQua = @"{""data"":" + JsonConvert.SerializeObject(giaHHDVKDM) + @"}";
                        break;

                    }

                case "giathuetainguyen":
                    {
                        var chiTiet = _db.GiaThueTaiNguyenCt.Where(x => x.Mahs == request.Mahs);
                        var hoSo = _db.GiaThueTaiNguyen.FirstOrDefault(x => x.Mahs == request.Mahs);

                        var giaTaiNguyenCT = new List<VMGiaThueTaiNguyen_DSCT>();
                        List<string> capFields = new List<string> { "Cap6", "Cap5", "Cap4", "Cap3", "Cap2", "Cap1" };
                        foreach (var item in chiTiet.OrderBy(x => x.SapXep))
                        {
                            string maTN = "";
                            int capDo = 1;
                            string maGoc = "";
                            string nhomTN = "";
                            foreach (var capField in capFields)
                            {
                                string capProperty = (string)item.GetType().GetProperty(capField).GetValue(item);

                                if (!string.IsNullOrEmpty(capProperty))
                                {
                                    maTN = capProperty;
                                    capDo = int.Parse(capField.Substring(3)); // Lấy số từ chuỗi "CapX"
                                    maGoc = capDo == 1 ? maTN : maTN.Substring(0, (maTN.Length > 2 ? maTN.Length - 2 : 0));
                                    break;
                                }
                            }

                            if (maTN == "")//do nhận dữ liệu có trường hợp để trống mã tài nguyên
                            {
                                continue;
                            }
                            //Gán nhóm tài nguyên để cho trường hợp truyền từng Mục tài nguyên
                            nhomTN = Regex.Replace(maTN, @"\d", "");
                            //Check gửi từng nhóm
                            //if(nhomTN == "IV" && item.Gia > 0)                                
                            //if(nhomTN == "IV")                                
                            giaTaiNguyenCT.Add(new VMGiaThueTaiNguyen_DSCT
                            {
                                MA_TAI_NGUYEN = maTN,
                                GIA_TINH_THUE = item.Gia,
                                THUE_SUAT = 0,
                                GHI_CHU = "Giá tính thuế tài nguyên",
                            });
                        }

                        var giaTaiNguyen = new List<VMGiaThueTaiNguyen>();
                        giaTaiNguyen.Add(new VMGiaThueTaiNguyen
                        {
                            DIA_BAN = request.DIA_BAN,
                            DONVI_TTSL = request.NGUON_SO_LIEU,
                            NGUON_SO_LIEU = request.NGUON_SO_LIEU,
                            SO_VAN_BAN = hoSo.Soqd,
                            NGAY_THUC_HIEN = hoSo.Thoidiem.ToString("yyyyMMdd"),
                            NGAY_BD_HIEU_LUC = hoSo.Thoidiem.ToString("yyyyMMdd"),
                            NGAY_KT_HIEU_LUC = null,
                            NGUOI_TAO = request.NGUOI_TAO,
                            NGUOI_DUYET = request.NGUOI_DUYET,
                            DS_TAI_NGUYEN_CT = giaTaiNguyenCT
                        });
                        jsonKetQua = @"{""data"":" + JsonConvert.SerializeObject(giaTaiNguyen) + @"}";
                        break;
                    }

                case "giathuetainguyendm":
                    {

                        //Chỉ lấy các danh mục theo hồ sơ đã kê khai giá
                        var hoSoChiTiet = (from hosoct in _db.GiaThueTaiNguyenCt
                                           join hoso in _db.GiaThueTaiNguyen.Where(x => x.Manhom == request.Mahs) on hosoct.Mahs equals hoso.Mahs
                                           select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiNguyenCt
                                           {
                                               Cap1 = hosoct.Cap1,
                                               Cap2 = hosoct.Cap2,
                                               Cap3 = hosoct.Cap3,
                                               Cap4 = hosoct.Cap4,
                                               Cap5 = hosoct.Cap5,
                                               Cap6 = hosoct.Cap6,
                                               Ten = hosoct.Ten,
                                               Dvt = hosoct.Dvt,
                                               Gia = hosoct.Gia,
                                               SapXep = hosoct.SapXep,
                                           });
                        if (!hoSoChiTiet.Any())
                        {
                            return "Danh mục chưa phát sinh hồ sơ giá thuế tài nguyên để so sánh và gửi danh mục lên cơ sở dữ liệu quốc giá";
                        }

                        var dmDVT = _db.DmDvt;
                        var giaTaiNguyenDM = new List<VMGiaThueTaiNguyenDM>();
                        List<string> capFields = new List<string> { "Cap6", "Cap5", "Cap4", "Cap3", "Cap2", "Cap1" };
                        //Lấy danh mục
                        List<string> listDM = new List<string>();
                        var model_giatndm = _db.GiaThueTaiNguyenDm.Where(x => x.Manhom == request.Mahs && x.Theodoi == "TD").OrderBy(x => x.Sapxep);
                        foreach (var item in model_giatndm)
                        {
                            foreach (var capField in capFields)
                            {
                                string capProperty = (string)item.GetType().GetProperty(capField).GetValue(item);

                                if (!string.IsNullOrEmpty(capProperty))
                                {
                                    listDM.Add(capProperty);
                                    break;
                                }
                            }
                        }

                        //Lấy danh mục có trong bảng kê khai và listDM để tạo danh mục xuất ra
                        List<string> listDM_hientai = new List<string>();

                        foreach (var item in hoSoChiTiet.OrderBy(x => x.SapXep))
                        {
                            var dvt = dmDVT.FirstOrDefault(x => x.Dvt.ToLower() == item.Dvt.ToLower());

                            string maTN = "";
                            int capDo = 1;
                            string maGoc = "";

                            foreach (var capField in capFields)
                            {
                                string capProperty = (string)item.GetType().GetProperty(capField).GetValue(item);

                                if (!string.IsNullOrEmpty(capProperty))
                                {
                                    maTN = capProperty;
                                    capDo = int.Parse(capField.Substring(3)); // Lấy số từ chuỗi "CapX"
                                    if (capDo == 1)
                                    {
                                        maGoc = null;
                                    }
                                    else if (capDo == 2)
                                    {
                                        maGoc = Regex.Replace(maTN, @"\d", "");
                                    }
                                    else
                                    {
                                        maGoc = maTN.Substring(0, (maTN.Length > 2 ? maTN.Length - 2 : 0));
                                    }

                                    break;
                                }
                            }
                            /*Xử lý nếu  nằm trong danh mục thì 
                              - Nằm trong danh mục thì TAI_NGUYEN_BTC = maTN;
                              - Ko trong danh mục thì lấy TAI_NGUYEN_BTC = mã tài nguyên trong danh mục gần nhất
                             */
                            string maBTC = maTN;
                            if (!listDM.Contains(maTN))
                            {
                                maBTC = maGoc;
                                //Kiểm tra mã gốc xem có nằm trong danh mục không nếu ko thì lùi lại thêm 1 lần
                                if (!listDM.Contains(maBTC))
                                {
                                    maBTC = maBTC.Substring(0, maBTC.Length - 2);
                                }
                            }
                            //gán lại mã cho mục II210103 do theo dm 05/VBHN-BTC tài nguyên này ở nhóm II2401
                            if (maTN == "II210103")
                            {
                                maGoc = "II2401";
                            }

                            //Kiểm tra trong danh muc nếu chưa có thì thêm vào
                            if (!listDM_hientai.Contains(maTN))
                            {
                                //Thử bỏ qua mã cấp 1
                                //if (capDo > 1)
                                giaTaiNguyenDM.Add(new VMGiaThueTaiNguyenDM
                                {
                                    DIA_BAN = request.DIA_BAN,
                                    MA_TAI_NGUYEN = maTN,
                                    TEN_TAI_NGUYEN = item.Ten,
                                    CAP_TAI_NGUYEN = capDo,
                                    DON_VI_TINH = (dvt == null ? null : dvt.Madvt), // Kiểm tra null trước khi truy cập thuộc tính
                                    MA_TAI_NGUYEN_TINH_CHA = maGoc,
                                    TAI_NGUYEN_BTC = maBTC,
                                    NGUOI_TAO = request.NGUOI_TAO,
                                });
                                listDM_hientai.Add(maTN);
                                //jsonKetQua = @"{""data"":" + JsonConvert.SerializeObject(giaTaiNguyenDM) + @"}";
                                //break;
                            }
                        }
                        jsonKetQua = @"{""data"":" + JsonConvert.SerializeObject(giaTaiNguyenDM) + @"}";
                        break;
                    }

                case "giaspdvcongichdm":
                    {
                        //2024.05.14 Do khánh hoà nhập thu gom rác thải vào GiaSpDvToiDa
                        //Chỉ lấy danh mục có đơn vị tính
                        var model = _db.GiaSpDvToiDaDm.Where(x => x.Manhom == request.Mahs).OrderBy(x => x.Sapxep);
                        var dmDVT = _db.DmDvt;
                        var danhMuc = new List<VMGiaDichVuRacThaiDM>();
                        foreach (var item in model.OrderBy(x => x.Sapxep))
                        {
                            var dvt = dmDVT.FirstOrDefault(x => x.Dvt.ToLower() == item.Dvt.ToLower());
                            if (!string.IsNullOrEmpty(item.Dvt) && dvt != null)
                                danhMuc.Add(new VMGiaDichVuRacThaiDM
                                {
                                    DIA_BAN = request.DIA_BAN,
                                    MA_DV_VCTG = item.Maspdv,
                                    TEN_DV_VCTG = item.Tenspdv,
                                    MO_TA = item.Tenspdv,
                                    DON_VI_TINH = dvt.Madvt,
                                    NGUOI_TAO = request.NGUOI_TAO,
                                });
                        }

                        jsonKetQua = @"{""data"":" + JsonConvert.SerializeObject(danhMuc) + @"}";
                        break;
                    }

                case "giaspdvcongich":
                    {
                        /*2024.05.15: File đính kèm chỉ nhận file .xls
                         * MA_BM của khánh hoà là 183
                         */
                        var model = _db.GiaSpDvToiDa.FirstOrDefault(x => x.Mahs == request.Mahs);
                        var model_file = _db.ThongTinGiayTo.FirstOrDefault(t => t.Mahs == model.Mahs);
                        string fileBase64 = "";
                        if (model_file == null)
                        {
                            return "Hồ sơ chưa có file đính kèm để gửi dữ liệu lên cơ sở dữ liệu quốc giá";

                        }
                        else
                        {
                            //Kiểm tra lại đường dẫn của file
                            string path = _env.WebRootPath + "/UpLoad/File/ThongTinGiayTo/" + model_file.FileName;
                            if (!System.IO.File.Exists(path))
                            {
                                return "Hồ sơ chưa có file đính kèm để gửi dữ liệu lên cơ sở dữ liệu quốc giá";
                            }
                            else
                            {
                                // Đọc tất cả dữ liệu từ tập tin
                                byte[] fileBytes = System.IO.File.ReadAllBytes(path);

                                // Chuyển đổi dữ liệu thành mã base64
                                fileBase64 = Convert.ToBase64String(fileBytes);
                            }

                        }
                        var giaDichVuRacThai = new List<VMGiaDichVuRacThai>();

                        giaDichVuRacThai.Add(new VMGiaDichVuRacThai
                        {
                            DIA_BAN = request.DIA_BAN,
                            DONVI_TTSL = request.NGUON_SO_LIEU,
                            NGUON_SO_LIEU = request.NGUON_SO_LIEU,
                            SO_VAN_BAN = model.Soqd,
                            NGAY_THUC_HIEN = model.Thoidiem.ToString("yyyyMMdd"),
                            NGAY_BD_HIEU_LUC = model.Thoidiem.ToString("yyyyMMdd"),
                            NGAY_KT_HIEU_LUC = null,
                            NGUOI_TAO = request.NGUOI_TAO,
                            NGUOI_DUYET = request.NGUOI_DUYET,
                            MA_BM = request.MaBM,
                            FILE_SO_LIEU = fileBase64,
                            TEN_FILE = model_file.FileName,
                        });

                        jsonKetQua = @"{""data"":" + JsonConvert.SerializeObject(giaDichVuRacThai) + @"}";
                        break;
                    }

                case "thamdinhgiahd":
                    {
                        //var model_hoso = _db.ThamDinhGia.FirstOrDefault(x => x.Mahs == request.Mahs);
                        var model = _db.ThamDinhGiaHD.FirstOrDefault(t => t.MaHoiDong == request.Mahs);

                        if (model == null || string.IsNullOrEmpty(model.FileQD_Base64))
                        {
                            return "Hồ sơ chưa có file đính kèm để gửi dữ liệu lên cơ sở dữ liệu quốc giá";
                        }

                        var hoiDongTDG = new List<VMHoiDongThamDinhGia>();
                        //Lấy danh sách thành viên hội đồng
                        var hoiDongTDG_ThanhVien = new List<VMHoiDongThamDinhGia_DSTVHD>();
                        var model_thanhvien = _db.ThamDinhGiaHDCt.Where(x => x.MaHoiDong == model.MaHoiDong).ToList();
                        if (model_thanhvien.Any())
                        {
                            foreach (var item in model_thanhvien.OrderBy(x => x.STT))
                                hoiDongTDG_ThanhVien.Add(new VMHoiDongThamDinhGia_DSTVHD
                                {
                                    HO_TEN = item.HoTen,
                                    CHUC_VU = item.ChucVu,
                                    VAI_TRO = item.VaiTro,
                                });
                        }
                        //Lấy tài liệu đính kèm
                        var hoiDongTDG_DinhKem = new List<VMHoiDongThamDinhGia_DSDK>();
                        hoiDongTDG_DinhKem.Add(new VMHoiDongThamDinhGia_DSDK
                        {
                            TEN_FILE = model.FileQD,
                            FILE_DINH_KEM = model.FileQD_Base64,
                        });
                        //Thông hội đồng thẩm định
                        hoiDongTDG.Add(new VMHoiDongThamDinhGia
                        {
                            DIA_BAN = request.DIA_BAN,
                            MA_HOI_DONG = model.MaHoiDong,
                            TO_TUNG = model.ToTung,
                            CAN_CU_PHAP_LY = model.CanCuPhapLy,
                            THEO_DE_NGHI_CUA = model.TheoDeNghi,
                            CAP_HOI_DONG = model.CapHoiDong,
                            LOAI_HINH_HOI_DONG = model.LoaiHoiDong,
                            SO_QUYET_DINH_THANH_LAP = model.SoQD,
                            NGAY_BAN_HANH = model.NgayQD.ToString("yyyyMMdd"),
                            CO_QUAN_BAN_HANH = model.CoQuanBanHanh,
                            TEN_HOI_DONG = model.TenHoiDong,
                            CHU_TICH_HOI_DONG = model.ChuTichHoiDong,
                            CHUC_VU = model.ChucVu,
                            NHIEM_VU_HOI_DONG = model.NhiemVuHoiDong,
                            NOI_DUNG_QUYET_DINH = model.NoiDungQD,
                            MA_TINH_THANH = model.MaTinhApDung,
                            MA_QUAN_HUYEN = model.MaHuyenApDung,
                            NGUOI_TAO = request.NGUOI_TAO,
                            DS_TV_HOI_DONG = hoiDongTDG_ThanhVien,
                            DS_DINH_KEM = hoiDongTDG_DinhKem,
                        });

                        jsonKetQua = @"{""data"":" + JsonConvert.SerializeObject(hoiDongTDG) + @"}";
                        break;
                    }

                case "thamdinhgia":
                    {
                        var model_hoso = _db.ThamDinhGia.FirstOrDefault(x => x.Mahs == request.Mahs);
                        var model_chitiet = _db.ThamDinhGiaCt.Where(t => t.Mahs == model_hoso.Mahs).ToList();
                        var model_giayto = _db.ThongTinGiayTo.Where(t => t.Mahs == model_hoso.Mahs).ToList();
                        var model_hoidong = _db.ThamDinhGiaHD.FirstOrDefault(t => t.Mahs == model_hoso.Mahs);

                        //Lấy tài liệu đính kèm
                        var dinhKem = new List<VMThamDinhGia_DSDK>();
                        foreach (var item in model_giayto)
                        {
                            string fileBase64 = "";
                            string path = _env.WebRootPath + "/UpLoad/File/ThongTinGiayTo/" + item.FileName;
                            if (System.IO.File.Exists(path))                           
                            {
                                // Đọc tất cả dữ liệu từ tập tin
                                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                                // Chuyển đổi dữ liệu thành mã base64
                                fileBase64 = Convert.ToBase64String(fileBytes);
                            }
                            dinhKem.Add(new VMThamDinhGia_DSDK
                            {
                                TEN_FILE = item.FileName,
                                FILE_DINH_KEM = fileBase64,
                            });
                        }
                        //Lấy chi tiết hồ sơ
                        var chiTiet = new List<VMThamDinhGia_DSTS>();
                        foreach (var item in model_chitiet)
                        {
                            chiTiet.Add(new VMThamDinhGia_DSTS
                            {
                                TEN_TAI_SAN = item.Tents,
                                DAC_DIEM=item.Dacdiempl,
                                DON_VI_TINH = item.Dvt,
                                SO_LUONG = item.Sl,
                                DON_GIA = item.Giatritstd,
                                THANH_TIEN = item.Giatritstd,
                            });
                        }

                            //Hồ sơ
                            var hoSo = new List<VMThamDinhGia>();                       
                        hoSo.Add(new VMThamDinhGia
                        {
                            DIA_BAN = request.DIA_BAN,
                            MA_HOI_DONG_DINH_GIA_TAI_SAN = model_hoidong != null ? model_hoidong.MaHoiDong : "",
                           
                            NGUOI_DUYET_ID = request.NGUOI_TAO,
                            DS_CHI_TIET_TS_BP = chiTiet,
                            DS_DINH_KEM = dinhKem,
                        });

                        jsonKetQua = @"{""data"":" + JsonConvert.SerializeObject(hoSo) + @"}";
                        break;
                    }
            }
            return jsonKetQua;
        }
    }

}
