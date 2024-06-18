using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models
{
    public class tblHeThong
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string DiaDanh { get; set; }
        public string TenDonVi { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string SoFax { get; set; }
        public string Email { get; set; }
        public string LinkAPIXacthuc { get; set; }
        public string TokenLGSP { get; set; }
        public string MaDiaBanHanhChinh { get; set; }
        public string MaDonViThuThap { get; set; }
        public string FileHDSD { get; set; }
        public string FileHDSDBase64 { get; set; }
        public string FileQuyChe { get; set; }
        public string FileQuyCheBase64 { get; set; }
        public string FileDangKy { get; set; }
        public string FileDangKyBase64 { get; set; }
        public string TimeOut { get; set; }
        [NotMapped]
        public IFormFile FileImportHDSD { get; set; }
        [NotMapped]
        public IFormFile FileImportQuyChe { get; set; }
        [NotMapped]
        public IFormFile FileImportDangKy { get; set; }
    }
}
