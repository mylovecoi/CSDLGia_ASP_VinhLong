using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models
{
    public class tblDSDonVi
    {
        public int STT { get; set; }

        [Key]
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string MaDonVi { get; set; }
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string TenDonVi { get; set; }
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        //public string CapDo { get; set; }//X,H,T,SSA
        public string MaDiaBan { get; set; }
        public string? MaQHNS { get; set; }
        public string? DiaChi { get; set; }
        public string? ThongTinLienHe { get; set; }
        public string? EmailQuanLy { get; set; }
        public string? EmailQuanTri { get; set; }
        [DefaultValue(3)]
        public int? SoNgayLamViec { get; set; }
        public string? TenDonViHienThi { get; set; }
        public string? TenDonViChuQuan { get; set; }
        public string? ChucVuQuanLy { get; set; }
        public string? ChucVuKyThay { get; set; }
        public string? TenQuanLy { get; set; }
        public string? DiaDanh { get; set; }
        //public string? ChucNang { get; set; } //Tổng hợp; Nhập liệu; Quản trị
    }
}
