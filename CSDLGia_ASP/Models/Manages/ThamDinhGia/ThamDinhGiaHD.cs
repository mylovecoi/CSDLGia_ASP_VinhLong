using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.ThamDinhGia
{
    public class ThamDinhGiaHD
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string MaHoiDong { get; set; }
        public string ToTung { get; set; }
        public string CanCuPhapLy { get; set; }
        public string TheoDeNghi { get; set; }
        public int CapHoiDong { get; set; }
        public int LoaiHoiDong { get; set; }
        public string SoQD { get; set; }
        public DateTime NgayQD { get; set; }
        public string CoQuanBanHanh { get; set; }
        public string TenHoiDong { get; set; }
        public string ChuTichHoiDong { get; set; }
        public string ChucVu { get; set; }
        public string NhiemVuHoiDong { get; set; }
        public string NoiDungQD { get; set; }
        public string MaTinhApDung { get; set; }
        public string MaHuyenApDung { get; set; }
        public string FileQD { get; set; }
        public string FileQD_Base64 { get; set; }
        //Trang thái kết nối CSDLQG
        public string TrangThaiCSDLQG { get; set; }
        public DateTime NgayKetNoi { get; set; }
        [NotMapped]
        public List<ThamDinhGiaHDCt> ThamDinhGiaHDCt { get; set; }
        [NotMapped]
        public IFormFile FileUpload { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
