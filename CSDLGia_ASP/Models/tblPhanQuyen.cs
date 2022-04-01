using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models
{
    public class tblPhanQuyen
    {
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string MaChucNang { get; set; }

        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string TenDangNhap { get; set; }

        public bool? PhanQuyen { get; set; }

        //Nhóm danh mục
        [DefaultValue(0)]
        public int? DM_DanhSach { get; set; }
        [DefaultValue(0)]
        public int? DM_ThayDoi { get; set; }
        //Nhóm nhập liệu
        [DefaultValue(0)]
        public int? HS_DanhSach { get; set; }
        [DefaultValue(0)]
        public int? HS_ThayDoi { get; set; }
        [DefaultValue(0)]
        public int? HS_HoanThanh { get; set; }
        //Nhóm khác
        [DefaultValue(0)]
        public int? K_BaoCao { get; set; }
        [DefaultValue(0)]
        public int? K_ThongTin { get; set; }
    }
}
