using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaHhDvkCtTh
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Mahhdv { get; set; }
        public string Manhom { get; set; }
        public DateTime Ngaychotbc { get; set; }
        public string Tenhhdv { get; set; }
        public string Dacdiemkt { get; set; }
        public string Xuatxu { get; set; }
        public string Dvt { get; set; }
        public double Gialk { get; set; }
        public double Gia { get; set; }
        public string Loaigia { get; set; }
        public string Nguontt { get; set; }
        public string Ghichu { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
