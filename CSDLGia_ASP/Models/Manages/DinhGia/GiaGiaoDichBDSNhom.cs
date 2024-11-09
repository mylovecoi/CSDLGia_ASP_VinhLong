using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaGiaoDichBDSNhom
    {
        [Key]
        public int Id { get; set; }
        public string Manhom { get; set; }
        public string Tennhom { get; set; }
        public string Sapxep { get; set; }
        public string Theodoi { get; set; }
        //Trang thái kết nối CSDLQG
        public string TrangThaiCSDLQG { get; set; }
        public DateTime NgayKetNoi { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
