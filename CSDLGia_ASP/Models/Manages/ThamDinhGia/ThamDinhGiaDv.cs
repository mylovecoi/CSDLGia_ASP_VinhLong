using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.ThamDinhGia
{
    public class ThamDinhGiaDv
    {
        [Key]
        public int Id { get; set; }
        public string Maso { get; set; }
        public string Tendv { get; set; }
        public string Diachi { get; set; }
        public string Nguoidaidien { get; set; }
        public string Chucvu { get; set; }
        public string Theodoi { get; set; }
        public string Sothe { get; set; }
        public DateTime Ngaycap { get; set; }
        public string Soqddungtd { get; set; }
        public DateTime Ngaydungtd { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
