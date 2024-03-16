using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaNuocShDmVung
    {
        [Key]
        public int Id { get; set; }
        public string Madoituong { get; set; }
        public string Doituongsd { get; set; }
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
