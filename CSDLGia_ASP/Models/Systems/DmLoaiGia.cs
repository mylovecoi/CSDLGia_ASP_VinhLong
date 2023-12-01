using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class DmLoaiGia
    {
        [Key]
        public int Id { get; set; }
        public string Maloaigia { get; set; }
        public string Tenloaigia { get; set; }
        public string Ghichu { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
