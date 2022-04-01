using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models
{
    public class tblDSDiaBan
    {
        public int STT { get; set; }

        [Key]
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string MaDiaBan { get; set; }
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string TenDiaBan { get; set; }
        [DefaultValue("H")]
        public string CapDo { get; set; }//X,H,T,SSA

        public string? GhiChu { get; set; }
    }
}
