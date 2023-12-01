using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class DmLoaiDat
    {
        [Key]
        public int Id { get; set; }
        public string Maloaidat { get; set; }
        public string Loaidat { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
