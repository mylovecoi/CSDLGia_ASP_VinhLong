using System;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CSDLGia_ASP.Models.Systems
{
    public class NhatKySuDung
    {
        [Key]
        public int Id { get; set; }
        public string Madv { get; set; }
        public IPAddress Diachitruycap { get; set; }
        public string Nguoisudung { get; set; }
        public string Tendangnhap { get; set; }
        public DateTime Thoigian { get; set; }
        public string Chucnang { get; set; }
        public string Hanhdong { get; set; }
        public string Noidung { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
