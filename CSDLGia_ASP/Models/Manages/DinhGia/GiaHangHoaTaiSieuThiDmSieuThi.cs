using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaHangHoaTaiSieuThiDmSieuThi
    {
        [Key]
        public int Id { get; set; }
        public string Masieuthi { get; set; }
        public string Tensieuthi { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
