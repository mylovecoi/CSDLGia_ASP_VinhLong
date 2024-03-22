using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.ViewModels
{
    public class VMImportExcel
    {
        public int Sheet { get; set; }
        public int LineStart { get; set; }
        public int LineStop { get; set; }
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public IFormFile FormFile { get; set; }
        public string MaDv { get; set; }
        public string Matt { get; set; }
        public string MadiabanBc { get; set; }
        public int Nam { get; set; }
        public int Thang { get; set; }
        public string MaNhom { get; set; }
        public string TenNhom { get; set; }
    }
}
