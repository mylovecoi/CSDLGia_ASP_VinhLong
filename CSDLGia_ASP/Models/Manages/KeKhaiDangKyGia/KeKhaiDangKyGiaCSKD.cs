using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia
{
    public class KeKhaiDangKyGiaCSKD
    {
        [Key]
        public int Id { get; set; }
        public string MaDv { get; set; }
        public string MaNghe { get; set; }
        public string MaCqCq { get; set; }
        public string MaCsKd { get; set; }
        public string TenCsKd { get; set; }
        public string DiaChi { get; set; }
        public string SoDt { get; set; }
        [NotMapped]
        public string TenDv { get; set; }
    }
}
