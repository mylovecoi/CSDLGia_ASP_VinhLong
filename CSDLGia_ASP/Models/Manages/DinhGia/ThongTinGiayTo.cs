using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class ThongTinGiayTo
    {
        public int Id { get; set; }
        public int STT { get; set; }
        public string MoTa { get; set; }
        public string FileName { get; set; }
        [NotMapped]
        public IFormFile FileUpLoad { get; set; }
        public string Mahs { get; set; }
        public string Madv { get; set; }
        public string Status { get; set; }
    }
}
