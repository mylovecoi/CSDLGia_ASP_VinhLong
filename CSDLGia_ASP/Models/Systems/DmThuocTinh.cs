using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class DmThuocTinh
    {
        [Key]
        public int Id { get; set; }
        public string MaThuocTinh { get; set; }
        public string TenThuocTinh { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        public int STTSapxep { get; set; }
        public string STTHienthi { get; set; }
        public string Style { get; set; }
    }
}
