using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaPhiLePhiDm
    {
        [Key]
        public int Id { get; set; }
        public string Phanloai { get; set; }
        public string Manhom { get; set; }
        public string Tennhom { get; set; }
        public string Dvt { get; set; }
        public string Stt { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        //Bổ sung
        public string MaSo { get; set; }
        public string MaSoGoc { get; set; }
        public string HienThi { get; set; }//HIện thị dữ liệu ra màn hình
        public int STT { get; set; }//Số thứ tự theo mã gốc
        public int CapDo { get; set; }//Bắt đầu từ 1

    }
}
