using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaKhungGiaDatCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Vungkt { get; set; }
        /*public string Loaixa { get; set; }*/
        public double Giattdb { get; set; }
        public double Giatddb { get; set; }
        public double Giatttd { get; set; }
        public double Giatdtd { get; set; }
        public double Giattmn { get; set; }
        public double Giatdmn { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }        
        public string Madv { get; set; }
        [NotMapped]
        public string Tendv { get; set; }
        [NotMapped]
        public DateTime Thoidiem { get; set; }
        [NotMapped]
        public string SoQD { get; set; }
    }
}
