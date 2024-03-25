using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Systems.Excell
{
    public class ExcellDanhMucLoaiDat
    {
        [Key]
        public int Id { get; set; }
        public string Maloaidat { get; set; }
        public string Loaidat { get; set; }        
        public int LineStart { get; set; }
        [NotMapped]
        public int LineStop { get; set; }

        [NotMapped]
        public int Sheet { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; } = null!;
    }
}
