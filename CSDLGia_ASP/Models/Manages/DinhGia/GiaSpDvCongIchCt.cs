using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaSpDvCongIchCt
    {
        [Key]

        public int Id { get; set; }
        public string HienThi { get; set; }
        public string Mahs { get; set; }
        public string Maspdv { get; set; }
        public string Manhom { get; set; }
        public string Ten { get; set; }
        public string LoaiDoThi { get; set; }
        public string LoaiDoThi2 { get; set; }
        public string Dvt { get; set; }
        public double Mucgiatu { get; set; }
        public double Mucgiaden { get; set; }
        public double Mucgia3 { get; set; }
        public double Mucgia4 { get; set; }
        public int Sapxep { get; set; }
        public string Style { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        [NotMapped]
        public Boolean NhapGia { get; set; }

        public string Madv { get; set; }
        [NotMapped]
        public string Tendv { get; set; }
        [NotMapped]
        public DateTime Thoidiem { get; set; }
        [NotMapped]
        public string Soqd { get; set; }

        // Excel
        [NotMapped]
        public int LineStart { get; set; }
        [NotMapped]
        public int LineStop { get; set; }
        [NotMapped]
        public int Sheet { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public IFormFile FormFile { get; set; }

        [NotMapped]
        public string Tennhom { get; set; }
      

    }
}