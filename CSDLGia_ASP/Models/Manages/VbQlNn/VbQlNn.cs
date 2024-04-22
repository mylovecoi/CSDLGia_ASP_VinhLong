using CSDLGia_ASP.Models.Manages.DinhGia;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.VbQlNn
{
    public class VbQlNn
    {
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Kyhieuvb { get; set; }
        public string Dvbanhanh { get; set; }
        public string Loaivb { get; set; }
        public DateTime Ngaybanhanh { get; set; }
        public DateTime Ngayapdung { get; set; }
        public string Tieude { get; set; }
        public string Ghichu { get; set; }
        public string Phanloai { get; set; }
        public string Ipf1 { get; set; }
        [NotMapped]
        public string Ipf1upload { get; set; }
        public string Ipf2 { get; set; }
        [NotMapped]
        public string Ipf2upload { get; set; }
        public string Ipf3 { get; set; }
        [NotMapped]
        public string Ipf3upload { get; set; }
        public string Ipf4 { get; set; }
        [NotMapped]
        public string Ipf4upload { get; set; }
        public string Ipf5 { get; set; }
        [NotMapped]
        public string Ipf5upload { get; set; }
        public string Madv { get; set; }
        public string Trangthai { get; set; }
        public DateTime Thoidiem { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        [NotMapped]
        public List<ThongTinGiayTo> ThongTinGiayTo { get; set; }
    }
}
