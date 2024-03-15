using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaThueNhaCongVu
    {
        [Key]
        public int Id { get; set; }
        public string District { get; set; }
        public string Manhom { get; set; }
        public string Tenduan { get; set; }
        public string Mota { get; set; }
        public string Dientich { get; set; }
        public string Dvt { get; set; }
        public string Dongiathue { get; set; }
        public string Thoidiemkc { get; set; }
        public string Thoidiemht { get; set; }
        public string Ttqd { get; set; }
        public string Ghichu { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string PhanLoaiHoSo { get; set; }//0: Hồ sơ nhập chi tiết; 1: Hồ sơ nhận dữ liệu từ file excel
        public string CodeExcel { get; set; }
    }
}
