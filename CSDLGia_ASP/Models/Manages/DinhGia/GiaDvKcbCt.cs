using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDvKcbCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Madv { get; set; }
        public string Maspdv { get; set; }
        public string Manhom { get; set; }
        public string Dvt { get; set; }
        public double Giadv { get; set; }
        public string Ghichu { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Phanloai { get; set; }
        public string Madichvu { get; set; }
        public string Tenspdv { get; set; }
        [NotMapped]
        public int LineStart { get; set; }
        [NotMapped]
        public int LineStop { get; set; }
        [NotMapped]
        public int Sheet { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public IFormFile FormFile { get; set; }
        public string Trangthai { get; set; }
    }
}
