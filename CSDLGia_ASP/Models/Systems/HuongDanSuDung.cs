using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace CSDLGia_ASP.Models.Systems
{
    public class HuongDanSuDung
    {
        [Key]
        public int Id { get; set; }
        public string TenChucNang { get; set; }
        public string FileGoc { get; set; }
        [NotMapped]
        public IFormFile FileGocUpload { get; set; }
        public string FileMau { get; set; }
        [NotMapped]
        public IFormFile FileMauUpload { get; set; }
        public int STTSapxep { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}
