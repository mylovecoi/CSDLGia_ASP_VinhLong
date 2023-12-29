using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaLpTbNhaCtClCl
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Capnha { get; set; }
        public string Thoigiansd { get; set; }
        public string Tylehm { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
