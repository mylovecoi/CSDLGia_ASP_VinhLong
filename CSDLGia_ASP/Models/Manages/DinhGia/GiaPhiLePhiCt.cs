using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaPhiLePhiCt
    {
        [Key]
        public int Id { get; set; }
        public string Madv { get; set; }
        public string Mahs { get; set; }
        public string Phanloai { get; set; }
        public string Ptcp { get; set; }
        public string Dvt { get; set; }
        public double Phantram { get; set; }
        public double Mucthutu { get; set; }
        public double Mucthuden { get; set; }
        public double Giatu { get; set; }
        public double Giaden { get; set; }
        public string Ghichu { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public int STTSapxep { get; set; }
        public string STTHienthi { get; set; }
        public string Style { get; set; }
        [NotMapped]
        public DateTime ThoiDiem { get; set; }
        [NotMapped]
        public string SoQD { get; set; }    
        [NotMapped]
        public string TenDv { get; set; }

        public string NhanHieu { get; set; }
        public string NuocSxLr { get; set; }
        public string TheTich { get; set; }
        public string SoNguoiTaiTrong { get; set; }
        public double DonGia { get; set; }
        public string MoTa { get; set; }
        public string TiLe { get; set; }



    }
}
