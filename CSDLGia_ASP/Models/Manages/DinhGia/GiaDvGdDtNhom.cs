using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDvGdDtNhom
    {
        [Key]
        public int Id { get; set; }
        public int SapXep { get; set; }
        public string MaNhom { get; set; }
        public string TenNhom { get; set; }
        public string Syle { get; set; }
        //Trang thái kết nối CSDLQG
        public string TrangThaiCSDLQG { get; set; }
        public DateTime NgayKetNoi { get; set; }
    }
}
