using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaHangHoaTaiSieuThiDm
    {
        [Key]
        public int Id { get; set; }
        public string Matt { get; set; }
        public string Tentt { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
