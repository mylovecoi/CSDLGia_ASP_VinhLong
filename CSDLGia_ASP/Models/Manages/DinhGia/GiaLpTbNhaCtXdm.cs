using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaLpTbNhaCtXdm
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string District { get; set; }
        public string Loaict { get; set; }
        public string Capnha { get; set; }
        public string Dvt { get; set; }
        public string Dongia { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
