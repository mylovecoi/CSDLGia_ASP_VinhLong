using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class DmPhanLoaiGia
    {
        [Key]
        public int Id { get; set; }
        public string MaPhanLoaiGia { get; set; }
        public string TenPhanLoaiGia { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public int STTSapxep { get; set; }
        public string STTHienthi { get; set; }
        public string Style { get; set; }
    }
}
