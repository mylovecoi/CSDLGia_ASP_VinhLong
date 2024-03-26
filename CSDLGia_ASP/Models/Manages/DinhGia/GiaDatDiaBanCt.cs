using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDatDiaBanCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Maloaidat { get; set; }
        public string Madiaban { get; set; }
        public string Maxp { get; set; }
        public string Khuvuc { get; set; }
        public string Diemdau { get; set; }
        public string Diemcuoi { get; set; }
        public string Loaiduong { get; set; }
        public string Mota { get; set; }
        public string Mdsd { get; set; }
        public double Giavt1 { get; set; }
        public double Giavt2 { get; set; }
        public double Giavt3 { get; set; }
        public double Giavt4 { get; set; }
        public double Giavt5 { get; set; }
        public double Hesok { get; set; }
        public double Sapxep { get; set; }
        public string Trangthai { get; set; }
        public string HienThi { get; set; }
        public string Style { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string MaDv { get; set; }
    }
}
