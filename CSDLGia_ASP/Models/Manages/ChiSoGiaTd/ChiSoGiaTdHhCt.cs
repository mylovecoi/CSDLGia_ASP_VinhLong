using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.ChiSoGiaTd
{
    public class ChiSoGiaTdHhCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Masohanghoa { get; set; }
        public string Masonhomhanghoa { get; set; }
        public string Tenhanghoa { get; set; }
        public string Masogoc { get; set; }
        public string Dvt { get; set; }
        public string Thang { get; set; }
        public string Nam { get; set; }
        public double Giagoc { get; set; }
        public double Giakychon { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
