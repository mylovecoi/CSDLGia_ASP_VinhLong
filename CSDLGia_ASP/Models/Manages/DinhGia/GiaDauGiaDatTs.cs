using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDauGiaDatTs
    {
        [Key]
        public int Id { get; set; }
        public string Mahuyen { get; set; }
        public string Maxa { get; set; }
        public string Tenduan { get; set; }
        public DateTime Thoidiem { get; set; }
        public string Dientichdat { get; set; }
        public string Dientichsanxd { get; set; }
        public string Soqdpagia { get; set; }
        public string Soqddaugia { get; set; }
        public string Soqdgiakhoidiem { get; set; }
        public string Soqdkqdaugia { get; set; }
        public string Mahs { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string PhanLoaiHoSo { get; set; }//0: Hồ sơ nhập chi tiết; 1: Hồ sơ nhận dữ liệu từ file excel
        public string CodeExcel { get; set; }
    }
}
