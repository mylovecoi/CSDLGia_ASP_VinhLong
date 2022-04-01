using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models
{
    public class tblDSTaiKhoan
    {
        public int? STT { get; set; }
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string? MaDonVi { get; set; }

        public string? MaNhomTaiKhoan { get; set; }

        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string? TenTaiKhoan { get; set; }

        [Key]
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string? MatKhau { get; set; }

        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }

        [DefaultValue(false)]
        public bool? TrangThai { get; set; }

        public string? ThongBao { get; set; }

        [DefaultValue(0)]
        public int SoLanDangNhap { get; set; }
        //Chức năng
        public bool? NhapLieu { get; set; }
        public bool? TongHop { get; set; }
        public bool? QuanTri { get; set; }
    }
}
