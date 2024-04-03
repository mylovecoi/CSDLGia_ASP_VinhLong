using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Systems
{
    public class DmLoaiDat
    {
        [Key]
        public int Id { get; set; }
        public string Maloaidat { get; set; }
        public string Loaidat { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        [NotMapped]
        public int LineStart { get; set; }
        [NotMapped]
        public int LineStop { get; set; }
        [NotMapped]
        public int Sheet { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public IFormFile FormFile { get; set; }
    }
}
