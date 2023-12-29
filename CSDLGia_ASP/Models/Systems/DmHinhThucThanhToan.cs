using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class DmHinhThucThanhToan
    {
        [Key]
        public int Id { get; set; }
        public string Mahinhthucthanhtoan { get; set; }
        public string Tenhinhthucthanhtoan { get; set; }
        public string Ghichu { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
