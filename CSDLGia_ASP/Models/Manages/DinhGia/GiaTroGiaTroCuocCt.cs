using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaTroGiaTroCuocCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Maspdv { get; set; }
        public string Mota { get; set; }
        public double Dongia { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Trangthai { get; set; }
        public string Madv { get; set; }
        [NotMapped]
        public string Tenspdv { get; set; }
        public int LineStart { get; set; }
        [NotMapped]
        public int LineStop { get; set; }
        [NotMapped]
        public int Sheet { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public IFormFile FormFile { get; set; }

        [NotMapped]
        public string Tendv { get;set; }
        [NotMapped]
        public string SoQD { get; set; }
        [NotMapped]
        public DateTime Thoidiem { get; set; }
        public string Dvt { get; set; }
    }
}
