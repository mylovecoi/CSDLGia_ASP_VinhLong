using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaNuocShCt
    {
        [Key]
        public int Id { get; set; }
        public string Madv { get; set; }
        public string Mahs { get; set; }
        public string Madoituong { get; set; }
        public string Doituongsd { get; set; }
        public string Namchuathue { get; set; }
        public double Giachuathue { get; set; }
        public string Namchuathue1 { get; set; }
        public double Giachuathue1 { get; set; }
        public string Namchuathue2 { get; set; }
        public double Giachuathue2 { get; set; }
        public string Namchuathue3 { get; set; }
        public double Giachuathue3 { get; set; }
        public string Namchuathue4 { get; set; }
        public double Giachuathue4 { get; set; }
        public string Thuevat { get; set; }
        public string Giacothue { get; set; }
        public string Phibvmttyle { get; set; }
        public string Phibvmt { get; set; }
        public string Thanhtien { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        /*[NotMapped]
        public string Madv { get; set; }*/
        [NotMapped]
        public DateTime Thoidiem { get; set; }
        [NotMapped]
        public int LineStart { get; set; }
        [NotMapped]
        public int LineStop { get; set; }
        [NotMapped]
        public int Sheet { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public IFormFile FormFile { get; set; }

        //Bố sung
        public string TyTrongTieuThu { get; set; }
        public string SanLuong { get; set; }
        public double ThueSuat { get; set; }
        public double DonGia1 { get; set; }
        public double DonGia2 { get; set; }
        public int STTSapxep { get; set; }
        public string STTHienthi { get; set; }
        public string Style { get; set; }

        [NotMapped]
        public string Tendv { get; set; }
        [NotMapped]
        public string SoQD { get; set; }
        [NotMapped]
        public DateTime ThoiDiem { get; set; }


    }
}
