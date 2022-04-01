using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models
{
    [Table("tblDMChucNang")]
    public class tblDMChucNang
    {
        public int? STT { get; set; }
        public string? KiHieu { get; set; }

        [Key]
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string MaChucNang { get; set; }

        public string TenChucNang { get; set; }

        [DefaultValue(1)]
        public int? CapDo { get; set; }

        public string MaGoc { get; set; }

        [DefaultValue(false)]
        public bool TrangThai { get; set; }

        [DefaultValue(false)]
        public bool CongBo { get; set; }

        public string? PhanLoai { get; set; }

        public string? TenBangHoSo { get; set; }

        public string? TenBangChiTiet { get; set; }

        //có thể thay thế bằng Controller/Action
        public string? UrlTongHop { get; set; }

        //Mặc định các quyền tồn tại trong hệ thống (0: hệ thống ko có; 1: hệ thống có chức năng)
        [DefaultValue(0)]
        public bool? DM_DanhSach { get; set; }
        [DefaultValue(0)]
        public bool? DM_ThayDoi { get; set; }
        [DefaultValue(0)]
        public bool? HS_DanhSach { get; set; }
        [DefaultValue(0)]
        public bool? HS_ThayDoi { get; set; }
        [DefaultValue(0)]
        public bool? HS_HoanThanh { get; set; }
        [DefaultValue(0)]
        public bool? K_BaoCao { get; set; }
        [DefaultValue(0)]
        public bool? K_ThongTin { get; set; }

    }
}
