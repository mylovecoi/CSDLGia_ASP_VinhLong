using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class DsNhomTaiKhoan
    {
        [Key]
        public int Id { get; set; }
        public string Maso { get; set; }
        public string Mota { get; set; }
        public string Permission { get; set; }
        public bool Macdinh { get; set; }
        public string Ghichu { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Chucnang { get; set; }
    }
}
