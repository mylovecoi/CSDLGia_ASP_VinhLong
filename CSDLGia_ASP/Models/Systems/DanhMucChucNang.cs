using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class DanhMucChucNang
    {

        [Key]
        public int Id { get; set; }
        public string Maso { get; set; }
        public string Capdo { get; set; }
        public string Maso_goc { get; set; }
        public string Menu { get; set; }
        public string Mota { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
