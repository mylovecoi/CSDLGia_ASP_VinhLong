using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaSpDvCuTheCt
    {
        [Key]

        public int Id { get; set; }

        public string Mahs { get; set; }
        public string Mota { get; set; }

        public double Mucgiatu { get; set; }
        public double Mucgiaden { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Phanloaidv { get; set; }
        public string Trangthai { get; set; }
        [NotMapped]
        public string Madv { get; set; }
        [NotMapped]
        public string Tendv { get; set; }
        [NotMapped]
        public string Tennhom { get; set; }
        [NotMapped]
        public DateTime Thoidiem { get; set; }
        [NotMapped]
        public string Soqd { get; set; }
        [NotMapped]
        public int LineStart { get; set; }
        [NotMapped]
        public int LineStop { get; set; }
        [NotMapped]
        public int Sheet { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public IFormFile FormFile { get; set; }

        // Bổ sung
        public string Tt { get; set; }
        public string Maspdv { get; set; }
        public string TenSpDv { get; set; }
        public string Manhom { get; set; }
        public string Dvt { get; set; }
        public string Mucgia1 { get; set; }
        public string Mucgia2 { get; set; }
        //

    }
}