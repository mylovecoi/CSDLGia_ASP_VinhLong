using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaHhDvkThCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Mahhdv { get; set; }
        public double Gialk { get; set; }
        public double Gia { get; set; }
        public string Loaigia { get; set; } = "5";//Giá bán lẻ (theo mã Thông 93)
        public string Nguontt { get; set; } = "2";//Do cơ quan/đơn vị quản lý nhà nước có liên quan cung cấp/báo cáo theo quy định (theo mã Thông 93)
        public string Ghichu { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Parse("0001-01-01");
        public DateTime Updated_at { get; set; } = DateTime.Parse("0001-01-01");
    }
}
