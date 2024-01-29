using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaHangHoaTaiSieuThiDmHHTaiSieuThi
    {
        [Key]
        public int Id { get; set; }
        public string Mahanghoa{ get; set; }
        public string Tenhanghoa { get; set; }
        public string Doituongsd { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
