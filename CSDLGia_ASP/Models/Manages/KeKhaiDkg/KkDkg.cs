using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.KeKhaiDkg
{
    public class KkDkg
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Maxa { get; set; }
        public string Mahuyen { get; set; }
        public string Theoqd { get; set; }
        public DateTime Ngaynhap { get; set; }
        public string Socv { get; set; }
        public string Socvlk { get; set; }
        public DateTime Ngaycvlk { get; set; }
        public DateTime Ngayhieuluc { get; set; }
        public string Nguoinop { get; set; }
        public string Dtlh { get; set; }
        public string Fax { get; set; }
        public DateTime Ngaynhan { get; set; }
        public string Sohsnhan { get; set; }
        public DateTime Ngaychuyen { get; set; }
        public string Lydo { get; set; }
        public string Trangthai { get; set; }
        public string Plhs { get; set; }
        public string Pldn { get; set; }
        public string Thoihan { get; set; }
        public string Phanloai { get; set; }
        public string Ghichu { get; set; }
        public string Congbo { get; set; }
        public string Ipt1 { get; set; }
        public string Ipf1 { get; set; }
        public string Ipt2 { get; set; }
        public string Ipf2 { get; set; }
        public string Ipt3 { get; set; }
        public string Ipf3 { get; set; }
        public string Ipt4 { get; set; }
        public string Ipf4 { get; set; }
        public string Ipt5 { get; set; }
        public string Ipf5 { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
