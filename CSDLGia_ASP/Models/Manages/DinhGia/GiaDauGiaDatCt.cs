using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDauGiaDatCt
    {
        [Key]
        public int Id { get; set; }
        public string Loaidat { get; set; }
        public string Khuvuc { get; set; }
        public string Mota { get; set; }
        public double Dientich { get; set; }
        public double Giakhoidiem { get; set; }
        public double Giadaugia { get; set; }
        public double Giasddat { get; set; }
        public string Mahs { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Solo { get; set; }
        public string Sothua { get; set; }
        public string Tobanbo { get; set; }
        public string Dvt { get; set; }
        public string Sotobanbo { get; set; }
        public string TrangThai { get; set; }
        public string MaDv { get; set; }
        [NotMapped]
        public DateTime ThoiDiem { get; set; }
        [NotMapped]
        public string SoQuyetDinh { get; set; }
        [NotMapped]
        public string PhanLoai { get; set; }
        [NotMapped]
        public string TenDuAn { get; set; }
    }
}
