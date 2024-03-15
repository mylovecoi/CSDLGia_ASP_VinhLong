using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaThiTruong
    {
        [Key]
        public int Id { get; set; }
        public string Thanglk { get; set; }
        public string Thang { get; set; }
        public string Namlk { get; set; }
        public string Nam { get; set; }
        public string Sobc { get; set; }
        public DateTime Ngaybc { get; set; }
        public string Mahuyen { get; set; }
        public string Trangthai { get; set; }
        public string Mahs { get; set; }
        public string Matt { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string PhanLoaiHoSo { get; set; }//0: Hồ sơ nhập chi tiết; 1: Hồ sơ nhận dữ liệu từ file excel
        public string CodeExcel { get; set; }
    }
}
