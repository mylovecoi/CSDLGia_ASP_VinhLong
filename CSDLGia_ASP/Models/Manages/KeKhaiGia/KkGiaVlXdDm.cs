using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.KeKhaiGia
{
    public class KkGiaVlXdDm
    {
        [Key]
        public int id { get; set; }
        public string tennhom { get; set; }
        public string ten { get; set; }
        public string level { get; set; }
        public string theodoi { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
